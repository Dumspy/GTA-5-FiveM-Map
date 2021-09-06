import { Discord, On, Client, ArgsOf, Guard } from '@typeit/discord';
import { TextChannel, MessageEmbed, Message } from 'discord.js';
import { NotSelf } from '../guards/NotSelf';
import { IsDM } from '../guards/IsDM';
import { Prefix } from '../guards/Prefix';

@Discord()
abstract class DMHandler {
    @On('message')
    @Guard(NotSelf, IsDM)
    private async onMessage([message]: ArgsOf<'message'>, client: Client) {
        const args = message.content.split(/ +/);
        const command = args.shift().toLowerCase().slice(1);
        switch (command) {
            case 'ansøg':
                if (args.length === 0) {
                    const memberCache = client.guilds.cache.get(process.env.DISCORD_POLITI_ID).members.cache.get(message.author.id);
                    if (typeof memberCache === 'undefined') {
                        return;
                    }
                    const applicationEmbed = new MessageEmbed()
                        .setAuthor('P.E.T')
                        .setTitle('Ansøgnings Formular')
                        .setDescription('```Hvorfor vil du være en del af P.E.T:\n*\nHvad har du af erfaring:\n*\nHvorfor skal vi vælge dig:\n*```')
                        .setColor(9025)
                        .setTimestamp()
                        .addField(':e_mail:', 'Udfyld formularen og send den tilbage')
                        .setFooter('P.E.T', client.user.avatarURL());
                    message.channel.send(applicationEmbed);
                } else {
                    console.log(message.content);
                }
                break;
            default:
                let str = message.content;
                const memberCache = client.guilds.cache
                    .get(process.env.DISCORD_POLITI_ID)
                    .members.cache.get(message.author.id);
                if (typeof memberCache === 'undefined') {
                    return;
                }
                if (
                    str.includes('Hvorfor vil du være en del af P.E.T:') &&
                    str.includes('Hvad har du af erfaring:') &&
                    str.includes('Hvorfor skal vi vælge dig:')
                ) {
                    let response = str.split('\n');
                    var formattedResponse: string[] = ['', '', ''];
                    let i = -1;

                    for (const string of response) {
                        if (
                            string === 'Hvorfor vil du være en del af P.E.T:' ||
                            string === 'Hvad har du af erfaring:' ||
                            string === 'Hvorfor skal vi vælge dig:'
                        ) {
                            i++;
                            continue;
                        }
                        formattedResponse[i] += string;
                    }

                    const applicationRecived = new MessageEmbed()
                        .setAuthor('P.E.T Ansøgning')
                        .setTitle(memberCache.nickname + ' Ansøgning')
                        .setDescription(
                            '**Hvorfor vil du være en del af P.E.T:**\n' +
                            formattedResponse[0] +
                            '\n**Hvad har du af erfaring:**\n' +
                            formattedResponse[1] +
                            '\n**Hvorfor skal vi vælge dig:**\n' +
                            formattedResponse[2]
                        )
                        .setColor(9025)
                        .setTimestamp()
                        .setFooter('P.E.T', client.user.avatarURL());
                    client.channels
                        .fetch(process.env.DISCORD_PET_ANSOENINGER_ID)
                        .then(async (c: TextChannel) => {
                            c.send(applicationRecived).then((message: Message) => {
                                message.react('👍'), message.react('👎');
                            });
                        });
                }
                break;
        }
    }
}
