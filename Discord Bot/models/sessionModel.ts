import * as mongoose from 'mongoose';
import { IAgent } from './agentModel'

export interface ISession extends mongoose.Document {
    status: {
        start: Date
        end?: Date
    }
    owner: IAgent['_id'];
}

const SessionSchema: mongoose.Schema = new mongoose.Schema({
    status: {
        start: { type: Date, default: Date.now },
        end: { type: Date, default: null }
    },
    owner: { type: mongoose.Schema.Types.ObjectId }
});

// Export the model and return your IUser interface
export default mongoose.model<ISession>('Session', SessionSchema);