import { TextField, CircularProgress, Divider, Grid, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, Checkbox, FormControlLabel, Tooltip, Alert } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet, GenerateJSONPut } from "../../RequestOptionGenerator";
import { JurisdictionAutoComplete } from "../AdminComponents/JurisdictionComponents/JurisdictionAutocomplete";
import AlertSnackbar from "../AlertSnackbar";

export default function IncomeApprovalForm(props) {

    //AGAIN AGAIN!!!!!!!!!!!!!!!!
    const DetailsBaseUrl = props.item.type === 4 
        ? "/API/Income/Airlines "
        : props.item.type === 3
            ? "/API/Income/Corporations"
            : props.item.type === 2
                ? "/API/Income/Businesses"
                : props.item.type === 1
                    ? "/API/Income/Hotels"
                    : props.item.type === 0
                        ? "/API/Income/Apartments"
                        : undefined

    const ApproveUrl = '/API/Income/SDC/'

    //This massive item will be usable for *ANYTHING*
    const [item, setItem] = useState({
        name: '', description: '', address: '', jurisdictionID: '', accountID: (props.account ? props.account.id : ""), miscIncome: 0, approved:false,
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
    const [loading, setLoading] = useState(false);

    const [changed,setChanged] = useState(false);

    if (props.item && !populated && !loading && props.open) {
        
        setLoading(true)

        fetch(DetailsBaseUrl+'/'+ props.item.id,GenerateGet(props.Session))
        .then(r=>r.json()).then(data=>{

            if(data.error || data.errors) {return;}

            setLoading(false)

            setItem({ ...item, ...data })
            setPopulated(true);

        })

    }

    const clearForm = () => {
        setItem({
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
        if(changed && props.setItems) {props.setItems(undefined)}
        props.setOpen(false)
    }

    const handleCheck = () => {

        setInProgress(true)

        fetch(ApproveUrl, GenerateJSONPut(props.Session))
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
                    setResult({ severity: 'success', text: 'Item has been ' + (data.approved ? '' : 'un') + 'approved' })
                    setSnackOpen(true)
                    setChanged(!changed)
                    setItem({...item},data);
                    return;
                }
            })

    }

    return (
        <>

            <Dialog maxWidth='sm' scroll='body' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> Item details </DialogTitle>
                <DialogContent>
                    <DialogContentText>

                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField label="Name" value={item.name} fullWidth
                                    style={{ marginTop: "5px", marginBottom: "5px" }} />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField label="Description" value={item.description} fullWidth
                                    multiline maxRows={4} minRows={4} variant='filled' style={{ marginTop: "5px", marginBottom: "5px" }} />
                            </Grid>
                            <Grid item xs={12}> <Divider /> </Grid>
                            <Grid item xs={6}>
                                <Tooltip title={'Address of the main HQ of this item. This location must be furnished'}>
                                    <TextField label="Address" value={item.address} fullWidth
                                        style={{ marginTop: "5px", marginBottom: "5px" }} /></Tooltip>
                            </Grid>
                            <Grid item xs={6}>
                                <JurisdictionAutoComplete jurisdictionID={item.jurisdictionID} />
                            </Grid>
                            {props.corporation || props.airline
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'The Real Life Equivalent, or counterpart to the corporation or airline you are attempting to file. The SDC will look over and verify this information, so be prepared to justify it!'}>
                                            <TextField label="RLE" value={item.rle} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} /></Tooltip>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'The *NET* yearly income of the RLE specified in the last box. Keep the source as it will be useful if questioned by the SDC. A figure before taxes is prefered'}>
                                            <TextField label="RLE Net Yearly" value={item.rleNetYearly} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                </> : <></>}
                            {props.airline ? <>
                                <Grid item xs={12}> <Divider /> </Grid>
                                <Grid item xs={4}>
                                    <Tooltip title={'Gates that hold small or ultra small planes'}>
                                        <TextField label="Small gates" value={item.gatesSM} fullWidth
                                            style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                </Grid>
                                <Grid item xs={4}>
                                    <Tooltip title={'Gates that hold medium planes'}>
                                        <TextField label="Medium gates" value={item.gatesMD} fullWidth
                                            style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                </Grid>
                                <Grid item xs={4}>
                                    <Tooltip title={'Gates that hold large or ultra-large planes'}>
                                        <TextField label="Large gates" value={item.gatesLG} fullWidth
                                            style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                </Grid>
                            </> : <></>}
                            {props.corporation || props.airline
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'Any number of *additional* buildings this corporation has'}>
                                            <TextField label="Buildings" value={item.buildings} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <Tooltip title={'Number of mergers that this corporation has done'}>
                                            <TextField label="Mergers" value={item.mergers} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Tooltip title={'Indicates whether or not this corporation or airline has metro ads. Contact the SDC to see how these can be purchased'}>
                                            <FormControlLabel label="Metro Ads" control={
                                                <Checkbox checked={item.metroAds}/>}
                                            />
                                        </Tooltip>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Tooltip title={'Indicates whether or not this corporation or airline has airport ads. Contact the SDC to see how these can be purchased'}>
                                            <FormControlLabel label="Airport Ads" control={
                                                <Checkbox checked={item.airportAds}/>}
                                            />
                                        </Tooltip>
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Tooltip title={'Indicates whether or not this corporation or airline is international. This provides a boost to your income. There is no but. Please click this.'}>
                                            <FormControlLabel label="International" control={
                                                <Checkbox checked={item.international}/>}
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
                                            <TextField label="Points of Sale" value={item.pointsOfSale} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Average spending at *per point of sale* (IE: The average price of a meal)'}>
                                            <TextField label="Average Spending" value={item.avgSpend} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Customers per hour *per point of sale* (IE: customers a chair at a restaurant sees per hour)'}>
                                            <TextField label="Customers/Hour" value={item.custPerHour} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Hours a day this business is open'}>
                                            <TextField label="Hours Open" value={item.hoursOpen} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                </> : <></>}
                            {props.hotel
                                ? <>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Amount of standard rooms in this hotel.'}>
                                            <TextField label="Rooms" value={item.rooms} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Amount of suites in this hotel'}>
                                            <TextField label="Suites" value={item.suites} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Average nightly rate of all standard rooms in this hotel'}>
                                            <TextField label="Room Rate" value={item.roomRate} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Tooltip title={'Average nightly rate of all suites in this hotel'}>
                                            <TextField label="Suite Rate" value={item.suiteRate} fullWidth
                                                style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                    </Grid>
                                </> : <></>}
                            {
                                props.apartment
                                    ? <>
                                        <Grid item xs={12}> <Divider /> </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Amount of studio units in this apartment'}>
                                                <TextField label="Studio Units" value={item.sUnits} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Amount of 1 Bedroom units in this apartment'}>
                                                <TextField label="1 Bedroom Units" value={item.b1Units} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Amount of 2 Bedroom units in this apartment'}>
                                                <TextField label="2 Bedroom units" value={item.b2Units} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Amount of 3 Bedroom units'}>
                                                <TextField label="3 Bedroom units" value={item.b3Units} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Amount of 4 Bedroom or Penthouse'}>
                                                <TextField label="Penthouse Units" value={item.pUnits} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={12}> <Divider /> </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Average rent for all studio units'}>
                                                <TextField label="Studio Units" value={item.sRent} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Average rent for all 1 Bedroom units'}>
                                                <TextField label="1 Bedroom Units" value={item.b1Rent} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <Tooltip title={'Average rent for all 2 Bedroom units'}>
                                                <TextField label="2 Bedroom units" value={item.b2Rent} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Average rent of all 3 Bedroom units'}>
                                                <TextField label="3 Bedroom units" value={item.b3Rent} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Tooltip title={'Average rent of all 4 bedroom or penthouse units'}>
                                                <TextField label="Penthouse Units" value={item.pRent} fullWidth
                                                    style={{ marginTop: "5px", marginBottom: "5px" }} type='number' /></Tooltip>
                                        </Grid>
                                    </> : <></>}
                            <Grid item xs={12}> <Divider /> </Grid>

                        </Grid>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    {
                        inProgress
                            ? <CircularProgress />
                            : <>
                                <Button onClick={handleClosing}>OK</Button>
                            </>
                    }
                </DialogActions>

                <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

            </Dialog>

        </>
    )

}