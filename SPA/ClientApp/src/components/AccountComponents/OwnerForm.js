import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, TableContainer, Table, TableHead, TableRow, TableCell, TextField, IconButton, Paper, TableBody, CircularProgress } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet, GenerateJSONPost, GenerateDelete } from '../../RequestOptionGenerator'
import DeleteIcon from '@mui/icons-material/Clear'
import AddIcon from '@mui/icons-material/Add'
import AlertSnackbar from "../AlertSnackbar";

function OwnerRow(props) {

    const [inProgress, setInProgress] = useState(false)

    const handleDeleteOwner=()=>{
        setInProgress(true)

        //Add newOwner
        fetch(props.baseurl+'?Owner=' + props.O.id, GenerateDelete(props.Session))
        .then(response=>response.json())
        .then(data=>{

            setInProgress(false)

            if(data.error){
                props.setResult({severity:'error', text:data.reason})
                props.setSnackOpen(true)
                return;
            }

            if(data.errors){
                props.setResult({severity:'error', text:"an unknown, serverside error occurred"})
                props.setSnackOpen(true)
                return;
            }

            props.setResult({severity:'success', text:props.O.id + ' has been removed!'})
            props.setSnackOpen(true)
            props.setOwners(undefined);

        })
    }

    return (<>
        <TableRow>
            <TableCell width={'50px'}><img src={props.O.imageURL === "" ? "/images/blankPerson.png": props.O.imageURL} alt={'PFP'} height={'50px'}/></TableCell>
            <TableCell width={'120px'}>{props.O.id}</TableCell>
            <TableCell>{props.O.name}</TableCell>
            <TableCell width={'70px'}> 
                {inProgress
                    ? <CircularProgress size={'50px'}/>
                    : <IconButton onClick={handleDeleteOwner} variant='contained'><DeleteIcon/></IconButton>
                }
            </TableCell>
        </TableRow>
    </>)

}

function AddOwnerRow(props) {

    const [newOwner, setNewOwner] = useState("");
    const [inProgress, setInProgress] = useState(false)

    const handleAddOwner=()=>{

        if(newOwner.length !== 5){
            props.setResult({severity:'error', text:'ID new owner is not valid'})
            props.setSnackOpen(true)
            return;
        }

        setInProgress(true)

        //Add newOwner
        fetch(props.baseurl+'?Owner=' + newOwner,GenerateJSONPost(props.Session))
        .then(response=>response.json())
        .then(data=>{

            setInProgress(false)

            if(data.error){
                props.setResult({severity:'error', text:data.reason})
                props.setSnackOpen(true)
                return;
            }

            if(data.errors){
                props.setResult({severity:'error', text:"an unknown, serverside error occurred"})
                props.setSnackOpen(true)
                return;
            }

            props.setResult({severity:'success', text:newOwner + ' has been added!'})
            props.setSnackOpen(true)
            props.setOwners(undefined);

        })
    }

    return (<>
        <TableRow>
            <TableCell colSpan={3}>
                <TextField label="New Owner ID" value={newOwner} onChange={(event) => setNewOwner(event.target.value)} fullWidth
                    style={{ marginTop: "5px", marginBottom: "5px" }} />
            </TableCell>
            <TableCell width={'70px'}> 
                {inProgress
                    ? <CircularProgress size={'50px'}/>
                    : <IconButton onClick={handleAddOwner} variant='contained'><AddIcon/></IconButton>
                }
            </TableCell>
        </TableRow>
    </>)

}


export default function OwnerForm(props) {

    const [owners, setOwners] = useState(undefined)
    const [loading, setLoading] = useState(false)

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const baseurl='/API/Bank/Accounts/'+props.account.id+'/Owners' ;

    if (!owners && !loading) {
        setLoading(true)
        
        fetch(baseurl, GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }
                setOwners(data)
                setLoading(false);
            })


    }

    const handleClosing = () => { props.setOpen(false) }
    
    return (
        <>
            <Dialog maxWidth='sm' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle>Owners of {props.account.name} ({props.account.id})</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        <TableContainer component={Paper} style={props.mini ? {} : { marginTop: '25px' }}>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell colSpan={3}> User </TableCell>
                                        <TableCell> Actions</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        !owners
                                            ? <TableRow> <TableCell colSpan={4} style={{ textAlign: "center" }}><CircularProgress /></TableCell> </TableRow>
                                            : <>
                                                {owners.length === 0
                                                    ? <TableRow><TableCell colSpan={4} style={{ textAlign: "center" }}> Wait this isn't possible </TableCell></TableRow>
                                                    : owners.map(O => { return (<OwnerRow {...props} baseurl={baseurl} O={O} setOwners={setOwners} setSnackOpen={setSnackOpen} setResult={setResult}/>) })
                                                }                                                
                                                <AddOwnerRow {...props} baseurl={baseurl} setOwners={setOwners} setSnackOpen={setSnackOpen} setResult={setResult}/>
                                            </>
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </DialogContentText>
                </DialogContent>
                <DialogActions> <Button onClick={handleClosing}>OK</Button> </DialogActions>
            </Dialog>
            <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />   

        </>
    )

}