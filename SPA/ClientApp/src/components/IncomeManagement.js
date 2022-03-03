import { Card, CardContent, CircularProgress, Divider, FormControl, Grid, IconButton, InputLabel, List, ListItem, Menu, MenuItem, Select } from "@mui/material";
import { Box } from "@mui/system";
import React, { useState } from "react";
import { GenerateGet } from "../RequestOptionGenerator";
import { Accountheaders, FormatAccountID } from "./AccountComponents/AccountDisplay";
import { ComingSoonDialog } from "./Accounts";
import IncomeForm from "./IncomeComponents/IncomeItemForm";
import IncomeTabs from "./IncomeComponents/IncomeTabs";
import NecoHeader from "./NecoHeader";
import TaxReportForm from "./TaxComponents/TaxReportForm";
import MoreVertIcon from '@mui/icons-material/MoreVert';
import PastTaxReportsForm from "./TaxComponents/PastTaxReportsForm";
import {APIURL} from '../App'

function AccountSelectMenuItem(props) {

    return (
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

export function AccountSelect(props) {

    const [val, setVal] = useState(undefined);
    const [accounts, setAccounts] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!accounts && !loading) {
        setLoading(true)

        fetch(APIURL + '/API/Bank/Accounts', GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }
                setAccounts(data)
                setLoading(false);
            })
    }

    const handleUpdateAccount = (index) => {
        setVal(index)
        props.setAccount(accounts[index]);
    }

    if (!accounts) {
        return (<CircularProgress />)
    }

    return (
        <>
            <FormControl fullWidth style={{ margintop: "15px" }} disabled={props.type === 0}>
                <InputLabel fullWidth>Account</InputLabel>
                <Select fullWidth label="Account" value={val} onChange={(event) => { handleUpdateAccount(event.target.value) }}>
                    {accounts.map((a, i) => {
                        return (
                            <MenuItem value={i}>
                                <AccountSelectMenuItem account={a} />
                            </MenuItem>)
                    }
                    )
                    }
                </Select></FormControl>
        </>
    )

}

