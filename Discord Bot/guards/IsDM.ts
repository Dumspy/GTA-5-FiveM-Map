import { Client, ArgsOf, GuardFunction } from '@typeit/discord';

export const IsDM: GuardFunction<'message'> = async ([message]: ArgsOf<'commandMessage'>, client : Client, next) => {
    if (message.channel.type === 'dm') {
        await next();
    }
}