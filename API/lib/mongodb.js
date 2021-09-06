const mongoose = require('mongoose')

const Vars = [
    'DB_HOST',
    'DB_PORT',
    'DB_DATABASE',
    'DB_PASSWORD',
    'DB_USERNAME'
]

async function init() {
    for (let i = 0; i < Vars.length; i++) {
        const variable = Vars[i]
        if (typeof process.env[variable] === 'undefined') {
            throw 'Missing environment variable ' + variable
        }
    }

    const host = process.env.DB_HOST
    const port = process.env.DB_PORT
    const database = process.env.DB_DATABASE
    const username = process.env.DB_USERNAME
    const password = process.env.DB_PASSWORD

    var connectionString
    if(username != 'false' && password != 'false'){
        connectionString = `mongodb://${username}:${password}@${host}:${port}/${database}`
    }
    else{
        connectionString = `mongodb://${host}:${port}/${database}`
    }

    console.log(`Connecting to mongodb instance at ${connectionString}`)

    await mongoose.connect(connectionString, {
        useNewUrlParser: true,
        useUnifiedTopology: true
    })

    console.log('Connected to mongodb!')
}

mongoose.connection.on('error', err => {
    console.error(err)
})

module.exports = {
    mongoose,
    init
}