import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { TextField, Button, Typography, CircularProgress, Container } from '@mui/material';
import Cookies from 'universal-cookie';
import AlertSnackbar from "../AlertSnackbar";
import { APIURL } from "../../App";

// react.school/material-ui


const cookies = new Cookies();

export default function LoginForm(props) {
    const history = useHistory();

    const [Pin, SetPin] = useState("");
    const [ID, SetID] = useState("");
    const [LoginInProgress, SetLoginInProgress] = useState(false);
    const [ResultOpen, setResultOpen] = useState(false);
    const [Result, setResult] = useState({
        text: "Desconozco",
        severity: "danger"
    });

    const handleIDChange = (event) => { SetID(event.target.value); };
    const handlePinChange = (event) => { SetPin(event.target.value); };

    const OnLoginButtonClick = (event) => {
        SetLoginInProgress(true);

        //Grab the ID and pin and create a tiny itty bitty object
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ "id": ID, "password": Pin })
        };


        fetch(APIURL + "/API/Users", requestOptions)
            .then(response => {
                SetLoginInProgress(false);
                return response.text(); 
            }).then(data => {
                if (!data.includes("-")) {
                    setResult({  severity: "error", text: data });
                    setResultOpen(true)
                } else {

                    //Remove the quotes around this
                    data=data.substring(1,data.length-1);

                    //We logged in, save a cookie, then let's get the heck out of here
                    cookies.set('SessionID', data, { path: '/', maxAge: 60 * 60 * 24 }) //The cookie will expire in a day
                    history.go();
                }
            })
    }

    return (
        <React.Fragment>
            <Container style={props.DarkMode ? { backgroundColor: '#444444', padding: '50px' } : { backgroundColor: '#ebebeb', padding: '50px' }}>
                <Typography>
                    <Typography variant="h6"
                        style={{ fontFamily: "Outfit", textAlign: "center" }}>
                        <img src={props.DarkMode ? "NecoWhite.png" : "NecoBlack.png"} alt="Logo" width="200" /><br />Welcome to neco<br /><br />
                    </Typography>
                    <TextField label="Neco ID" value={ID} onChange={handleIDChange} fullWidth
                        style={{ marginTop: "5px", marginBottom: "5px" }} /><br />
                    <TextField label="Password" value={Pin} type="password" onChange={handlePinChange} fullWidth
                        style={{ marginTop: "5px", marginBottom: "5px" }} /><br />

                    <br />
                </Typography>
                <div style={{ textAlign: 'center' }}>
                    {LoginInProgress ? <CircularProgress /> : <>
                        <Button variant='contained' color='primary' disabled={LoginInProgress} onClick={OnLoginButtonClick}
                            style={{ margin: "10px" }}> Log In </Button></>}
                </div>
            </Container>

            <AlertSnackbar open={ResultOpen} setOpen={setResultOpen} result={Result}/>

        </React.Fragment>
    );

}
