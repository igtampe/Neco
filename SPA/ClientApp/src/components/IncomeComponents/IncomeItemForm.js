import { TextField, CircularProgress, Divider, Grid, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, Checkbox, FormControlLabel, Tooltip, Alert } from "@mui/material";
import React, { useState } from "react";
import { APIURL } from "../../App";
import { GenerateJSONPost, GenerateJSONPut } from "../../RequestOptionGenerator";
import { JurisdictionAutoComplete } from "../AdminComponents/JurisdictionComponents/JurisdictionAutocomplete";
import AlertSnackbar from "../AlertSnackbar";

export default function IncomeForm(props) {

    //We have to do this again que lindo :dancing emoji:
    const baseUrl = props.airline
        ? APIURL + "/API/Income/Airlines"
        : props.corporation
            ? APIURL + "/API/Income/Corporations"
            : props.business
                ? APIURL + "/API/Income/Businesses"
                : props.hotel
                    ? APIURL + "/API/Income/Hotels"
                    : props.apartment
                        ? APIURL + "/API/Income/Apartments"
                        : undefined

    const stringType = props.airline
        ? "airline"
        : props.corporation
            ? "corporation"
            : props.business
                ? "business"
                : props.hotel
                    ? "hotel"
                    : props.apartment
                        ? "apartment"
                        : "?????"

    //This massive request will be usable for *ANYTHING*
    const [request, setRequest] = useState({
        name: '', description: '', address: '', jurisdictionID: '', accountID: (props.account ? props.account.id : ""), miscIncome: 0,
        gatesSM: 0, gatesMD: 0, gatesLG: 0,
        rle: '', rleNetYearly: 0, buildings: 0, mergers: 0, metroAds: false, airportAds: false, international: false,
        pointsOfSale: 0, avgSpend: 0, custPerHour: 0, hoursOpen: 0,
        rooms: 0, suites: 0, roomRate: 200, suiteRate: 400,
        sUnits: 0, b1Units: 0, b2Units: 0, b3Units: 0, pUnits: 0, sRent: 500, b1Rent: 750, b2Rent: 1000, b3Rent: 1250, pRent: 1250,
    })

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const [populated, setPopulated] = useState(false);
    const [inProgress, setInProgress] = useState(false);

    if (props.item && !populated && props.open) {
        setPopulated(true);
        setRequest({
            ...request, ...props.item,
            jurisdictionID: (props.item.jurisdiction ? props.item.jurisdiction.id : ""),
            accountID: (props.item.account ? props.item.account.id : "")
        })

    }

    const clearForm = () => {
        setRequest({
            name: '', description: '', address: '', jurisdictionID: '', accountID: (props.account ? props.account.id : ""), miscIncome: 0,
            gatesSM: 0, gatesMD: 0, gatesLG: 0,
            rle: '', rleNetYearly: 0, buildings: 0, mergers: 0, metroAds: false, airportAds: false, international: false,
            pointsOfSale: 0, avgSpend: 0, custPerHour: 0, hoursOpen: 0,
            rooms: 0, suites: 0, roomRate: 200, suiteRate: 400,
            sUnits: 0, b1Units: 0, b2Units: 0, b3Units: 0, pUnits: 0, sRent: 500, b1Rent: 750, b2Rent: 1000, b3Rent: 1250, pRent: 1250,
        })

        setPopulated(false);
    }

    const handleClosing = () => {
        clearForm();
        props.setOpen(false)
    }

    const handleOK = () => {

        setRequest({ ...request, accountID: (props.account ? props.account.id : request.accountID) })
        var R = { ...request, accountID: (props.account ? props.account.id : request.accountID) }

        if (R.accountID === "") {
            setResult({ severity: 'danger', text: 'Item must be owned by an account' })
            setSnackOpen(true)
            return;
        }

        if (R.jurisdictionID === "") {
            setResult({ severity: 'danger', text: 'Please select a jurisdiction' })
            setSnackOpen(true)
            return;
        }

        if (R.name === '') {
            setResult({ severity: 'danger', text: 'Name cannot be empty!' })
            setSnackOpen(true)
            return;
        }

        setInProgress(true)

        var RequestOptions;
        var URL = baseUrl

        if (props.item) {
            RequestOptions = GenerateJSONPut(props.Session, R)
            URL = URL + '/' + props.item.id
        } else {
            RequestOptions = GenerateJSONPost(props.Session, R)
        }

        console.log(RequestOptions.body)

        fetch(URL, RequestOptions)
            .then(response => response.json())
            .then(data => {
                setInProgress(false)
                if (data.error) {
                    setResult({ severity: 'danger', text: data.reason })
                    setSnackOpen(true)
                    return;
                } else if (data.errors) {
                    setResult({ severity: 'danger', text: 'An unknown error occurred!' })
                    setSnackOpen(true)
                    return;
                } else {
                    setResult({ severity: 'success', text: 'Item ' + data.name + ' has been ' + (props.item ? 'updated' : 'created') + '!' })
                    setSnackOpen(true)

                    if (props.setCollection) { props.setCollection(undefined) }
                    handleClosing()

                    return;
                }
            })

    }

    return (
        <>

            <Dialog maxWidth='sm' scroll='body' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> {props.item ? 'Edit an existing ' : 'File a new '} {stringType} </DialogTitle>
                <DialogContent>
                    <DialogContentText>

                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField label="Name" value={request.name} onChange={(event) => setRequest({ ...request, name: event.target.value })} fullWidth
                                    style={{ marginTop: "5px", marginBottom: "5px" }} />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField label="Description" value={request.description} onChange={(event) => setRequest({ ...request, description: event.target.value })} fullWidth
                                    multiline maxRows={4} minRows={4} variant='filled' style={{ marginTop: "5px", marginBottom: "5px" }} />
                            </Grid>
                            <Grid item xs={12}> <Divider /> </Grid>
                            <Grid item xs={6}>
                                <Tooltip title={'Address of the main HQ of this ' + stringType + '. This location must be furnished'}>
                                    <TextField label="Address" value={request.address} onChange={(event) => setRequest({ ...request, address: event.target.value })} fullWidth
                                        style={{ marginTop: "5px", marginBottom: "5px" }} /></Tooltip>
                            </Grid>
                            <Grid item xs={6}>
                                <JurisdictionAutoComplete jurisdictionID={request.jurisdictionID} setJurisdictionID={(id) => { setRequest({ ...request, jurisdictionID: id }) }} />
                            </Grid>
                            {props.corporation || props.airline
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'The Real Life Equivalent, or counterpart to the corporation or airline you are attempting to file. The SDC will look over and verify this information, so be prepared to justify it!'}>
                                            <TextField label="RLE" value={request.rle} onChange={(event) => setRequest({ ...request, rle: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} /></Tooltip>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'The *NET* yearly income of the RLE specified in the last box. Keep the source as it will be useful if questioned by the SDC. A figure before taxes is prefered'}>
                                            <TextField label="RLE Net Yearly" value={request.rleNetYearly} onChange={(event) => setRequest({ ...request, rleNetYearly: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                </> : <></>}
                            {props.airline ? <>
                                <Grid item xs={12}> <Divider /> </Grid>
                                <Grid item xs={4}>
                                    <Tooltip title={'Gates that hold small or ultra small planes'}>
                                        <TextField label="Small gates" value={request.gatesSM} onChange={(event) => setRequest({ ...request, gatesSM: event.target.value })} fullWidth
                                            style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                </Grid>
                                <Grid item xs={4}>
                                    <Tooltip title={'Gates that hold medium planes'}>
                                        <TextField label="Medium gates" value={request.gatesMD} onChange={(event) => setRequest({ ...request, gatesMD: event.target.value })} fullWidth
                                            style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                </Grid>
                                <Grid item xs={4}>
                                    <Tooltip title={'Gates that hold large or ultra-large planes'}>
                                        <TextField label="Large gates" value={request.gatesLG} onChange={(event) => setRequest({ ...request, gatesLG: event.target.value })} fullWidth
                                            style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                </Grid>
                            </> : <></>}
                            {props.corporation || props.airline
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'Any number of *additional* buildings this corporation has'}>
                                            <TextField label="Buildings" value={request.buildings} onChange={(event) => setRequest({ ...request, buildings: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'Number of mergers that this corporation has done'}>
                                            <TextField label="Mergers" value={request.mergers} onChange={(event) => setRequest({ ...request, mergers: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Tooltip title={'Indicates whether or not this corporation or airline has metro ads. Contact the SDC to see how these can be purchased'}>
                                            <FormControlLabel label="Metro Ads" control={
                                                <Checkbox checked={request.metroAds} onClick={() => setRequest({ ...request, metroAds: !request.metroAds })} />}
                                            />
                                        </Tooltip>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Tooltip title={'Indicates whether or not this corporation or airline has airport ads. Contact the SDC to see how these can be purchased'}>
                                            <FormControlLabel label="Airport Ads" control={
                                                <Checkbox checked={request.airportAds} onClick={() => setRequest({ ...request, airportAds: !request.airportAds })} />}
                                            />
                                        </Tooltip>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Tooltip title={'Indicates whether or not this corporation or airline is international. This provides a boost to your income. There is no but. Please click this.'}>
                                            <FormControlLabel label="International" control={
                                                <Checkbox checked={request.international} onClick={() => setRequest({ ...request, international: !request.international })} />}
                                            />
                                        </Tooltip>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Alert severity='warning'>{
                                            !props.item
                                                ? "Your corporation or airline will not immediately generate income, and is subject to approval by the Salary Determination Committee (SDC)"
                                                : "Edits to this corporation or airline will requrie it to be re-approved by the SDC. You will not have income from this item until then"
                                        }</Alert>
                                    </Grid>
                                </> : <></>}
                            {props.business
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Points at which a sale can occur in this business. For most businesses, this represents the number of cash registers. For restaurants, this is the number of chairs'}>
                                            <TextField label="Points of Sale" value={request.pointsOfSale} onChange={(event) => setRequest({ ...request, pointsOfSale: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Average spending at *per point of sale* (IE: The average price of a meal)'}>
                                            <TextField label="Average Spending" value={request.avgSpend} onChange={(event) => setRequest({ ...request, avgSpend: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Customers per hour *per point of sale* (IE: customers a chair at a restaurant sees per hour)'}>
                                            <TextField label="Customers/Hour" value={request.custPerHour} onChange={(event) => setRequest({ ...request, custPerHour: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Hours a day this business is open'}>
                                            <TextField label="Hours Open" value={request.hoursOpen} onChange={(event) => setRequest({ ...request, hoursOpen: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                </> : <></>}
                            {props.hotel
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Amount of standard rooms in this hotel.'}>
                                            <TextField label="Rooms" value={request.rooms} onChange={(event) => setRequest({ ...request, rooms: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Amount of suites in this hotel'}>
                                            <TextField label="Suites" value={request.suites} onChange={(event) => setRequest({ ...request, suites: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Average nightly rate of all standard rooms in this hotel'}>
                                            <TextField label="Room Rate" value={request.roomRate} onChange={(event) => setRequest({ ...request, roomRate: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Average nightly rate of all suites in this hotel'}>
                                            <TextField label="Suite Rate" value={request.suiteRate} onChange={(event) => setRequest({ ...request, suiteRate: event.target.value })} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                </> : <></>}
                            {
                                props.apartment
                                    ? <>
                                        <Grid item xs={12}> <Divider /> </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Amount of studio units in this apartment'}>
                                                <TextField label="Studio Units" value={request.sUnits} onChange={(event) => setRequest({ ...request, sUnits: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Amount of 1 Bedroom units in this apartment'}>
                                                <TextField label="1 Bedroom Units" value={request.b1Units} onChange={(event) => setRequest({ ...request, b1Units: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Amount of 2 Bedroom units in this apartment'}>
                                                <TextField label="2 Bedroom units" value={request.b2Units} onChange={(event) => setRequest({ ...request, b2Units: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Amount of 3 Bedroom units'}>
                                                <TextField label="3 Bedroom units" value={request.b3Units} onChange={(event) => setRequest({ ...request, b3Units: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Amount of 4 Bedroom or Penthouse'}>
                                                <TextField label="Penthouse Units" value={request.pUnits} onChange={(event) => setRequest({ ...request, pUnits: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={12}> <Divider /> </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Average rent for all studio units'}>
                                                <TextField label="Studio Units" value={request.sRent} onChange={(event) => setRequest({ ...request, sRent: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Average rent for all 1 Bedroom units'}>
                                                <TextField label="1 Bedroom Units" value={request.b1Rent} onChange={(event) => setRequest({ ...request, b1Rent: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Average rent for all 2 Bedroom units'}>
                                                <TextField label="2 Bedroom units" value={request.b2Rent} onChange={(event) => setRequest({ ...request, b2Rent: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Average rent of all 3 Bedroom units'}>
                                                <TextField label="3 Bedroom units" value={request.b3Rent} onChange={(event) => setRequest({ ...request, b3Rent: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Average rent of all 4 bedroom or penthouse units'}>
                                                <TextField label="Penthouse Units" value={request.pRent} onChange={(event) => setRequest({ ...request, pRent: event.target.value })} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                    </> : <></>}

                            {!props.corporation && !props.airline ?
                                <Grid item xs={12}>
                                    <Alert severity='warning'>Any item that generates over 500k pecunia/month will require approval from the Salary Determination Committee (SDC)</Alert>
                                </Grid>
                                : <></>}
                            <Grid item xs={12}> <Divider /> </Grid>

                        </Grid>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    {
                        inProgress
                            ? <CircularProgress />
                            : <>
                                <Button onClick={handleOK}>Ok</Button>
                                <Button onClick={handleClosing}>Cancel</Button>
                            </>
                    }
                </DialogActions>

                <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

            </Dialog>

        </>
    )

}