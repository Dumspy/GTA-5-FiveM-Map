import { Command, CommandMessage, Guard } from '@typeit/discord';
import { NotSelf } from '../guards/NotSelf'
import { NotDM } from '../guards/NotDM'
import sessionInteraction from '../libs/sessionInteraction'

export abstract class Status {
    @Command('Status')
    @Guard(NotSelf, NotDM)
    async status(command: CommandMessage) {
        command.reply(await sessionInteraction.SessionStatusByDiscord(parseInt(command.author.id)) ? 'true' : 'false')
    }
}