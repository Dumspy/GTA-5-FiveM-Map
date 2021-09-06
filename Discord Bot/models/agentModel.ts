import * as mongoose from 'mongoose';
import {IAPIKey} from './apiKeyModel'

export interface IAgent extends mongoose.Document {
    username: string;
    alias?: string;
    password: string;
    authed: Boolean;
    rank: Number;
    accessLevel: Number;
    api: IAPIKey['_id'];
    discordid?: Number;
}

const AgentSchema: mongoose.Schema = new mongoose.Schema({
    username: { type: String, unique: true, lowercase: true, required: true },
    alias: { type: String, unique: true, default: undefined },
    password: { type: String, required: true },
    authed: { type: Boolean, default: false },
    rank: { type: Number, min: 0, max: 3, default: 0 },
    accessLevel: { type: Number, min: 0, max: 2, default: 0 },
    api: { type: mongoose.Schema.Types.ObjectId},
    discordid:{type: Number, unique: true}
});

// Export the model and return your IUser interface
export default mongoose.model<IAgent>('Agent', AgentSchema);
