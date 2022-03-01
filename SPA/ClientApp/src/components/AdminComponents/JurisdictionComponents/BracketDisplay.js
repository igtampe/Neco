import { Button, CircularProgress, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import React, { useState } from "react";
import EditIcon from '@mui/icons-material/Edit'
import DelIcon from '@mui/icons-material/Delete'
import BracketEditor from "./BracketEditor";
import DeleteConfirm from "../../Subcomponents/DeleteConfirm";
import { GenerateDelete } from "../../../RequestOptionGenerator";
import { APIURL } from "../../../App";

export const IncomeTypes = [ "Personal","Corporate","Government", "Charity" ]

function BracketRow(props){

    //This is basically a component
    const [open, setOpen] = useState(false);
    const [delConfirm, setDelConfirm] = useState(false);

    const [deleting, setDeleting] = useState(false)

    const RealDelete = () => {

        setDeleting(true)

        //send the fetch y lo que sea
        fetch(APIURL + '/API/Taxes/Brackets/' + props.B.id,GenerateDelete(props.Session))
        .then(r=>r.json())
        .then(data=>{

            setDeleting(false)
            if(data.error) {return;}            
            props.setBrackets(undefined)

        })

    }

    return (
        <>
            <TableRow>
                <TableCell width={'150px'}>{props.B.name}</TableCell>
                <TableCell width={'90px'}>{(props.B.rate.toLocaleString('en-US',{style:'percent'}))}</TableCell>
                <TableCell width={'120px'}>{IncomeTypes[props.B.incomeType]}</TableCell>
                <TableCell>{props.B.start.toLocaleString()}p</TableCell>
                <TableCell>{props.B.end.toLocaleString()}p</TableCell>
                <TableCell width={'120px'}>

                    { 
                        deleting ? <> <CircularProgress/> </>
                        : <>
                            <IconButton onClick={() => { setOpen(true)}}><EditIcon /></IconButton>
                            <IconButton onClick={() => { setDelConfirm(true)}}><DelIcon /></IconButton>
                        </>
                    }

                </TableCell>
            </TableRow>

            <BracketEditor bracket={props.B} setBrackets={props.setBrackets} open={open} setOpen={setOpen} Session={props.Session} {...props}/>
            
            <DeleteConfirm open={delConfirm} setOpen={setDelConfirm} delete={RealDelete}>
                Are you sure you want to delete this bracket?
            </DeleteConfirm>

        </>
    )

}

export default function BracketDisplay(props){

    const [brackets, setBrackets] = useState(undefined)
    const [loading, setLoading] = useState(false)

    const [newOpen, setNewOpen] = useState(false);

    if(props.JurisdictionID && !brackets && !loading){

        //Time to load
        setLoading(true)

        fetch(APIURL + '/API/Taxes/Jurisdiction/' + props.JurisdictionID + '/Brackets') //actually we can just fetch normally no?
        .then(response=>response.json())
        .then(data=>{

            //if error, oops
            if(data.error){ return; }

            setBrackets(data)
            setLoading(false)

        })

    }

    return(<>
    
        <TableContainer component={Paper} style={{ marginTop: '25px' }}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Rate</TableCell>
                            <TableCell>Income Type</TableCell>
                            <TableCell>Start</TableCell>
                            <TableCell>End</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            !brackets ?
                                <TableRow>
                                    <TableCell colSpan={6} style={{ textAlign: "center" }}><CircularProgress /></TableCell>
                                </TableRow> : <>
                                    { brackets.map(B => { return (<BracketRow B={B} setBrackets={setBrackets} Session={props.Session} {...props}/>)})}
                                    <TableRow><TableCell colSpan={6} style={{ textAlign: "center" }}>
                                        <Button variant="contained" onClick={()=>{setNewOpen(true)}}> Add a new Tax Bracket</Button>
                                    </TableCell></TableRow> 
                                </>
                        }
                    </TableBody>
                </Table>
            </TableContainer>

            <BracketEditor setBrackets={setBrackets} open={newOpen} setOpen={setNewOpen} Session={props.Session} {...props}/>
    
    </>)

}