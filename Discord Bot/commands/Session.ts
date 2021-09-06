import { Command, CommandMessage, Guard } from '@typeit/discord';
import { NotSelf } from '../guards/NotSelf'
import { NotDM } from '../guards/NotDM'
import sessionInteraction from '../libs/sessionInteraction'

export abstract class Session {
    @Command('Session')
    @Guard(NotSelf, NotDM)
    async session(command: CommandMessage) {
        const args = command.content.split(/ +/)
        args.shift()
        const autherId = parseInt(command.author.id)
        if(!args[0]){return}
        switch (args[0].toLocaleLowerCase()) {
            case 'start':
                if (await sessionInteraction.CheckIn(autherId)) {
                    command.reply('Session started')
                    return
                }
                command.reply('Something went wrong')
                break;
            case 'end':
                if (await sessionInteraction.CheckOut(autherId)) {
                    command.reply('Session ended')
                    return
                }
                command.reply('Something went wrong')
                break;
            case 'update':
                if(await sessionInteraction.Update(autherId)){
                    command.reply('Session updated')
                    return
                }
                command.reply('Something went wrong')
                break;
            case 'status':
                const status = await sessionInteraction.SessionStatusByDiscord(autherId) ? 'active' : 'inactive'
                command.reply('Current session status: ' + status)
                break;
        }
    }
}