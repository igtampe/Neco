import React, { useState } from "react";
import { Box, TextField, IconButton, Select, MenuItem, FormControl, InputLabel, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, CircularProgress, Paper } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search'
import EditIcon from '@mui/icons-material/Edit'
import { GenerateGet } from '../../../RequestOptionGenerator'

//Requires type, settype, setcollection
export function IncomeItemSortSelect(props) {
    return (
        <FormControl fullWidth style={{ margintop: "15px" }}>
            <InputLabel fullWidth>Type</InputLabel>
            <Select fullWidth label="Label" value={props.type} onChange={(event) => {
                props.setType(event.target.value)
                props.setCollection(undefined)
            }}>
                <MenuItem value={0}>Name</MenuItem>
                <MenuItem value={1}>Name Descending</MenuItem>
                <MenuItem value={2}>Newest first</MenuItem>
                <MenuItem value={3}>Oldest first</MenuItem>
                <MenuItem value={4}>Last Updated first</MenuItem>
                <MenuItem value={5}>Last updated last</MenuItem>
            </Select></FormControl>
    )

}

function IncomeItemRow(props) {

    //This is basically a component
    const [open, setOpen] = useState(false);

    return (
        <>
            <TableRow>
                <TableCell width={'90px'}>
                    <a href={props.J.imageURL === "" ? "/Flag.png" : props.J.imageURL}>
                        <img alt={'Flag'} src={props.J.imageURL === "" ? "/Flag.png" : props.J.imageURL} height={'50px'} /></a>
                </TableCell>
                <TableCell width={'90px'}>{props.J.id}</TableCell>
                <TableCell>{props.J.name}</TableCell>
                <TableCell>{!props.J.parentJurisdiction ? "None" : props.J.parentJurisdiction.id + ": " + props.J.parentJurisdiction.name }</TableCell>
                <TableCell width={'90px'}>

                    {/** Edit Button. Potentially make the editor here too */}
                    <IconButton onClick={() => { setOpen(true)}}><EditIcon /></IconButton>

                </TableCell>
            </TableRow>

            <JurisdictionEditor jurisdiction={props.J} setJurisdictions={props.setJurisdictions} open={open} setOpen={setOpen} Session={props.Session}/>

        </>
    )
}

function TableHeaders(props){
    return(<TableRow>
        <TableCell></TableCell>
        <TableCell>ID</TableCell>
        <TableCell>Name</TableCell>
        <TableCell>Parent</TableCell>
        <TableCell>Actions</TableCell>
    </TableRow>
    )
}

export default function JurisdictionDisplay(props) {

    const [query, setQuery] = useState("");
    const [type, setType] = useState(0)

    const [jurisdictions, setJurisdictions] = useState(undefined)
    const [loading, setLoading] = useState(false);

    const [newOpen, setNewOpen] = useState(false)

    const startSearch = (event) => { setJurisdictions(undefined) }

    //OK now 
    if (!jurisdictions && !loading) {

        setLoading(true)

        var URL = '/API/Taxes/Jurisdictions?Type=' + type
        if (query !== "") { URL = URL + '&Query=' + query }

        fetch(URL, GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
            .then(response => { return (response.json()) })
            .then(data => {

                //if there was an error then oops
                if (data.error) { return; }

                setJurisdictions(data)
                setLoading(false)

            })

    }


    return (
        <React.Fragment>
            <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <TextField fullWidth label="Search" value={query} onChange={(event) => { setQuery(event.target.value) }} />
                {props.Vertical ? <></> :
                    <Box sx={{ display: 'flex', alignItems: 'flex-end' }} style={{ marginLeft: "25px" }}>
                        <JurisdictionTypeSelect type={type} setType={setType} setCollection={setJurisdictions} /> </Box>
                }
                <IconButton onClick={startSearch} style={{ marginLeft: '10px', marginBottom: '7px' }}><SearchIcon /></IconButton>
                <IconButton onClick={() => { setNewOpen(true)}} style={{ marginBottom: '7px' }}><AddIcon /></IconButton>
            </Box>
            {props.Vertical ?
                <Box sx={{ display: 'flex', alignItems: 'flex-end' }} style={{ marginTop: "25px" }}>
                    <JurisdictionTypeSelect type={type} setType={setType} setCollection={setJurisdictions} />
                </Box>
                : <></>}

            <TableContainer component={Paper} style={{ marginTop: '25px' }}>
                <Table>
                    <TableHead>
                        <TableHeaders {...props}/>
                    </TableHead>
                    <TableBody>
                        {
                            !jurisdictions ?
                                <TableRow>
                                    <TableCell colSpan={5} style={{ textAlign: "center" }}><CircularProgress /></TableCell>
                                </TableRow> : <>{

                                    jurisdictions.length === 0 ?
                                        <TableRow>
                                            <TableCell colSpan={5} style={{ textAlign: "center" }}>No jurisdictions</TableCell>
                                        </TableRow> : <>{ jurisdictions.map(J => { return (<JurisdictionRow J={J} setJurisdictions={setJurisdictions} Session={props.Session}/>)})}</>
                                }
                                </>
                        }
                    </TableBody>
                </Table>
            </TableContainer>

            <JurisdictionEditor setJurisdictions={setJurisdictions} open={newOpen} setOpen={setNewOpen} Session={props.Session}/>

        </React.Fragment>
    );

}
