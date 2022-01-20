import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { TextField, Button, Typography, CircularProgress, Container, Dialog, DialogTitle, DialogContent, DialogActions, Card, CardContent, Divider } from '@mui/material';
import AlertSnackbar from "../AlertSnackbar";

// react.school/material-ui



const TandcCard = (props) => {
return (
    <Card sx={{ minWidth: '100%'}}>
      <CardContent>
        <Typography gutterBottom> {props.title} </Typography>
        <Divider/>
        <div style={{minWidth:'100%', maxHeight:'200px', overflowY:'auto'}} > {props.text}</div>
      </CardContent>
    </Card>
)
}

const UMSWEBTandc = <>
The UMSWEB is designed to be used by citizens of the UMS (Ultra Modern Sector). Any other activity will be illegal under UMS Law. Under this system you can:<br/>
<br/>
 [1.] Pay taxes<br/>
 [2.] Manage your monets<br/>
 [5.] More amazing stuff<br/>
 <br/>
The UMSWEB is a program presented as is. The UMS Is not responsible for any problems that occur here-on out. Legal mumbo jumbo goes here and very big words like taxes and regulatory bodies 
and federal prosecution along with penal code 1422 and law 14. All transactions will be logged and notified to the UMS Government. The UMS Reserves the right to use any and all 
information provided. Usage of this system implies acceptance. The UMS is (C)2019 Nexus LLC<br/>
</>

const ViBETandc = <>
By using this product you agree to all terms set in this doucment. Please read them. Its super short so please. If you don't agree to them then just don't use ViBE. Simple.<br/>
<br/>
A. By using this product you agree to have Igtampe not be responsible for anything it does. It's provided AS IS. I will help recover what I can but I make NO GUARANTEES.<br/>
<br/>
B. Do not send money to the Lemon. Your assets will be frozen if you do.<br/>
<br/>
C. This program is not for resale, it's a free utility.<br/>
<br/>
e s o   e s   t o d o<br/>
-IGT
</>

const NecoTandc = <>
By using Neco, you agree to the following terms and conditions as set forth by this itty bitty piece of paper that you will surely read over because there's three terms and
conditions for this app like my god what am I even doing anymore.<br/>
<br/>
[1.] As Neco is a heavily inspired rework of ViBE, therefore I find it fitting to apply ViBE's terms of service as well<br/><br/>
[2.] As ViBE was subject to the UMSWEB Terms and conditions, use of this product is also subject to the UMSWEB Terms and conditions, if and only if one or more of the following apply to you:<br/>
<br/>
[a] You plan to use this program for financial transactions of the UMS Pecunia (P),<br/>
[b] You plan to use this program to open, maintain, or close a bank account in a UMS Based bank<br/>
[c] You plan to use this program to receive income from any UMS based legal entity or employment.<br/>
    <br/>
In addition, Neco's administration (IE: Chopo) reserves the right to delete, reset, or modify any account balances due to any actions you commit. You will be notified if such changes are made.<br/>
<br/>
Thank you for using Neco<br/>
-IGT
</>


