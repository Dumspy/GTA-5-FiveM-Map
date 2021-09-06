const mongoose = require('mongoose')

const sessionSchema = new mongoose.Schema({
    status: {
        start: { type: Date, default: Date.now },
        end: { type: Date, default: null }
    },
    owner: { type: mongoose.Schema.Types.ObjectId, ref: 'agent' }
});

var Session = mongoose.model('session', sessionSchema)

module.exports = Session