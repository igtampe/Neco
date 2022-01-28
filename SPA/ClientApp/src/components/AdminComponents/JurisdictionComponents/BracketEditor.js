import React, { useState } from "react";
import {
    Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, FormControl, Grid,
    InputLabel, MenuItem, Select, TextField
} from "@mui/material";
import AlertSnackbar from "../../AlertSnackbar";
import { CircularProgress } from "@mui/material";
import { GenerateGet } from "../../../RequestOptionGenerator";
import BracketDisplay from "./BracketDisplay";

export function IncomeTypeSelect(props) {
    return (
        <FormControl fullWidth style={{ margintop: "15px" }}>
            <InputLabel fullWidth>Type</InputLabel>
            <Select fullWidth label="Label" value={props.type} onChange={(event) => { props.setType(event.target.value) }}>
                <MenuItem value={0}>Personal</MenuItem>
                <MenuItem value={1}>Corporate</MenuItem>
                {/**<MenuItem value={2}>Government</MenuItem>
                <MenuItem value={3}>Charity</MenuItem>
                These income types pay no taxes
                **/}
            </Select></FormControl>
    )

}


export default function BracketEditor(props) {

    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [rate, setRate] = useState(0.0)
    const [incomeType, setIncomeType] = useState(0)
    const [start, setStart] = useState(0)
    const [end, setEnd] = useState(0)

    const [populated, setPopulated] = useState(false);

    const [result, setResult] = useState({
        severity: "success",
        text: "idk"
    })

    const [SnackOpen, setSnackOpen] = useState(false);

    if (props.bracket && !populated && props.open) {
        //Populate
        setPopulated(true);
        setName(props.bracket.name)
        setDescription(props.bracket.description)
        setRate(props.bracket.rate * 100)
        setIncomeType(props.bracket.incomeType)
        setStart(props.bracket.start)
        setEnd(props.bracket.end)
    }

    const ClearForm = () => {
        setName("")
        setDescription("")
        setRate(0)
        setIncomeType(0)
        setStart(0)
        setEnd(0)

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
            return;
        }

        //Send the request 
        var requestOptions;
        var url;

        if (props.bracket) {
            requestOptions = { method: 'PUT' };
            url = "API/Taxes/brackets/" + props.bracket.id
        } else {
            requestOptions = { method: 'POST', };
            url = "API/Taxes/Brackets?Jurisdiction=" + props.JurisdictionID
        }

        requestOptions = {
            ...requestOptions,
            headers: { 'Content-Type': 'application/json', 'SessionID': props.Session },
            body: JSON.stringify({
                ...props.bracket,
                name: name, description: description,
                rate: (rate/100.0), incomeType:incomeType,
                start: start, end: end
            })
        }

        console.log(requestOptions.body)

        fetch(url, requestOptions)
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

                    if (props.setBrackets) { props.setBrackets(undefined) }
                    if (props.setBracket)  { props.setBracket(data) }

                    setSnackOpen(true);

                    handleClosing();
                }
            })

    }

    return (
        <React.Fragment>
            <Dialog fullWidth maxWidth="sm" open={props.open} onClose={handleClosing}>
                <DialogTitle>{props.bracket ? 'Edit a Bracket' : 'Create a new Bracket'}</DialogTitle>
                <DialogContent>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField label="Name" value={name} onChange={(event) => setName(event.target.value)} fullWidth
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={6}>
                            <IncomeTypeSelect type={incomeType} setType={setIncomeType} />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField label="Rate Percentage" value={rate} onChange={(event) => setRate(event.target.value)} fullWidth type='number'
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField label="Start Range" value={start} onChange={(event) => setStart(event.target.value)} fullWidth type='number'
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField label="End Range" value={end} onChange={(event) => setEnd(event.target.value)} fullWidth type='number'
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField label="Description/Notes" value={description} onChange={(event) => setDescription(event.target.value)} fullWidth multiline
                                style={{ marginTop: "5px", marginBottom: "5px" }} maxRows={6} minRows={6} variant="filled" color="secondary" />
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleOK}>OK</Button>
                    <Button onClick={handleClosing}>Cancel</Button>
                </DialogActions>
                <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />
            </Dialog>

        </React.Fragment>
    );

}
