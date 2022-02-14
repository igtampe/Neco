import { CircularProgress, Divider, Grid, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, Checkbox, FormControlLabel } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet, GenerateJSONPut } from "../../RequestOptionGenerator";
import AlertSnackbar from "../AlertSnackbar";
import { ItemTypes } from "./SDCFeedDisplay";

export default function IncomeApprovalForm(props) {

    //We have to do this again que lindo :dancing emoji:
    const DetailsBaseUrl = props.airline
        ? "/API/Income/Airlines"
        : props.corporation
            ? "/API/Income/Corporations"
            : props.business
                ? "/API/Income/Businesses"
                : props.hotel
                    ? "/API/Income/Hotels"
                    : props.apartment
                        ? "/API/Income/Apartments"
                        : undefined

    const ApproveUrl = '/API/Income/SDC/'

    //This massive item will be usable for *ANYTHING*
    const [item, setItem] = useState({
        name: '', description: '', address: '', jurisdictionID: '', accountID: (props.account ? props.account.id : ""), miscIncome: 0, approved: false,
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

    const [changed, setChanged] = useState(false);

    if (props.item && !populated && !loading && props.open) {

        setLoading(true)

        fetch(DetailsBaseUrl + '/' + props.item.id, GenerateGet(props.Session))
            .then(r => r.json()).then(data => {

                if (data.error || data.errors) { return; }

                setLoading(false)

                setItem({ ...item, ...data, jurisdictionID: (data.jurisdiction ? data.jurisdiction.id : '') })
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
        if (changed && props.setItems) { props.setItems(undefined) }
        props.setOpen(false)
    }

    const handleCheck = () => {

        setInProgress(true)

        fetch(ApproveUrl + item.id, GenerateJSONPut(props.Session))
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
                    setItem({ ...item, approved:data.approved });
                    return;
                }
            })

    }

    return (
        <>

            <Dialog maxWidth='sm' scroll='body' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> {item.name} </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        {
                            !populated ? <div style={{textAlign:'center'}}><CircularProgress /></div> : <>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        A(n) <u>{item.international ? 'International ' : ''}{ItemTypes[item.type]}</u> <br/>
                                        Filed by <u>{item.account ? item.account.name + " (" + item.account.id + ")" : ''}</u><br/>
                                        Located in <u>{item.address}, {item.jurisdiction ? item.jurisdiction.name + " (" + item.jurisdiction.id + ")" : ''}</u>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <b>Description:</b> <br/>
                                        {item.description==="" ? <i>No description</i>: item.description}
                                    </Grid>
                                    {props.corporation || props.airline
                                        ? <>
                                            <Grid item xs={12}><Divider /> </Grid>
                                            <Grid item xs={12}>
                                                RLE: <u>{item.rle}</u> making <u>${item.rleNetYearly.toLocaleString()}</u> USD/Year
                                            </Grid>
                                            <Grid item xs={12}> <Divider /> </Grid>
                                        </> : <></>}
                                    {props.airline ? <>
                                        <Grid item xs={12}>
                                            <u>{item.gatesSM}</u> small gate(s), <u>{item.gatesMD}</u> medium gate(s), and <u>{item.gatesLG}</u> large gate(s), 
                                        </Grid>
                                    </> : <></>}
                                    {props.corporation || props.airline
                                        ? <>
                                            <Grid item xs={12}>
                                                <u>{item.buildings}</u> Additional Building(s) and <u>{item.mergers}</u> Merger(s)
                                            </Grid>
                                            <Grid item xs={12}>
                                                Has {!item.metroAds && !item.airportAds ? "no" : <>
                                                    { item.metroAds ? "Metro" : "" }{item.metroAds && item.airportAds ? " and " : ""}{item.airportAds ? "Airport": ""}
                                                </>} Ads
                                            </Grid>
                                        </> : <></>}
                                    {props.business
                                        ? <>
                                            <Grid item xs={12}> <Divider /> </Grid>
                                            <Grid item xs={12}>
                                                <u>{item.pointsOfSale}</u> points of sale serving <u>{item.custPerHour}</u> customer(s) per hour spending <u>~{item.avgSpend}p</u> each
                                            </Grid>
                                            <Grid item xs={12}>
                                                Open <u>{item.hoursOpen}</u> hours a day
                                            </Grid>
                                        </> : <></>}
                                    {props.hotel
                                        ? <>
                                            <Grid item xs={12}>
                                                <u>{item.rooms}</u> room(s) at <u>{item.roomRate.toLocaleString()}</u>p/night and{' '}
                                                <u>{item.suites}</u> suite(s) at <u>{item.suiteRate.toLocaleString()}</u>p/night
                                            </Grid>
                                        </> : <></>}
                                    {
                                        props.apartment
                                            ? <>
                                                <Grid item xs={12}> <Divider /> </Grid>
                                                <Grid item xs={12}>
                                                    {item.sUnits > 0 ? <><u>{item.sUnits}</u> studio unit(s) at <u>{item.sRent.toLocaleString()}</u>p/month<br/></> : '' }
                                                    {item.b1Units > 0 ? <><u>{item.b1Units}</u> 1 Bedroom unit(s) at <u>{item.b1Rent.toLocaleString()}</u>p/month<br/></> : '' }
                                                    {item.b2Units > 0 ? <><u>{item.b2Units}</u> 2 Bedroom unit(s) at <u>{item.b2Rent.toLocaleString()}</u>p/month<br/></> : '' }
                                                    {item.b3Units > 0 ? <><u>{item.b3Units}</u> 3 Bedroom unit(s) at <u>{item.b3Rent.toLocaleString()}</u>p/month<br/></> : '' }
                                                    {item.pUnits > 0 ? <><u>{item.pUnits}</u> 4 Bedroom or Penthouse unit(s) at <u>{item.pRent.toLocaleString()}</u>p/month<br/></> : '' }
                                                </Grid>
                                            </> : <></>}
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={3}> <b>Total Income:</b></Grid>
                                    <Grid item xs={9}> {item.calculatedIncome.toLocaleString()}p/month</Grid>
                                    <Grid item xs={12}> <Divider /> </Grid>
                                    <Grid item xs={3}>
                                        {inProgress 
                                            ? <div style={{textAlign:'center'}}><CircularProgress /></div>
                                            : <FormControlLabel label="Approved" control={<Checkbox checked={item.approved} onChange={handleCheck} />}/>
                                        }
                                    </Grid>
                                    <Grid item xs={9}>
                                        By approving or unapproving this item, you certify that this item has passed, or failed the checks of the SDC, and that you have been authorized to commit this action by the rest of the committee
                                    </Grid>
                                </Grid>
                            </>
                        }
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button disbaled={inProgress} onClick={handleClosing}>OK</Button>
                </DialogActions>

                <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

            </Dialog>

        </>
    )

}