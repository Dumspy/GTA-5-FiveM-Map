const apiKeyModel = require('../models/apiKeyModel.js')
const crypto = require('crypto')
const util = require('util')

const randomBytes = util.promisify(crypto.randomBytes)

async function isValid(key) {
    var dbKey = await apiKeyModel.findOne({
        'key': key
    })
    if(dbKey){
        await dbKey.populate('owner').execPopulate()
        if(dbKey.owner.authed){
            return dbKey.expiration>Date.now()         
        }
    }
    return false
}

async function keyToOwnerID(key){
    var dbKey = await apiKeyModel.findOne({
        'key': key
    })
    return dbKey ? dbKey.owner : null
}

async function keyToAccessLevel(key){
    var dbKey = await apiKeyModel.findOne({
        'key': key
    })
    if(dbKey){
        await dbKey.populate('owner').execPopulate()
    }
    return dbKey ? dbKey.owner.accessLevel : -1
}

async function keyToRank(key){
    var dbKey = await apiKeyModel.findOne({
        'key': key
    })
    if(dbKey){
        await dbKey.populate('owner').execPopulate()
    }
    return dbKey ? dbKey.owner.rank : -1
}

async function resetKey(keyId) {
    var dbKey = await apiKeyModel.findOne({
        '_id': keyId
    })
    let bytes = await randomBytes(32)
    dbKey.key = bytes.toString('hex')
    dbKey.expiration = Date.now() + 86400000
    await dbKey.save()
}

async function createKey(ownerId) {
    let bytes = await randomBytes(32)
    var apiKey = new apiKeyModel()
    apiKey.key = bytes.toString('hex')
    apiKey.expiration = Date.now() + 86400000
    apiKey.owner = ownerId
    await apiKey.save()
    return apiKey._id
}

module.exports = {
    apiKeyModel,
    isValid,
    resetKey,
    createKey,
    keyToOwnerID,
    keyToAccessLevel,
    keyToRank
}