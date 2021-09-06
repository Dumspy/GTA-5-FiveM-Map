const express = require('express')
const router = express.Router()
const waypointModel = require('../lib/waypointModelInteraction')
const agentModel = require('../lib/agentModelInteraction')
const apiKeyModel = require('../lib/apiKeyModelInteraction')
const sessionModel = require('../lib/sessionModelInteraction')


// /api/map/*

//Waypoints
router.post('/createWaypoint', async (req, res) => {
    if (await apiKeyModel.isValid(req.body.key)) {
        res.send(await waypointModel.createWaypoint(req.body))
        return
    }
    res.send('key error')
})

router.get('/getWaypoints&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        let waypointsJson = await waypointModel.getAllWaypoints()
        waypointsJson = '{"Values":' + JSON.stringify(waypointsJson) + '}'
        res.send(waypointsJson)
        return
    }
    res.send('key error')
})

router.post('/updateWaypoint', async (req, res) => {
    if (await apiKeyModel.isValid(req.body.key)) {
        res.send(await waypointModel.updateWaypoint(req.body))
        return
    }
    res.send('key error')
})

router.delete('/deleteWaypoint&id=:id&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(await waypointModel.deleteWaypoint(req.params.id))
        return
    }
    res.send('key error')
})

//User
router.get('/usernameInUse&username=:username', (req, res) => {
    agentModel.doesUsernameExits(req.params.username, true).then(response => {
        res.send(response)
    })
})

router.post('/createUser', (req, res) => {
    agentModel.createUser(req.body).then(response => {
        res.send(response)
    })
})

router.post('/loginUser', (req, res) => {
    agentModel.loginUser(req.body).then(response => {
        res.send(response)
    })
})

router.get('/getAllUser&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key) && await apiKeyModel.keyToRank(req.params.key) >= 2) {
        let userJson = await agentModel.getAllUsers()
        userJson = '{"Values":' + JSON.stringify(userJson) + '}'
        res.send(userJson)
        return
    }
    res.send('key error')
})

router.post('/updateUser&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key) && await apiKeyModel.keyToRank(req.params.key) >= 2) {
        await agentModel.updateUser(req.body)
        res.send('success')
        return
    }
    res.send('key error')
})
//Sessions
router.post('/checkIn&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(await sessionModel.CheckIn(await apiKeyModel.keyToOwnerID(req.params.key)))
        return
    }
    res.send('key error')
})

router.post('/checkUd&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(await sessionModel.CheckOut(await apiKeyModel.keyToOwnerID(req.params.key)))
        return
    }
    res.send('key error')
})

router.get('/getActiveSession&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(await sessionModel.ActiveSession(await apiKeyModel.keyToOwnerID(req.params.key)))
        return
    }
    res.send('key error')
})

router.get('/getActiveSessionById&id=:id&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key) && await apiKeyModel.keyToRank(req.params.key) >= 2) {
        res.send(await sessionModel.ActiveSessionByUserId(req.params.id))
        return
    }
    res.send('key error')
})

router.get('/getAccessLevel&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(await apiKeyModel.keyToAccessLevel(req.params.key) + "")
        return
    }
    res.send('key error')
})

router.get('/getRank&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(await apiKeyModel.keyToRank(req.params.key) + "")
        return
    }
    res.send('key error')
})

router.get('/getActiveSessions&key=:key', async (req, res) => {
    if (await apiKeyModel.isValid(req.params.key)) {
        res.send(JSON.stringify(await sessionModel.GetUsernamesWithActiveSession()))
        return
    }
    res.send('key error')
})

module.exports = router