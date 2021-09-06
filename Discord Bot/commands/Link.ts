import { Command, CommandMessage, Guard } from '@typeit/discord';
import { NotSelf } from '../guards/NotSelf'
import { NotDM } from '../guards/NotDM'
import AgentSchema from '../models/agentModel'

export abstract class Link {
    @Command('link')
    @Guard(NotSelf, NotDM)
    async link(command: CommandMessage) {
        const args = command.content.split(/ +/)
        if (args[1].length != 4) { return }
        var dbResult = await AgentSchema.findOne({
            discordid: parseInt(args[1])
        })
        if (!dbResult || dbResult.discordid>9999) { return }
        dbResult.discordid = parseInt(command.author.id)
        dbResult.save()
    }
}