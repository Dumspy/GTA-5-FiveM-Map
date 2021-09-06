import {
    Client,
    ArgsOf,
    GuardFunction
} from '@typeit/discord';

export function InServer(serverId: string) {
    const InServer: GuardFunction<'message'> = async ([message]: ArgsOf<'message'>, client, next) => {
        if (message.guild.id == serverId) {
            await next();
        }
    };
    return InServer;
}