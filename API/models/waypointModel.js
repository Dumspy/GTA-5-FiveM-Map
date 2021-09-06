const mongoose = require('mongoose')

const waypointSchema = new mongoose.Schema({
    name: String,
    pos: {
        x: { type: Number, min: -30, max: 30 },
        y: { type: Number, min: -40, max: 40 }
    },
    iconId: Number,
    description: String,
    color: {
        r: { type: Number, min: 0, max: 255 },
        g: { type: Number, min: 0, max: 255 },
        b: { type: Number, min: 0, max: 255 },
    },
    created: { type: Date, default: Date.now },
    updated: { type: Date, default: Date.now },
    deleted: { type: Boolean, default: false}
});

var Waypoint = mongoose.model('waypoint',waypointSchema)

module.exports = Waypoint