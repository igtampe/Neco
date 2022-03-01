import React, { useState } from "react";
import { Grid, Checkbox, Box, TextField, IconButton, Select, MenuItem, FormControl, InputLabel, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, CircularProgress, Paper } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search'
import EditIcon from '@mui/icons-material/Edit'
import DeleteIcon from '@mui/icons-material/Delete'
import { GenerateDelete, GenerateGet } from '../../RequestOptionGenerator'
import DeleteConfirm from "../Subcomponents/DeleteConfirm";
import IncomeForm from './IncomeItemForm'
import { APIURL } from "../../App";

//Requires type, settype, setcollection
export function IncomeItemSortSelect(props) {
    return (
        <FormControl fullWidth style={{ margintop: "15px" }}>
            <InputLabel fullWidth>Sort</InputLabel>
            <Select fullWidth label="Sort" value={props.sort} onChange={(event) => {
                props.setSort(event.target.value)
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
    const [delOpen, setDelOpen] = useState(false);
    const [delInProgress, setDelInProgress] = useState(false);

    const realDelete = () => {

        setDelInProgress(true)

        fetch(props.baseUrl + '/' + props.i.id, GenerateDelete(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.error) { return; }

                setDelInProgress(false)
                //Success:
                props.setCollection(undefined)

            })

    }

    return (
        <>
            <TableRow>
                <TableCell>{props.i.name}</TableCell>
                <TableCell width={'150px'}>{props.i.calculatedIncome.toLocaleString()}p</TableCell>
                <TableCell width={'70px'}>
                    <Checkbox checked={props.i.approved} readOnly />
                </TableCell><TableCell width={'120px'}>
                    <IconButton disabled={delInProgress} onClick={() => { setOpen(true) }}><EditIcon /></IconButton>
                    <IconButton disabled={delInProgress} onClick={() => { setDelOpen(true) }}><DeleteIcon /></IconButton>
                </TableCell>
            </TableRow>

            <IncomeForm {...props} open={open} setOpen={setOpen} item={props.i} />

            <DeleteConfirm {...props} open={delOpen} setOpen={setDelOpen} delete={realDelete}>
                Are you sure you want to delete {props.i.name}? You will cease receiving income from this item, and it will not show up on any future reports.
            </DeleteConfirm>

        </>
    )
}

function TableHeaders(props) {
    return (<TableRow>
        <TableCell>Name</TableCell>
        <TableCell>Income</TableCell>
        <TableCell>Approved</TableCell>
        <TableCell>Actions</TableCell>
    </TableRow>
    )
}

export default function IncomeItemDisplay(props) {

    const [query, setQuery] = useState("");
    const [sort, setSort] = useState(0)

    const [loading, setLoading] = useState(false);

    //what a mess
    const collection = props.airline
        ? props.airlines
        : props.corporation
            ? props.corporations
            : props.business
                ? props.businesses
                : props.hotel
                    ? props.hotels
                    : props.apartment
                        ? props.apartments
                        : undefined


    const setCollection = props.airline
        ? props.setAirlines
        : props.corporation
            ? props.setCorporations
            : props.business
                ? props.setBusinesses
                : props.hotel
                    ? props.setHotels
                    : props.apartment
                        ? props.setApartments
                        : undefined

    const baseUrl = props.airline
        ? APIURL + "/API/Income/Airlines"
        : props.corporation
            ? APIURL + "/API/Income/Corporations"
            : props.business
                ? APIURL + "/API/Income/Businesses"
                : props.hotel
                    ? APIURL + "/API/Income/Hotels"
                    : props.apartment
                        ? APIURL + "/API/Income/Apartments"
                        : undefined


    const startSearch = (event) => { setCollection(undefined) }

    //OK now 
    if (!collection && setCollection && props.open && !loading) {

        setLoading(true)

        var URL = baseUrl + '?AccountID=' + props.account.id + '&Sort=' + sort
        if (query !== "") { URL = URL + '&Query=' + query }

        fetch(URL, GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
            .then(response => { return (response.json()) })
            .then(data => {

                //if there was an error then oops
                if (data.error) { return; }

                setCollection(data)
                setLoading(false)

            })

    }

    return (
        <React.Fragment>
            <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <Grid container spacing={2}>
                    <Grid item xs={props.Vertical ? 12 : 8}>
                        <TextField fullWidth label="Search" value={query} onChange={(event) => { setQuery(event.target.value) }} />
                    </Grid>
                    {props.Vertical ? <></> : <Grid item xs={4}><IncomeItemSortSelect sort={sort} setSort={setSort} setCollection={setCollection} /></Grid>}
                </Grid>
                <IconButton onClick={startSearch} style={{ marginLeft: '10px', marginBottom: '7px' }}><SearchIcon /></IconButton>
            </Box>
            {props.Vertical ?
                <Box sx={{ display: 'flex', alignItems: 'flex-end' }} style={{ marginTop: "25px" }}>
                    <IncomeItemSortSelect sort={sort} setSort={setSort} setCollection={setCollection} />
                </Box>
                : <></>}

            <TableContainer component={Paper} style={{ marginTop: '25px' }}>
                <Table>
                    <TableHead>
                        <TableHeaders {...props} />
                    </TableHead>
                    <TableBody>
                        {
                            !collection
                                ? <TableRow><TableCell colSpan={4} style={{ textAlign: 'center' }}> <CircularProgress /> </TableCell></TableRow>
                                : <>
                                    {
                                        collection.length === 0
                                            ? <TableRow><TableCell colSpan={4} style={{ textAlign: 'center' }}> No items were found </TableCell></TableRow>
                                            : <>
                                                {
                                                    collection.map(i => {

                                                        return (
                                                            <IncomeItemRow {...props} i={i} collection={collection} setCollection={setCollection} baseUrl={baseUrl} />
                                                        )
                                                    })

                                                }
                                            </>
                                    }
                                </>
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </React.Fragment>
    );

}
