const sessionModel = require('../models/sessionModel')

async function CheckIn(key) {
    if (!key) { return 'error' }
    var currentSession = new sessionModel({
        'owner': key
    })
    await currentSession.save()
    return 'success'
}

async function CheckOut(key) {
    if (!key) { return 'error' }
    var dbSession = await sessionModel.findOne({
        'owner': key,
        'status.end': null
    })

    if (dbSession) {
        dbSession.status.end = Date.now()
        dbSession.save()
        return 'success'
    }
    return 'error'
}

async function ActiveSession(key) {
    if (!key) { return 'error' }
    var dbSession = await sessionModel.findOne({
        'owner': key,
        'status.end': null
    })
    return dbSession ? true : false
}

async function ActiveSessionByUserId(UserId) {
    var dbSession = await sessionModel.findOne({
        'owner': UserId,
        'status.end': null
    })
    return dbSession ? true : false
}

async function GetUsernamesWithActiveSession() {
    var dbSessions = await sessionModel.find({
        'status.end': null
    }).populate({ path: 'owner' })

    usernames = ""
    for (const session of dbSessions) {
        if (!session.owner.alias) {
            usernames += session.owner.username + ","
        } else {
            usernames += session.owner.alias + ","
        }
    }
    usernames = usernames.substring(0, usernames.length - 1);
    return usernames
}

module.exports = {
    sessionModel,
    CheckIn,
    CheckOut,
    ActiveSession,
    ActiveSessionByUserId,
    GetUsernamesWithActiveSession
}