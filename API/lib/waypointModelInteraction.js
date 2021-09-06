const waypointModel = require('../models/waypointModel.js')

async function createWaypoint(body) {
    const currentWaypoint = new waypointModel({
        name: body.name,
        pos: {
            x: parseFloat(body.posX.replace(',', '.')),
            y: parseFloat(body.posY.replace(',', '.'))
        },
        iconId: body.iconID,
        description: body.description,
        color: {
            r: body.r,
            g: body.g,
            b: body.b
        }
    })
    await currentWaypoint.save()
    return currentWaypoint._id
}

async function updateWaypoint(body) {
    var dbWaypoint = await waypointModel.findOne({
        '_id': body.id
    })
    if (dbWaypoint) {
        dbWaypoint.name = body.name,
            dbWaypoint.iconId = body.iconID,
            dbWaypoint.Description = body.description,
            dbWaypoint.color = {
                r: body.r,
                g: body.g,
                b: body.b
            }
        dbWaypoint.updated = Date.now()
        dbWaypoint.save()
        return 'success'
    }
    return 'error'
}

async function getAllWaypoints() {
    const filter = { deleted: false };
    const all = await waypointModel.find(filter);
    return all
}

async function deleteWaypoint(id) {
    var dbWaypoint = await waypointModel.findOne({
        '_id': id
    })
    if (dbWaypoint) {
        dbWaypoint.deleted = true;
        await dbWaypoint.save()
        return 'success'
    }
    return 'error'
}

module.exports = {
    waypointModel,
    createWaypoint,
    updateWaypoint,
    getAllWaypoints,
    deleteWaypoint
}