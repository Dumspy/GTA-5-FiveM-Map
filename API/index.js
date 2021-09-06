require('dotenv').config()
const 
    bodyParser = require('body-parser')
    cors = require('cors')
    mongodb = require('./lib/mongodb')
    fs = require('fs').promises
    path = require('path')
    Express = require('express')
    app = Express()
    port = process.env.WEB_PORT
    Sentry = require('@sentry/node')

app.use(bodyParser.urlencoded({
    'extended': true
}))

app.use(bodyParser.json())
app.use(cors())

async function startUp(){
    await mongodb.init()
    await Sentry.init({ dsn: 'https://16cf57ea3c8a4bb9a3e7261e4523bcd7@o224625.ingest.sentry.io/5246855' });
    console.info('Loading apis...')
    const routeFolder = path.join(__dirname, 'api')
    const files = await fs.readdir(routeFolder)

    for (let i = 0; i < files.length; i++) {
        const fileName = files[i]
        const basename = path.basename(fileName, path.extname(fileName))
        app.use('/api/' + basename, require(path.join(routeFolder, fileName)))
        console.debug('Registered ' + '/api/' + basename)
    }

    // Start webserver as last thing
    let server = app.listen(port)

    if (!server) {
        throw 'Could not start server on ' + port
    }

    console.log('Running server on ' + port)
}

startUp().catch(err =>{
    throw err
})