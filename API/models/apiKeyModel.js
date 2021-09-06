const mongoose = require('mongoose')

const apiKeySchema = new mongoose.Schema({
    key: { type: String, unique: true },
    expiration: { type: Date },
    owner: { type: mongoose.Schema.Types.ObjectId, ref: 'agent' }
});

var APIKey = mongoose.model('apiKey', apiKeySchema)

module.exports = APIKey