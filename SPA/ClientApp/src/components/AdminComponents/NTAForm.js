import React, { useState } from "react";
import {
    Alert,
    Button, CircularProgress, Container, Grid, TextField, Typography
} from "@mui/material";

export default function NTAForm(props) {

    const [id, setID] = useState("")
    const [amount, setAmount] = useState(0);

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const [sendInProgress, setSendInProgress] = useState(false);


    const handleOK = (event) => {

        setSnackOpen(false);        
        setSendInProgress(true)

        if (id === "") {
            setResult({ severity: "error", text: "ID cannot be empty" })
            setSnackOpen(true);
            return;
        }

        //Send the request 
        var requestOptions = { method: 'PUT', headers: { 'Content-Type': 'application/json', 'SessionID': props.Session } };
        var url = "API/Bank/Accounts/" + id + '/NTA?Amount=' + amount;

        fetch(url, requestOptions)
            .then(response => { return response.json() })
            .then(data => {
                setSendInProgress(false);
                if (data.error) {

                    setResult({ severity: "error", text: "Could not NTA: " + data.reason })
                    setSnackOpen(true);

                } else {
                    setResult({
                        severity: "success",
                        text: amount.toLocaleString() + "p has been deposited to " + id + "!"
                    })
                    setSnackOpen(true);

                }
            })

    }

    return (
        <React.Fragment>

            <Grid container spacing={0} direction="column" alignItems="center" justifyContent="center">
                <Grid item xs={2}>
                    
                <Container style={{paddingLeft:'25%', paddingRight:'25%', paddingTop:'25px'}}>
                    <Typography>
                    <Typography variant="h5" style={{ fontFamily: "Outfit", textAlign: "center"}}> NON-TAXED ADD</Typography><br/>
                    <Typography style={{color:'red'}}><b>WARNING:</b></Typography><br/>
                    The following utility is given to manually adjust the balance of a Neco bank account by the specified positive <i>or negative</i> amount. Please use it responsibly.
                    Don't even think of giving yourself more money. Or do. I'm just some text on the frontend. I can't do anything. <br/><br/>
                    <Grid container spacing={2}>
                        <Grid item xs={6}>
                            <TextField label="Neco Bank Account ID" value={id} onChange={(event)=> setID(event.target.value)} fullWidth 
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField label="Amount" value={amount} onChange={(event)=> setAmount(event.target.value)} type='number' fullWidth
                                    style={{ marginTop: "5px", marginBottom: "5px" }} /><br />
                        </Grid>
                    </Grid>
                    <br />
                </Typography>
                <div style={{ textAlign: 'center' }}>
                    {sendInProgress ? <CircularProgress /> : <>
                        <Button variant='contained' color='primary' disabled={sendInProgress} onClick={handleOK}
                            style={{ margin: "10px" }}> SEND THE MONEY </Button></>}
                </div>

                    <Alert severity={result.severity} hidden={!SnackOpen} style={{marginTop:'20px'}}> {result.text} </Alert>

                </Container>
                </Grid>
            </Grid>

        </React.Fragment>
    );

}
