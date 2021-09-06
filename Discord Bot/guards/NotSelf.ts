import {
    Client,
    ArgsOf,
    GuardFunction
} from '@typeit/discord';

export const NotSelf: GuardFunction<'message'> = async ([message]: ArgsOf<'commandMessage'>, client: Client, next) => {
    if (client.user.id !== message.author.id) {
        await next();
    }
}