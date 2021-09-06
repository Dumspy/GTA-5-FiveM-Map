import { Client } from '@typeit/discord';
import { MongoDB } from './libs/mongodb'
import * as dotenv from 'dotenv';
import * as mongoose from 'mongoose';

dotenv.config()

async function start() {
  MongoDB.Init()
  const client = new Client({
    classes: [
      `${__dirname}/discords/*.ts`, // glob string to load the classes
      `${__dirname}/discords/*.js` // If you compile using "tsc" the file extension change to .js
    ],
    silent: false,
    variablesChar: ':'
  });

  await client.login(process.env.DISCORD_TOKEN);
}
start();

mongoose.connection.on('open', () => {
  console.info(`Connected to mongodb instance at ${MongoDB.getConnectionString()}`)
})

mongoose.connection.on('error', (err: any) => {
  console.error(err)
})