const agentModel = require('../models/agentModel.js')
const apiKeyModel = require('./apiKeyModelInteraction.js')
const bcrypt = require('bcrypt')

async function doesUsernameExits(username, onlyBool = false) {
    var dbUser = await agentModel.findOne({
        'username': username
    })
    if (!onlyBool) {
        return dbUser ? dbUser : false
    }
    return dbUser ? true : false
}

async function createUser(body) {
    let response = await doesUsernameExits(body.username, true)
    if (response) { return 'usernameInUse' }
    body.password = await bcrypt.hash(body.password, 10);
    const currentAgent = new agentModel({
        username: body.username,
        password: body.password,
        discordid: Math.floor(1111 + Math.random() * 9999)
    })
    await currentAgent.save()
    return 'accountCreated'
}

async function loginUser(body) {
    let dbAgent = await doesUsernameExits(body.username)
    if (!dbAgent) { return 'noUser' }
    if (!dbAgent.authed) { return 'unauthed' }
    if (!await bcrypt.compare(body.password, dbAgent.password)) { return 'wrongPassword' }
    if (!dbAgent.api) {
        dbAgent.api = await apiKeyModel.createKey(dbAgent._id)
        await dbAgent.save()
    } else {
        await apiKeyModel.resetKey(dbAgent.api)
    }
    await dbAgent.populate('api').execPopulate();
    return 'success:'+dbAgent.api.key
}

async function getAllUsers(){
    const agents = await agentModel.find();
    for (const agent of agents) {
        agent.password = undefined
        agent.api = undefined      
    }
    return agents
}

async function updateUser(body){
    var dbUser = await agentModel.findOne({
        '_id': body.id
    })
    if(dbUser){
        dbUser.authed = body.auth == 'true'
        dbUser.rank = body.rank
        dbUser.accessLevel = body.accessLevel
        dbUser.save()
        return 'success'
    }
    return 'error'
}

module.exports = {
    agentModel,
    doesUsernameExits,
    createUser,
    loginUser,
    getAllUsers,
    updateUser
}