import { Card, CardContent, CircularProgress, Divider, Grid, List, ListItem, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from "@mui/material";
import React, { useState } from "react";
import AccountDisplay from "./AccountComponents/AccountDisplay";
import AccountForm from "./AccountComponents/AccountForm";
import SendMonet from "./AccountComponents/SendMonet";
import NecoHeader from "./NecoHeader";

export function ComingSoonDialog(props){

    const handleClosing = () => { props.setOpen(false) }

return(
    <>
    
    <Dialog open={props.open} onClose={handleClosing}>
        <DialogTitle>
            Coming Soon!
        </DialogTitle>
        <DialogContent>
            <DialogContentText>
                This feature is not yet implemented!
            </DialogContentText>
        </DialogContent>
        <DialogActions>
            <Button onClick={handleClosing}>Ok</Button>
        </DialogActions>
    </Dialog>

    </>
)

}

export default function AccountsComponent(props) {

    const [accounts, setAccounts] = useState(undefined);
    const [sendMoneyOpen, setSendMoneyOpen] = useState(false)

    const [CSOpen,setCSOpen] = useState(false);
    const [newOpen, setNewOpen] = useState(false);    

    if(!props.User) {return (<CircularProgress/>)}

    console.log(props)

    return (
        <React.Fragment>
            <NecoHeader name={props.User ? props.User.name : '...'} />
            <Grid container spacing={2} style={{ minWidth: '100%' }}>
                <Grid item xs={props.Vertical ? 12 : 9}>
                    <AccountDisplay accounts={accounts} setAccounts={setAccounts} {...props}/>
                </Grid>
                <Grid item xs={props.Vertical ? 12 : 3}>
                    <Card sx={{ minWidth: '100%', maxHeight:'100%', height:(props.Vertical ? 'auto': '60vh') }}>
                        <CardContent>
                            <b>Quick actions</b>
                            <Divider />
                            <List>
                                <ListItem button onClick={()=>{setNewOpen(true)}}> Open an Account </ListItem>
                                <ListItem button onClick={()=>{setSendMoneyOpen(true)}}> Send Money </ListItem>
                                <ListItem button onClick={()=>{setCSOpen(true)}}> Send a Check </ListItem>
                                <ListItem button onClick={()=>{setCSOpen(true)}}> Send a Bill </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>

            <AccountForm open={newOpen} setOpen={setNewOpen} setAccounts={setAccounts} {...props}/>
            <SendMonet {...props} open={sendMoneyOpen} setOpen={setSendMoneyOpen}/>

            <ComingSoonDialog open={CSOpen} setOpen={setCSOpen}/>

        </React.Fragment>
    );

}
