import * as mongoose from 'mongoose';
import {IAgent} from './agentModel'

export interface IAPIKey extends mongoose.Document {
    key?: string;
    expiration?: Date;
    owner: IAgent['_id'];
}

const APIKeySchema: mongoose.Schema = new mongoose.Schema({
    key: { type: String, unique: true},
    expiration: { type: Date},
    owner: { type: mongoose.Schema.Types.ObjectId},
});

// Export the model and return your IUser interface
export default mongoose.model<IAPIKey>('apiKey', APIKeySchema);
