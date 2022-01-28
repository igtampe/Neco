import React, { useState } from "react";
import { Box, TextField, IconButton, Table, TableContainer, TableHead, TableRow, TableCell, TableBody, CircularProgress, Paper, Checkbox, Dialog, DialogTitle, DialogContent, DialogActions, Button } from '@mui/material'
import SearchIcon from '@mui/icons-material/Search'
import { GenerateJSONPut } from "../../../RequestOptionGenerator";
import AlertSnackbar from "../../AlertSnackbar";
import {Password} from '@mui/icons-material'

function ResetPassForm(props){

    const [newPass,setNewPass] = useState('')

    const clearForm = () => { setNewPass('') }
    const handleClosing = () => { 
        clearForm();
        props.setOpen(false) 
    }

    const handleOK = () => {

        if (newPass === "") {
            props.setResult({ severity: "danger", text: "New password must not be empty" })
            props.setSnackOpen(true);
            return;
        }

        //Grab the ID and pin and create a tiny itty bitty object
        const requestOptions = GenerateJSONPut(props.Session,{ "new": newPass})
        
        fetch('API/Users/' + props.user.id + '/reset', requestOptions)
            .then(response => {
                return response.text()
            }).then(data => {
                if (data !== "") {
                    props.setResult({ severity: "danger", text: data})
                    props.setSnackOpen(true);
                } else {
                    //s u c c e s s
                    props.setResult({ severity: "success", text: "Password changed successfully" })
                    props.setSnackOpen(true);
                    handleClosing();
                }
            })



    }


return(
    <>
    
    <Dialog maxWidth='xs' fullWidth open={props.open} onClose={handleClosing}>
        <DialogTitle> Reset password for {props.user.name} ({props.user.id}) </DialogTitle>
        <DialogContent>
            <TextField label="ID" value={props.user.id} fullWidth style={{ marginTop: "5px", marginBottom: "5px" }} />
            <TextField label="New Password" value={newPass} onChange={(event)=> setNewPass(event.target.value)} fullWidth type='password'
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
        </DialogContent>
        <DialogActions>
            <Button onClick={handleOK}>Ok</Button>
            <Button onClick={handleClosing}>Cancel</Button>
        </DialogActions>
    </Dialog>

    </>
    )
}

function UserRow(props) {

    //This is basically a component
    const [user, setUser] = useState(props.U)
    const [updating, setUpdating] = useState(false)
    const [resetOpen, setResetOpen] = useState(false)

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const updateUser = (newvals) => {

        setUpdating(true)

        var CRR = {
            isAdmin: user.isAdmin, isGov: user.isGov,
            isSDC: user.isSDC, isUploader: user.isUploader
        }

        CRR = { ...CRR, ...newvals };

        fetch('/API/Users/' + user.id + '/Roles', GenerateJSONPut(props.Session, CRR))
            .then(response => response.json())
            .then(data => {

                setUpdating(false);

                if (data.error) {
                    setResult({ severity: 'danger', text: 'Could not update roles: ' + data.reason })
                    setSnackOpen(true);
                    return;
                } else if (data.errors) {
                    setResult({ severity: 'danger', text: 'An unknown error occurred' })
                    setSnackOpen(true);
                    return;
                }

                setResult({ severity: 'success', text: 'Updated roles successfully!' })
                setSnackOpen(true);
                setUser(data);

            })

    }

    return (
        <>
            <TableRow>
                <TableCell width={'50px'}>
                    <a href={props.U.imageURL === "" ? "/Images/Blankperson.png" : props.U.imageURL}>
                        <img alt={'Profile'} src={props.U.imageURL === "" ? "/Images/Blankperson.png" : props.U.imageURL} height={'50px'} /></a>
                </TableCell>
                <TableCell width={'90px'}>{props.U.id}</TableCell>
                <TableCell>{props.U.name}</TableCell>
                {
                    updating
                        ? <>
                            <TableCell colSpan={5} style={{textAlign:'center'}}> <CircularProgress/>  </TableCell>
                        
                        </> : <>
                            <TableCell width={'70px'}> <Checkbox checked={user.isAdmin} onClick={() => { updateUser({ isAdmin: !user.isAdmin }) }} /> </TableCell>
                            <TableCell width={'70px'}> <Checkbox checked={user.isGov} onClick={() => { updateUser({ isGov: !user.isGov }) }} /> </TableCell>
                            <TableCell width={'70px'}> <Checkbox checked={user.isSDC} onClick={() => { updateUser({ isSDC: !user.isSDC }) }} /> </TableCell>
                            <TableCell width={'70px'}> <Checkbox checked={user.isUploader} onClick={() => { updateUser({ isUploader: !user.isUploader }) }} /></TableCell>
                            <TableCell width={'70px'}> <IconButton onClick={()=>setResetOpen(true)}> <Password/></IconButton></TableCell>
                        </>
                }
            </TableRow>

            <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />
            <ResetPassForm open={resetOpen} setOpen={setResetOpen} user={user} setResult={setResult} setSnackOpen={setSnackOpen} {...props}/>

        </>
    )


}

export default function BankDisplay(props) {

    const [query, setQuery] = useState("");

    const [users, setUsers] = useState(undefined)
    const [loading, setLoading] = useState(false);

    const startSearch = (event) => { setUsers(undefined) }

    //OK now 
    if (!users && !loading) {

        setLoading(true)

        var URL = '/API/Users/Dir'
        if (query !== "") { URL = URL + '?Query=' + query }

        fetch(URL).then(response => response.json())
            .then(data => {

                //if there was an error then oops
                if (data.error) { return; }

                setUsers(data)
                setLoading(false)

            })

    }


    return (
        <React.Fragment>
            <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                <TextField fullWidth label="Search" value={query} onChange={(event) => { setQuery(event.target.value) }} />
                <IconButton onClick={startSearch} style={{ marginLeft: '10px', marginBottom: '7px' }}><SearchIcon /></IconButton>
            </Box>
            <TableContainer component={Paper} style={{ marginTop: '25px' }}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>PFP</TableCell>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Admin</TableCell>
                            <TableCell>Gov</TableCell>
                            <TableCell>SDC</TableCell>
                            <TableCell>Upload</TableCell>
                            <TableCell>Reset</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            !users ?
                                <TableRow>
                                    <TableCell colSpan={9} style={{ textAlign: "center" }}><CircularProgress /></TableCell>
                                </TableRow> : <>{

                                    users.length === 0 ?
                                        <TableRow>
                                            <TableCell colSpan={9} style={{ textAlign: "center" }}>No Users found</TableCell>
                                        </TableRow> : <>{users.map(U => <UserRow U={U} setUsers={setUsers} Session={props.Session} />)}</>
                                }
                                </>
                        }
                    </TableBody>
                </Table>
            </TableContainer>

        </React.Fragment>
    );

}
