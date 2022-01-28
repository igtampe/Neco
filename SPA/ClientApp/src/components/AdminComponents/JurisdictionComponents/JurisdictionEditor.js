import React, { useState } from "react";
import {
    Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, FormControl, Grid,
    InputLabel, MenuItem, Select, TextField
} from "@mui/material";
import AlertSnackbar from "../../AlertSnackbar";
import PicturePicker from "../../PicturePicker";
import { CircularProgress } from "@mui/material";
import { GenerateGet } from "../../../RequestOptionGenerator";
import { JurisdictionTypeSelect } from "./JurisdictionDisplay";
import BracketDisplay from "./BracketDisplay";

//Requires jurisdictions, setJurisdictions, type, jurisdiction, setJurisdiction.
export function JurisdictionSelect(props) {

    const [loading, setLoading] = useState(false);

    if (!props.jurisdictions && props.setJurisdictions && !loading && props.type && props.type !== 0) {
        //if we don't have jurisdictions and we can set jurisdictions and we're not loading
        //AND we have a type *A N D* type is not 0

        setLoading(true)

        var URL = '/API/Taxes/Jurisdictions?Type=' + (props.type-1)

        fetch(URL, GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
            .then(response => { return (response.json()) })
            .then(data => {

                //if there was an error then oops
                if (data.error) { return; }

                props.setJurisdictions(data)
                setLoading(false)
            })

    }

    if(props.type===0){return (<></>)}
    if(!props.jurisdictions){ return(<CircularProgress/>) }

    return (
        <>
            <FormControl fullWidth style={{ margintop: "15px" }} disabled={props.type===0}>
                <InputLabel fullWidth>Parent</InputLabel>
                <Select fullWidth label="Label" value={props.Jurisdiction} onChange={(event) => { props.setJurisdictionID(event.target.value) }}>
                    { props.jurisdictions.map( j=>{ return( <MenuItem value={j.id}>{j.id}: {j.name}</MenuItem> )}) }
                </Select></FormControl>
        </>

    )

}

export default function JurisdictionEditor(props) {

    const [name, setName] = useState("");
    const [imageURL, setImageURL] = useState("");

    const [parentID, setParentID] = useState("");
    const [accountID, setAccountID] = useState("");
    const [population, setPopulation] = useState(0);

    const [type, setType] = useState(0);
    const [jurisdictions, setJurisdictions] = useState(undefined)

    const [pickerOpen, setPickerOpen] = useState(false)

    const [populated, setPopulated] = useState(false);

    const [result, setResult] = useState({
        severity: "success",
        text: "idk"
    })

    const [SnackOpen, setSnackOpen] = useState(false);

    if (props.jurisdiction && !populated && props.open) {
        //Populate
        setPopulated(true);
        setName(props.jurisdiction.name)
        setImageURL(props.jurisdiction.imageURL)
        setType(props.jurisdiction.type)    
        setPopulation(props.jurisdiction.population)
        if (props.jurisdiction.parentJurisdiction) { setParentID(props.jurisdiction.parentJurisdiction.id) }
    }

    const ClearForm = () => {
        setName("")
        setImageURL("")
        setType(0)
        setPopulation(0)

        setPopulated(false)
    }

    const handleClosing = (event) => {

        props.setOpen(false)
        ClearForm();
    }

    const handleOK = (event) => {

        if (name === "") {
            setResult({ severity: "danger", text: "Name cannot be empty!" })
            setSnackOpen(true);
        }

        if (parentID === "" && type !== 0) {
            setResult({ severity: "danger", text: "Must set a parent for non-country jurisdiction" })
            setSnackOpen(true);
        }

        //Send the request 
        var requestOptions;
        var url;

        if (props.jurisdiction) {
            requestOptions = { method: 'PUT' };
            url = "API/Taxes/Jurisdiction/" + props.jurisdiction.id
        } else {
            requestOptions = { method: 'POST', };
            url = "API/Taxes/Jurisdiction"
        }

        requestOptions = {
            ...requestOptions,
            headers: { 'Content-Type': 'application/json', 'SessionID': props.Session },
            body: JSON.stringify({
                type: type, name: name, population: population,
                imageURL: imageURL, parentJurisdictionID: parentID, 
                tiedAccountID:accountID
            })
        }
        
        console.log(requestOptions.body)

        fetch(url,requestOptions)
            .then(response => { return response.json() })
            .then(data => {
                if (data.error) {

                    setResult({
                        severity: "danger",
                        text: "Could not " + (props.jurisdiction ? "update" : "create") + " this jurisdiction: " + data.reason
                    })

                    setSnackOpen(true);

                } else {
                    setResult({
                        severity: "success",
                        text: name + " has been " + (props.jurisdiction ? "updated" : "created") + "!"
                    })

                    if (props.setJurisdictions) { props.setJurisdictions(undefined) }
                    if (props.setJurisdiction) { props.setJurisdiction(data) }

                    setSnackOpen(true);

                    handleClosing();
                }
            })

    }

    return (
        <React.Fragment>
            <Dialog fullWidth maxWidth="md" open={props.open} onClose={handleClosing}>
                <DialogTitle>{props.jurisdiction ? 'Edit a Jurisdiction' : 'Create a new Jurisdiction'}</DialogTitle>
                <DialogContent>
                    <table style={{ width: "100%" }}>
                        <tr>
                            <td>
                                <TextField label="Name" value={name} onChange={(event) => setName(event.target.value)} fullWidth
                                    style={{ marginTop: "5px", marginBottom: "5px" }} />                            </td>
                            <td rowSpan="2" style={{ width: "135px" }}>
                                <Button onClick={() => setPickerOpen(true)}>
                                    <img src={imageURL === "" ? "/flag.png" : imageURL} alt="Flag" width="100px" style={{ marginLeft: "25px", marginRight: "10px" }} />
                                </Button>
                            </td>
                        </tr>
                    </table>
                    <Grid container spacing={2}>
                        <Grid item xs={6}>
                            <TextField label="Population" value={population} onChange={(event) => setPopulation(event.target.value)} fullWidth
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField label="Tied Neco Bank Account" value={accountID} onChange={(event) => setAccountID(event.target.value)} fullWidth
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={6}>
                            <JurisdictionTypeSelect type={type} setType={setType} setCollection={setJurisdictions} />
                        </Grid>
                        <Grid item xs={6}>
                            <JurisdictionSelect type={type} setJurisdictionID={setParentID} jurisdictions={jurisdictions} setJurisdictions={setJurisdictions}
                            Jurisdiction={parentID}/>
                        </Grid>
                    </Grid>

                    {
                        !props.jurisdiction ? <></> : <>
                            <Divider style={{marginTop:'15px', marginBottom:'15px'}}/>    
                            <BracketDisplay JurisdictionID={props.jurisdiction.id} Session={props.Session}/>
                        
                        </>
                    }
                    

                </DialogContent>
                <DialogActions>
                    <Button onClick={handleOK}>OK</Button>
                    <Button onClick={handleClosing}>Cancel</Button>
                </DialogActions>
            </Dialog>

            <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

            <PicturePicker open={pickerOpen} setOpen={setPickerOpen} imageURL={imageURL} setImageURL={setImageURL} defaultImage={"/flag.png"} />

        </React.Fragment>
    );

}
