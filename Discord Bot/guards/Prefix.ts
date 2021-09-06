import {
    Client,
    ArgsOf,
    GuardFunction
} from '@typeit/discord';

export function Prefix(text: string) {
    const Prefix: GuardFunction<'message'> = async ([message]: ArgsOf<'message'>, client, next) => {
        if (message.content.startsWith(text)) {
            await next();
        }
    };
    return Prefix;
}