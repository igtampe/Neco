import { Search } from "@mui/icons-material";
import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, TableContainer, Table, TableHead, TableRow, TableCell, Box, TextField, IconButton, Paper, TableBody, CircularProgress } from "@mui/material";
import React, { useState } from "react";
import { APIURL } from "../../App";
import { GenerateGet } from '../../RequestOptionGenerator'
import { IncomeTypes } from '../AdminComponents/JurisdictionComponents/BracketDisplay'
import { IncomeTypeSelect } from "../AdminComponents/JurisdictionComponents/BracketEditor";
import { FormatAccountID } from "./AccountDisplay";

function AccountPickerRow(props) {

    return (<>
        <TableRow>
            <TableCell width={'120px'}>{FormatAccountID(props.A.id)}</TableCell>
            <TableCell>{props.A.name}</TableCell>
            <TableCell width={'120px'}>{IncomeTypes[props.A.incomeType]}</TableCell>
            <TableCell width={'70px'}> <Button onClick={() => { props.handleOK(props.A.id) }} variant='contained'>Select</Button></TableCell>
        </TableRow>
    </>)


}


export default function AccountPicker(props) {

    const [accounts, setAccounts] = useState(undefined)

    const [query, setQuery] = useState("")
    const [type, setType] = useState(0)

    const [loading, setLoading] = useState(false)

    if (!accounts && !loading) {
        setLoading(true)

        var url;
        if (props.self) { url = APIURL + '/API/Bank/Accounts' }
        else {
            url = APIURL + '/API/Bank/Accounts/Dir?Type=' + type
            if (query !== "") { url = url + '&Query=' + query }
        }

        console.log(url)

        fetch(url, GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }
                setAccounts(data)
                setLoading(false);
            })


    }

    const handleClosing = () => { props.setOpen(false) }
    const handleOK = (ID) => {
        props.setAccountID(ID)
        handleClosing();
    }

    return (
        <>
            <Dialog maxWidth='sm' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> Pick an account</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        {
                            props.self ? <></> :
                                <>
                                    <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                                        <TextField fullWidth label="Search" value={query} onChange={(event) => { setQuery(event.target.value) }} />
                                        <IconButton onClick={() => { setAccounts(undefined) }}><Search /></IconButton>
                                    </Box>
                                    <Box sx={{ display: 'flex', alignItems: 'flex-end', marginTop: '15px' }}>
                                        <IncomeTypeSelect type={type} setType={(type) => { setType(type); setAccounts(undefined) }} />
                                    </Box>
                                </>
                        }
                        <TableContainer component={Paper} style={props.mini ? {} : { marginTop: '25px' }}>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>ID</TableCell>
                                        <TableCell>Name</TableCell>
                                        <TableCell>Type</TableCell>
                                        <TableCell></TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        !accounts
                                            ? <TableRow> <TableCell colSpan={4} style={{ textAlign: "center" }}><CircularProgress /></TableCell> </TableRow>
                                            : <>
                                                {accounts.length === 0
                                                    ? <TableRow><TableCell colSpan={4} style={{ textAlign: "center" }}> No accounts were found </TableCell></TableRow>
                                                    : accounts.map(A => { return (<AccountPickerRow A={A} handleOK={handleOK} />) })
                                                }
                                            </>
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClosing}>Cancel</Button>
                </DialogActions>

            </Dialog>

        </>
    )

}