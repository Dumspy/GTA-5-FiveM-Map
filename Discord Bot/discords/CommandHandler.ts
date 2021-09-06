import { CommandNotFound, Discord, CommandMessage, On, ArgsOf, Client } from '@typeit/discord';
import * as Path from 'path';

@Discord('!', {import: [Path.join(__dirname, '..', 'commands', '*.js')]})
export class DiscordApp {
    @CommandNotFound()
    notFoundA(command: CommandMessage) {
        if(command.channel.type === 'dm'){return}
        command.reply('Command not found');
    }
}