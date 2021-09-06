import SessionSchema from '../models/sessionModel'
import AgentSchema from '../models/agentModel'

export default class sessionInteraction {
    public static async SessionStatusByDiscord(DiscordID: Number) {
        const dbResult = await AgentSchema.findOne({
            discordid: DiscordID
        })
        if (!dbResult) { return undefined }

        const dbResult2 = await SessionSchema.findOne({
            'owner': dbResult._id,
            'status.end': null
        })

        return dbResult2 ? true : false
    }

    public static async SessionStatusByAgentID(AgentID: Number) {
        const dbResult2 = await SessionSchema.findOne({
            'owner': AgentID,
            'status.end': null
        })
        return dbResult2 ? true : false
    }

    public static async CheckOut(DiscordID: Number) {
        const dbResult = await AgentSchema.findOne({
            discordid: DiscordID
        })
        if (!dbResult) { return false }
        if(!await this.SessionStatusByAgentID(dbResult._id)){return false}
        var dbResult2 = await SessionSchema.findOne({
            'owner': dbResult._id,
            'status.end': null
        })

        if(dbResult2){
            //@ts-ignore
            dbResult2.status.end = Date.now()
            dbResult2.save()
            return true
        }
        return false
    }

    public static async CheckIn(DiscordID: Number){
        const dbResult = await AgentSchema.findOne({
            discordid: DiscordID
        })
        if(!dbResult){return false}
        if(await this.SessionStatusByAgentID(dbResult._id)){return false}
        const currentSession = new SessionSchema({
            'owner': dbResult._id
        })
        await currentSession.save()
        return true
    }

    public static async Update(DiscordID: Number){
        const dbResult = await AgentSchema.findOne({
            discordid: DiscordID
        })
        if(!dbResult){return false}
        if(await this.SessionStatusByAgentID(dbResult._id)){
            var dbResult2 = await SessionSchema.findOne({
                'owner': dbResult._id,
                'status.end': null
            })
    
            if(dbResult2){
                //@ts-ignore
                dbResult2.status.end = Date.now()
                dbResult2.save()
                return true
            }
            return false
        }
        const currentSession = new SessionSchema({
            'owner': dbResult._id
        })
        await currentSession.save()
        return true
    }
}