export default function RegisterForm(props) {
    const history = useHistory();

    const [Name, SetName] = useState("");
    const [Pin, SetPin] = useState("");
    const [LoginInProgress, SetLoginInProgress] = useState(false);
    
    const [ResultOpen, setResultOpen] = useState(false);
    const [Result, setResult] = useState({ text: "Desconozco", severity: "danger" });

    const [WelcomeDialog, setWelcomeDialog] = useState(false);
    const [newUser, setNewUser] = useState(
        {
            "name": "Test",
            "imageURL": "",
            "isAdmin": false,
            "isGov": false,
            "isSdc": false,
            "isUploader": false,
            "id": "f0191ffa-de09-473b-b3eb-50a4beabf94c",
            "idGenerator": {},
            "id": "12633"
          }
    )

    const [tandc, setTandc] = useState(false)

    const handleIDChange = (event) => { SetName(event.target.value); };
    const handlePinChange = (event) => { SetPin(event.target.value); };

    const OnLoginButtonClick = (event) => {
        SetLoginInProgress(true);
        setTandc(false);

        //Grab the ID and pin and create a tiny itty bitty object
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ "name": Name, "password": Pin })
        };


        fetch("API/Users/register", requestOptions)
            .then(response => {
                SetLoginInProgress(false);
                return response.json(); 
            }).then(data => {
                if (data.error) {
                    setResult({  severity: "error", text: data.reason });
                    setResultOpen(true)
                } else {
                    setNewUser(data)
                    setWelcomeDialog(true)
                }
            })
    }

    const SendToLogin = () => {
        setWelcomeDialog(false)
        history.push('/login')
    }

    return (
        <React.Fragment>
            <Container style={props.DarkMode ? { backgroundColor: '#444444', padding: '50px' } : { backgroundColor: '#ebebeb', padding: '50px' }}>
                <Typography>
                    <Typography variant="h6"
                        style={{ fontFamily: "Outfit", textAlign: "center" }}>
                        <img src={props.DarkMode ? "NecoWhite.png" : "NecoBlack.png"} alt="Logo" width="200" /><br />Register to neco<br /><br />
                    </Typography>
                    <TextField label="Name" value={Name} onChange={handleIDChange} fullWidth
                        style={{ marginTop: "5px", marginBottom: "5px" }} /><br />
                    <TextField label="Password" value={Pin} type="password" onChange={handlePinChange} fullWidth
                        style={{ marginTop: "5px", marginBottom: "5px" }} /><br />

                    <br />
                </Typography>
                <div style={{ textAlign: 'center' }}>
                    {LoginInProgress ? <CircularProgress /> : <>
                        <Button variant='contained' color='primary' disabled={LoginInProgress} onClick={()=>{setTandc(true)}}
                            style={{ margin: "10px" }}> Register </Button></>}
                </div>
            </Container>

            <AlertSnackbar open={ResultOpen} setOpen={setResultOpen} result={Result}/>

            <Dialog maxWidth='sm' fullWidth open={tandc} onClose={()=>{setTandc(false)}}>
                <DialogTitle>Terms and Conditions</DialogTitle>
                <DialogContent>
                    <div style={{textAlign:'center'}}><b>By hitting "Register" you agree to these terms <br/><br/></b></div>
                    <TandcCard title={'Neco Terms and Conditions'} text={NecoTandc}/><br/>
                    <TandcCard title={'ViBE Terms and Conditions'} text={ViBETandc}/><br/>
                    <TandcCard title={'UMSWEB Terms and Conditions'} text={UMSWEBTandc}/>
                </DialogContent>
                <DialogActions>
                    <Button color='success' onClick={OnLoginButtonClick}> Register </Button>
                    <Button onClick={()=>{setTandc(false)}}> Cancel </Button>
                </DialogActions>
            </Dialog>

            <Dialog maxWidth='sm' fullWidth open={WelcomeDialog}>
                <DialogTitle style={{textAlign:'center', fontFamily:'outfit', fontSize:'25px'}}>Welcome to Neco, {newUser.name}!</DialogTitle>
                <DialogContent style={{fontFamily:'outfit'}}>
                        <div style={{textAlign:'Center'}}>
                        Your NecoID is:
                        <Typography sx={{fontSize:'40px', fontFamily:'monospace'}}> {newUser.id} </Typography>
                        Please make sure not to forget this, as this is the ID you use to sign in!<br/><br/>
                        </div>
                        <Divider/>
                        <br/>
                        
                        If you would like your ID changed or to gain any roles (such as Image Uploader for a PFP), please contact a Neco Administrator.<br/>
                        <br/>
                        Again, Welcome to Neco! <br/>
                        -IGT

                </DialogContent>
                <DialogActions>
                    <Button onClick={SendToLogin} color='success'> Login </Button>
                </DialogActions>
            </Dialog>

        </React.Fragment>
    );

}
