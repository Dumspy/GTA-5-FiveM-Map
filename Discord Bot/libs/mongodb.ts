import * as mongoose from 'mongoose'

export class MongoDB {
    private static Vars = [
        'DB_HOST',
        'DB_PORT',
        'DB_DATABASE',
        'DB_PASSWORD',
        'DB_USERNAME'
    ]

    private static connectionString: string

    public static getConnectionString(): string {
        return this.connectionString
    }

    private static host: string
    private static port: string
    private static database: string
    private static username: string
    private static password: string

    public static Init(): void {
        for (let i = 0; i < this.Vars.length; i++) {
            const variable = this.Vars[i]
            if (typeof process.env[variable] === 'undefined') {
                throw 'Missing environment variable ' + variable
            }
        }

        this.host = process.env.DB_HOST
        this.port = process.env.DB_PORT
        this.database = process.env.DB_DATABASE
        this.username = process.env.DB_USERNAME
        this.password = process.env.DB_PASSWORD

        if (this.username != 'false' && this.password != 'false') {
            this.connectionString = `mongodb://${this.username}:${this.password}@${this.host}:${this.port}/${this.database}`
        }
        else {
            this.connectionString = `mongodb://${this.host}:${this.port}/${this.database}`
        }
        this.Connect()
    }

    public static async Connect() {
        console.info(`Connecting to mongodb instance at ${this.connectionString}`)
        await mongoose.connect(this.connectionString, {
            useNewUrlParser: true,
            useUnifiedTopology: true
        }).catch(err => {
            console.error(err)
        })
    }
}