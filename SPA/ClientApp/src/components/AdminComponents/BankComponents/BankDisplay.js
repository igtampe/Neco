import React, { useState } from "react";
import { Box, TextField, IconButton, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, CircularProgress, Paper } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search'
import EditIcon from '@mui/icons-material/Edit'
import AddIcon from '@mui/icons-material/Add'
import BankEditor from "./BankEditor";
import { APIURL } from "../../../App";

function BankRow(props) {

    //This is basically a component
    const [open, setOpen] = useState(false);

    return (
        <>
            <TableRow>
                <TableCell width={'125px'}>
                    <a href={props.B.imageURL === "" ? "/Bank.png" : props.B.imageURL}>
                        <img alt={'Flag'} src={props.B.imageURL === "" ? "/Bank.png" : props.B.imageURL} height={'50px'} /></a>
                </TableCell>
                <TableCell width={'90px'}>{props.B.id}</TableCell>
                <TableCell>{props.B.name}</TableCell>
                <TableCell width={'90px'}>

                    {/** Edit Button. Potentially make the editor here too */}
                    <IconButton onClick={() => { setOpen(true)}}><EditIcon /></IconButton>

                </TableCell>
            </TableRow>

            <BankEditor bank={props.B} setBanks={props.setBanks} open={open} setOpen={setOpen} Session={props.Session}/>

        </>
    )


}

export default function BankDisplay(props) {

    const [query, setQuery] = useState("");

    const [banks, setBanks] = useState(undefined)
    const [loading, setLoading] = useState(false);

    const [newOpen, setNewOpen] = useState(false)

    const startSearch = (event) => { setBanks(undefined) }

    //OK now 
    if (!banks && !loading) {

        setLoading(true)

        var URL = APIURL + '/API/Bank'
        if (query !== "") { URL = URL + '?Query=' + query }

        fetch(URL).then(response =>response.json())
            .then(data => {

                //if there was an error then oops
                if (data.error) { return; }

                setBanks(data)
                setLoading(false)

            })

    }


    return (
        <React.Fragment>
            <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <TextField fullWidth label="Search" value={query} onChange={(event) => { setQuery(event.target.value) }} />
                <IconButton onClick={startSearch} style={{ marginLeft: '10px', marginBottom: '7px' }}><SearchIcon /></IconButton>
                <IconButton onClick={() => { setNewOpen(true)}} style={{ marginBottom: '7px' }}><AddIcon /></IconButton>
            </Box>
            <TableContainer component={Paper} style={{ marginTop: '25px' }}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Logo</TableCell>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            !banks ?
                                <TableRow>
                                    <TableCell colSpan={4} style={{ textAlign: "center" }}><CircularProgress /></TableCell>
                                </TableRow> : <>{

                                    banks.length === 0 ?
                                        <TableRow>
                                            <TableCell colSpan={4} style={{ textAlign: "center" }}>No Banks</TableCell>
                                        </TableRow> : <>{ banks.map(B => <BankRow B={B} setBanks={setBanks} Session={props.Session}/>)}</>
                                }
                                </>
                        }
                    </TableBody>
                </Table>
            </TableContainer>

            <BankEditor setBanks={setBanks} open={newOpen} setOpen={setNewOpen} Session={props.Session}/>

        </React.Fragment>
    );

}
