import { Card, CardContent, CircularProgress, Divider, Grid, List, ListItem, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from "@mui/material";
import React, { useState } from "react";
import AccountForm from "./AccountComponents/AccountForm";
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

    const [CSOpen,setCSOpen] = useState(false);
    const [newOpen, setNewOpen] = useState(false);    

    if(!props.User) {return (<CircularProgress/>)}

    console.log(props)

    return (
        <React.Fragment>
            <NecoHeader name={props.User ? props.User.name : '...'} />
            <Grid container spacing={2} style={{ minWidth: '100%' }}>
                <Grid item xs={9}>

                    Accordion for each account that in the summary has Bank name, Account Name, Balance
                    and in the details includes 3 latest transactions and buttons to:

                    Menu with:
                        see more transactions
                        Manage owners
                        Close account
                    
                    Send Money (big)

                </Grid>
                <Grid item xs={3}>
                    <Card sx={{ minWidth: '100%', maxHeight:'100%', height:'60vh' }}>
                        <CardContent>
                            <b>Quick actions</b>
                            <Divider />
                            <List>
                                <ListItem button onClick={()=>{setNewOpen(true)}}> Add an Account </ListItem>
                                <ListItem button onClick={()=>{setCSOpen(true)}}> Send a Check </ListItem>
                                <ListItem button onClick={()=>{setCSOpen(true)}}> Send a Bill </ListItem>
                                <ListItem button onClick={()=>{setCSOpen(true)}}> Add an Income Item </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>

            <AccountForm open={newOpen} setOpen={setNewOpen} {...props}/>
            <ComingSoonDialog open={CSOpen} setOpen={setCSOpen}/>

        </React.Fragment>
    );

}
