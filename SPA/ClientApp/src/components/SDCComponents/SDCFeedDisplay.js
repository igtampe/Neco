import React, { useState } from "react";
import { IconButton, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, CircularProgress, Paper } from '@mui/material'
import OpenIcon from '@mui/icons-material/FileOpen'
import { GenerateGet} from "../../RequestOptionGenerator";
import IncomeApprovalForm from "./IncomeItemApprovalForm";
import { APIURL } from "../../App";

export const ItemTypes = ['Apartment','Hotel','Business','Corporation','Airline']

function ApproveCorpRow(props) {

    //This is basically a component
    const [open, setOpen] = useState(false);

    return (
        <>
            <TableRow>
                <TableCell>{props.c.name}</TableCell>
                <TableCell width={'250px'}>{ItemTypes[props.c.type]} ({props.c.type})</TableCell>
                <TableCell width={'250px'}>{props.c.account ? props.c.account.name + " (" + props.c.account.id + ")" : "" }</TableCell>
                <TableCell width={'250px'}>{props.c.jurisdiction ? props.c.jurisdiction.name + " (" + props.c.jurisdiction.id + ")" : "" }</TableCell>
                <TableCell width={'250px'}>{props.c.calculatedIncome.toLocaleString()}p</TableCell>
                <TableCell width={'120px'}>
                    <IconButton onClick={() => { setOpen(true) }}><OpenIcon /></IconButton>
                </TableCell>
            </TableRow>

            <IncomeApprovalForm {...props} open={open} setOpen={setOpen} item={props.c} setItems={props.setCorps}
                airline={props.c.type===4} corporation={props.c.type===3} business={props.c.type===2} hotel={props.c.type===1} apartment={props.c.type===0}
            />

        </>
    )
}

export default function SDCFeedDisplay  (props) {

    const [corps, setCorps] = useState(undefined)
    const [loading, setLoading] = useState(false);

    //OK now 
    if (!corps && !loading) {

        setLoading(true)

        fetch(APIURL + '/API/Income/SDC/'+ (props.approved ? "A" : "Una") +'pproved', GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
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
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell>Name</TableCell>
                            <TableCell>Type</TableCell>
                            <TableCell>Account</TableCell>
                            <TableCell>Jurisdiction</TableCell>
                            <TableCell>Income</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody> {!corps
                        ? <TableRow><TableCell colSpan={6} style={{ textAlign: 'center' }}> <CircularProgress /> </TableCell></TableRow>
                        : <>
                            {
                                corps.length === 0
                                    ? <TableRow><TableCell colSpan={6} style={{ textAlign: 'center' }}> 
                                        {props.approved
                                            ? 'No items have yet to be approved'
                                            : 'No items currently pending approval'
                                        }
                                    </TableCell></TableRow>
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
