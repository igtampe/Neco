import React, { useState } from "react";
import { IconButton, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, CircularProgress, Paper } from '@mui/material'
import OpenIcon from '@mui/icons-material/FileOpen'
import { GenerateGet, GenerateJSONPut } from "../../RequestOptionGenerator";

function ApproveCorpRow(props) {

    //This is basically a component
    const [open, setOpen] = useState(false);
    const [inProgress, setInProgress] = useState(false);

    const approve = () => {

        setInProgress(true)

        fetch('API/Income/SDC/Corporations/' + props.c.id, GenerateJSONPut(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.error) { return; }

                setInProgress(false)
                props.setCollection(undefined)
            })

    }

    return (
        <>
            <TableRow>
                <TableCell>{props.c.name}</TableCell>
                <TableCell width={'250px'}>{props.c.calculatedIncome.toLocaleString()}p</TableCell>
                <TableCell width={'120px'}>
                    <IconButton disabled={inProgress} onClick={() => { setOpen(true) }}><OpenIcon /></IconButton>
                </TableCell>
            </TableRow>

        </>
    )
}

export default function ApproveCorpDisplay(props) {

    const [corps, setCorps] = useState(undefined)
    const [loading, setLoading] = useState(false);

    //OK now 
    if (!corps && !loading) {

        setLoading(true)

        fetch('/API/Income/SDC/Unapproved', GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
            .then(response => { return (response.json()) })
            .then(data => {

                //if there was an error then oops
                if (data.error || data.errors) { return; }

                setCorps(data)
                setLoading(false)

            })

    }

    return (
        <React.Fragment>
            <TableContainer component={Paper} style={{ marginTop: '25px', height:'30vh', overflowY:'auto'}}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Income</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody> {!corps
                        ? <TableRow><TableCell colSpan={3} style={{ textAlign: 'center' }}> <CircularProgress /> </TableCell></TableRow>
                        : <>
                            {
                                corps.length === 0
                                    ? <TableRow><TableCell colSpan={3} style={{ textAlign: 'center' }}> No Corporations are pending approval</TableCell></TableRow>
                                    : <> {corps.map(c => {
                                        return ( <ApproveCorpRow {...props} c={c} setCorps={setCorps} /> )
                                    })}
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