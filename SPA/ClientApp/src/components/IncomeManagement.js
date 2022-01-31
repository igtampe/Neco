import { Card, CardContent, CircularProgress, Divider, FormControl, Grid, InputLabel, List, ListItem, MenuItem, Select} from "@mui/material";
import { Box } from "@mui/system";
import React, { useState } from "react";
import { GenerateGet } from "../RequestOptionGenerator";
import { Accountheaders, FormatAccountID } from "./AccountComponents/AccountDisplay";
import { ComingSoonDialog } from "./Accounts";
import IncomeTabs from "./IncomeComponents/IncomeTabs";
import NecoHeader from "./NecoHeader";

function AccountSelectMenuItem(props){

return(
    <Box sx={{ width: '100%', alignItems: 'flex' }}>
    <table width='100%'>
        <tr>
            <td> <b>{props.account.name}</b></td>
            <td rowSpan={2} style={{ flexGrow: true, textAlign: 'right' }}>
                <img src={Accountheaders[props.account.incomeType]} alt={'Account Type Header'} height='20px' /></td>
        </tr>
        <tr><td>{FormatAccountID(props.account.id)}</td></tr>
    </table>
</Box>
)
}

export function AccountSelect(props){

    const [val, setVal] = useState(undefined);
    const [accounts, setAccounts] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!accounts && !loading) {
        setLoading(true)

        fetch('/API/Bank/Accounts', GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }
                setAccounts(data)
                setLoading(false);
            })
    }

    const handleUpdateAccount = (index) =>{
        setVal(index)
        props.setAccount(accounts[index]);
    }

    if(!accounts){
        return (<CircularProgress/>)
    }

    return (
        <>
            <FormControl fullWidth style={{ margintop: "15px" }} disabled={props.type===0}>
                <InputLabel fullWidth>Account</InputLabel>
                <Select fullWidth label="Account" value={val} onChange={(event) => { handleUpdateAccount(event.target.value) }}>
                    { accounts.map( (a,i) =>{  return( 
                        <MenuItem value={i}>
                            <AccountSelectMenuItem account={a}/>
                        </MenuItem>)}
                    )  
                    }
                </Select></FormControl>
        </>
    )

}

export default function IncomeComponent(props) {

    const [Account, setAccount] = useState(undefined)
    const [CSOpen,setCSOpen] = useState(false);

    const [summary, setSummary] = useState(undefined);

    const updateAccount=(account)=>{
        setSummary(undefined)
        setAccount(account)
    }

    if(!props.User) {return (<CircularProgress/>)}

    return (
        <React.Fragment>
            <NecoHeader name={props.User ? props.User.name : '...'} />
            <Grid container spacing={2} style={{ minWidth: '100%' }}>
                <Grid item xs={props.Vertical ? 12 : 9}>
                    <IncomeTabs {...props} summary={summary} setSummary={setSummary} account={Account}/>
                </Grid>
                <Grid item xs={props.Vertical ? 12 : 3}>
                    <Card sx={{ minWidth: '100%', maxHeight:'100%', height:(props.Vertical ? 'auto': '60vh') }}>
                        <CardContent>
                        <AccountSelect {...props} setAccount={updateAccount}/>
                        <br/><br/>
                            <b>Quick actions</b>
                            <Divider />
                            <List>
                                <ListItem button disabled={!Account} onClick={()=>{setCSOpen(true)}}> Generate a Tax Report  </ListItem>
                                <ListItem button disabled={!Account} onClick={()=>{setCSOpen(true)}}> See past Tax Reports </ListItem>
                            </List>
                            <br/><b>File New Income Items:</b>
                            <Divider/>
                            <List>
                                <ListItem button disabled={!Account || Account.incomeType!==1} onClick={()=>{setCSOpen(true)}}> File an Airline </ListItem>
                                <ListItem button disabled={!Account || Account.incomeType!==1} onClick={()=>{setCSOpen(true)}}> File a Corporation </ListItem>
                                <ListItem button disabled={!Account} onClick={()=>{setCSOpen(true)}}> File a Business </ListItem>
                                <ListItem button disabled={!Account} onClick={()=>{setCSOpen(true)}}> File a Hotel </ListItem>
                                <ListItem button disabled={!Account} onClick={()=>{setCSOpen(true)}}> File an Apartment Building </ListItem>                                
                            </List>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>

            <ComingSoonDialog open={CSOpen} setOpen={setCSOpen}/>

        </React.Fragment>
    );

}
