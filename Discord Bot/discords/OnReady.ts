import { Discord, On } from '@typeit/discord';

@Discord()
abstract class AppDiscord {
    @On('ready')
    private onReady() {
        console.info('Bot Ready')
    }
}