const mongoose = require('mongoose')

const agentSchema = new mongoose.Schema({
    username: { type: String, unique: true, lowercase: true },
    alias: { type: String, unique:true, default: undefined },
    password: String,
    authed: { type: Boolean, default: false },
    rank: { type: Number, min: 0, max: 3, default: 0 }, //0 Kadet 1 Agent 2 Ledelse 3 Rigspoliti
    accessLevel: { type: Number, min: 0, max: 2, default: 0 },
    api: { type: mongoose.Schema.Types.ObjectId, ref: 'apiKey' },
    discordid:{type: Number, unique: true}
});

var Agent = mongoose.model('agent', agentSchema)

module.exports = Agent