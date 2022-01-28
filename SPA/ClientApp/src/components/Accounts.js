import { Card, CardContent, CircularProgress, Divider, Grid, List, ListItem } from "@mui/material";
import React from "react";
import NecoHeader from "./NecoHeader";

export default function AccountsComponent(props) {

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
                                <ListItem button> Add an Account </ListItem>
                                <ListItem button> Send a Check </ListItem>
                                <ListItem button> Send a Bill </ListItem>
                                <ListItem button> Add an Income Item </ListItem>
                            </List>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </React.Fragment>
    );

}