export default function IncomeComponent(props) {

    const [Account, setAccount] = useState(undefined)
    const [CSOpen, setCSOpen] = useState(false);

    const [anchorEl, setAnchorEl] = React.useState(null);
    const MenuOpen = Boolean(anchorEl);

    const [summary, setSummary] = useState(undefined);
    const [airlines, setAirlines] = useState(undefined);
    const [corporations, setCorporations] = useState(undefined);
    const [businesses, setBusinesses] = useState(undefined);
    const [hotels, setHotels] = useState(undefined);
    const [apartments, setApartments] = useState(undefined);

    const setCollections = [setAirlines, setCorporations, setBusinesses, setHotels, setApartments]

    const [creator, setCreator] = useState({ open: false, type: -1 })
    const [genOpen, setGenOpen] = useState(false);
    const [pastOpen, setPastOpen] = useState(false);

    const updateAccount = (account) => {
        setSummary(undefined); setAirlines(undefined); setCorporations(undefined);
        setBusinesses(undefined); setHotels(undefined); setApartments(undefined);
        setAccount(account)
    }

    const handleClick = (event) => { setAnchorEl(event.currentTarget); };
    const handleClose = () => { setAnchorEl(null); };

    if (!props.User) { return (<CircularProgress />) }

    return (
        <React.Fragment>
            <NecoHeader name={props.User ? props.User.name : '...'} />
            { props.Vertical ?<Box sx={{ display: 'flex', alignItems: 'flex-end', marginBottom:'15px' }}>
                <AccountSelect {...props} setAccount={updateAccount} />    
                {
                    Account ? <IconButton onClick={handleClick}><MoreVertIcon /></IconButton> : <></>
                }        
            </Box>:<></>}
            <Grid container spacing={2} style={{ minWidth: '100%' }}>
                <Grid item xs={props.Vertical ? 12 : 9}>
                    <IncomeTabs {...props} summary={summary} setSummary={setSummary} account={Account}
                        airlines={airlines} setAirlines={setAirlines} corporations={corporations} setCorporations={setCorporations}
                        businesses={businesses} setBusinesses={setBusinesses} hotels={hotels} setHotels={setHotels}
                        apartments={apartments} setApartments={setApartments} />
                </Grid>
                {
                    props.Vertical ? <></> : <>
                        <Grid item xs={props.Vertical ? 12 : 3}>
                            <Card sx={{ minWidth: '100%', maxHeight: '100%', height: (props.Vertical ? 'auto' : '60vh') }}>
                                <CardContent>
                                    <AccountSelect {...props} setAccount={updateAccount} />
                                    <br /><br />
                                    <b>Quick actions</b>
                                    <Divider />
                                    <List>
                                        <ListItem button disabled={!Account} onClick={() => { setGenOpen(true) }}> Generate a Tax Report  </ListItem>
                                        <ListItem button disabled={!Account} onClick={() => { setPastOpen(true) }}> See past Tax Reports </ListItem>
                                    </List>
                                    <br /><b>File New Income Items:</b>
                                    <Divider />
                                    <List>
                                        <ListItem button disabled={!Account || Account.incomeType !== 1} onClick={() => { setCreator({ open: true, type: 0 }) }}> File an Airline </ListItem>
                                        <ListItem button disabled={!Account || Account.incomeType !== 1} onClick={() => { setCreator({ open: true, type: 1 }) }}> File a Corporation </ListItem>
                                        <ListItem button disabled={!Account} onClick={() => { setCreator({ open: true, type: 2 }) }}> File a Business </ListItem>
                                        <ListItem button disabled={!Account} onClick={() => { setCreator({ open: true, type: 3 }) }}> File a Hotel </ListItem>
                                        <ListItem button disabled={!Account} onClick={() => { setCreator({ open: true, type: 4 }) }}> File an Apartment Building </ListItem>
                                    </List>
                                </CardContent>
                            </Card>
                        </Grid>
                    </>
                }
            </Grid>

            <IncomeForm {...props} open={creator.open && creator.type !== -1} setOpen={(val) => setCreator({ ...creator, open: val })} account={Account}
                airline={creator.type === 0} corporation={creator.type === 1} business={creator.type === 2} hotel={creator.type === 3} apartment={creator.type === 4}
                setCollection={setCollections[creator.type]}
            />

            <PastTaxReportsForm {...props} open={pastOpen} setOpen={setPastOpen} account={Account}/>

            <TaxReportForm {...props} open={genOpen} setOpen={setGenOpen} account={Account} />

            <ComingSoonDialog open={CSOpen} setOpen={setCSOpen} />

            <Menu anchorEl={anchorEl} open={MenuOpen} onClose={handleClose} >
                    <MenuItem><b>Quick actions</b></MenuItem>
                    <MenuItem><Divider /></MenuItem>
                    <MenuItem button disabled={!Account} onClick={() => { setGenOpen(true) }}> Generate a Tax Report  </MenuItem>
                    <MenuItem button disabled={!Account} onClick={() => { setPastOpen(true) }}> See past Tax Reports </MenuItem>
                    <MenuItem><br /><b></b></MenuItem>
                    <MenuItem><br /><b>File New Income Items:</b></MenuItem>
                    <MenuItem><Divider /></MenuItem>
                    <MenuItem button disabled={!Account || Account.incomeType !== 1} onClick={() => { setCreator({ open: true, type: 0 }) }}> File an Airline </MenuItem>
                    <MenuItem button disabled={!Account || Account.incomeType !== 1} onClick={() => { setCreator({ open: true, type: 1 }) }}> File a Corporation </MenuItem>
                    <MenuItem button disabled={!Account} onClick={() => { setCreator({ open: true, type: 2 }) }}> File a Business </MenuItem>
                    <MenuItem button disabled={!Account} onClick={() => { setCreator({ open: true, type: 3 }) }}> File a Hotel </MenuItem>
                    <MenuItem button disabled={!Account} onClick={() => { setCreator({ open: true, type: 4 }) }}> File an Apartment Building </MenuItem>
            </Menu>


        </React.Fragment>
    );

}
