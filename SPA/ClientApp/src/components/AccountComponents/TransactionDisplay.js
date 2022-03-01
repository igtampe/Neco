import { Box, CircularProgress, FormControl, Grid, IconButton, InputLabel, MenuItem, Paper, Select, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField } from "@mui/material";
import React, { useState } from "react";
import ReceiptIcon from '@mui/icons-material/Receipt'
import { GenerateGet } from "../../RequestOptionGenerator";
import { Search } from "@mui/icons-material";
import ReceiptDialog from "./ReceiptDIalog";
import { APIURL } from "../../App";

function TransactionRow(props){

    //This is basically a component
    const [open, setOpen] = useState(false);

    var D = new Date(props.T.date);

    return (
        <>
            <TableRow>
                <TableCell width={'200px'}>{D.toLocaleString()}</TableCell>
                <TableCell>{props.T.name}</TableCell>
                <TableCell width={'120px'}>{props.T.amount.toLocaleString()}p</TableCell>
                <TableCell width={'70px'}> <IconButton onClick={() => { setOpen(true)}}><ReceiptIcon /></IconButton></TableCell>
            </TableRow>

            <ReceiptDialog {...props} open={open} setOpen={setOpen} transactionID={props.T.id}/>

        </>
    )

}

function TransactionTypeSelect(props){
    return (
        <FormControl fullWidth style={{ margintop: "15px" }}>
            <InputLabel fullWidth>Type</InputLabel>
            <Select fullWidth label="Label" value={props.type} onChange={(event) => {
                props.setType(event.target.value)
                props.setCollection(undefined)
            }}>
                <MenuItem value={-1}>Any</MenuItem>
                <MenuItem value={0}>Debit</MenuItem>
                <MenuItem value={1}>Credit</MenuItem>
            </Select></FormControl>
    )
}

function TransactionSortSelect(props){
    return (
        <FormControl fullWidth style={{ margintop: "15px" }}>
            <InputLabel fullWidth>Sort</InputLabel>
            <Select fullWidth label="Label" value={props.sort} onChange={(event) => {
                props.setSort(event.target.value)
                props.setCollection(undefined)
            }}>
                <MenuItem value={0}>Date</MenuItem>
                <MenuItem value={1}>Date (Ascending)</MenuItem>
                <MenuItem value={2}>Amount</MenuItem>
                <MenuItem value={3}>Amount (Ascending)</MenuItem>
            </Select></FormControl>
    )
}

export default function TransactionDisplay(props){

    const [transactions, setTransactions] = useState(undefined)
    const [loading, setLoading] = useState(false)

    const [query, setQuery] = useState("")
    const [startDate, setStartDate] = useState("")
    const [endDate, setEndDate] = useState("")
    const [transactionType, setTransactionType] = useState(-1)
    const [sort,setSort] = useState(0);
    
    const startSearch = () => { setTransactions(undefined) }    

    if(props.accountID && !transactions && !loading){

        //Time to load
        setLoading(true)

        var url = APIURL + '/API/Bank/Accounts/' + props.accountID + '/Transactions'
        
        if(props.mini){url=url+"?Take=3&Sort=0"} //this should be the ONLY prop in mini
        else { url=url+'?Type='+ transactionType +'&Sort=' + sort }

        if(query!==""){url=url+'&Query='+query}
        if(startDate!==""){url=url+'&Start='+startDate}
        if(endDate!==""){url=url+'&End='+endDate}

        fetch(url, GenerateGet(props.Session)) //actually we can just fetch normally no?
        .then(response=>response.json())
        .then(data=>{
            //if error, oops
            if(data.error || data.errors){ return; }

            setTransactions(data)
            setLoading(false)
        })

    }

    return(<>
    
        { props.mini ? <></> 
        : <>
        
        <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <TextField fullWidth label="Search" value={query} onChange={(event) => { setQuery(event.target.value) }} />
                <IconButton onClick={startSearch}><Search /></IconButton>
            </Box>

            <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <Grid container style={{ marginTop: '15px' }} spacing={2}>
                    <Grid item xs={props.Vertical ? 6 : 3}>
                        <TransactionTypeSelect type={transactionType} setType={setTransactionType} setCollection={setTransactions}/>
                    </Grid>
                    <Grid item xs={props.Vertical ? 6 : 3}>
                        <TransactionSortSelect sort={sort} setSort={setSort} setCollection={setTransactions}/>
                    </Grid>
                    <Grid item xs={props.Vertical ? 6 : 3}>
                        <TextField label="Start" type="date" value={startDate} onChange={(event) => { 
                            setStartDate(event.target.value) 
                            startSearch();
                        }} InputLabelProps={{ shrink: true, }} fullWidth />
                    </Grid>
                    <Grid item xs={props.Vertical ? 6 : 3}>
                        <TextField label="End" type="date" value={endDate} onChange={(event) => { 
                            setEndDate(event.target.value) 
                            startSearch();
                        }} InputLabelProps={{ shrink: true, }} fullWidth />
                    </Grid>
                </Grid>
            </Box>

        </> }

        <TableContainer component={Paper} style={props.mini ? {} : { marginTop: '25px' }}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Date</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Amount</TableCell>
                            <TableCell>Receipt</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            !transactions 
                                ? <TableRow> <TableCell colSpan={4} style={{ textAlign: "center" }}><CircularProgress /></TableCell> </TableRow> 
                                : <>
                                    { transactions.length===0 
                                        ? <TableRow><TableCell colSpan={6} style={{ textAlign: "center" }}> This account has no transactions </TableCell></TableRow> 
                                        : transactions.map(T => {  return (<TransactionRow T={T} {...props}/>) })
                                    }
                                </>
                        }
                    </TableBody>
                </Table>
            </TableContainer>
    
    </>)

}