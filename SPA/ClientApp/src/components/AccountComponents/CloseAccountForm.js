import React, { useState } from "react";
import { Button, CircularProgress, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, TextField } from "@mui/material";
import AlertSnackbar from "../AlertSnackbar";
import { GenerateDelete } from '../../RequestOptionGenerator'

export default function CloseAccountForm(props) {

    const [confirmID, setConfirmID] = useState("")
    const [inProgress, setInProgress] = useState(false)

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const handleClosing = () => { props.setOpen(false) }
    const RealDelete = () => {

        if (confirmID !== props.accountID) {
            setResult({ severity: 'error', text: 'Account ID does not match' })
            setSnackOpen(true)
            return;
        }

        setInProgress(true)

        //Add newOwner
        fetch(APIURL + '/API/Bank/Accounts/' + props.accountID, GenerateDelete(props.Session))
            .then(response => response.json())
            .then(data => {
                
                setInProgress(false);

                if (data.error) {
                    setResult({ severity: 'error', text: data.reason })
                    setSnackOpen(true)
                    return;
                }

                if (data.errors) {
                    setResult({ severity: 'error', text: "an unknown, serverside error occurred" })
                    setSnackOpen(true)
                    return;
                }

                setResult({ severity: 'success', text: props.accountID + ' has been closed' })
                setSnackOpen(true)
                props.setAccount(data)

                handleClosing();

            })
    }

    return (
        <React.Fragment>
            <Dialog maxWidth="sm" fullwidth open={props.open} onClose={handleClosing}>
                <DialogTitle>Are you sure you want to close this account?</DialogTitle>
                <DialogContent>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <DialogContentText>
                                You and all other owners of this account will lose access to this account. To proceed, please type the ID of the account <b>({props.accountID})</b>:
                            </DialogContentText>
                        </Grid>
                        <Grid item xs={12}>
                            <TextField fullWidth label="Account ID" value={confirmID} onChange={(event) => { setConfirmID(event.target.value) }} />
                        </Grid>
                        <Grid item xs={12} style={{ textAlign:'center' }}>
                            <Button disabled={props.accountID !== confirmID || inProgress} color={'secondary'} onClick={RealDelete} variant="contained">
                                {
                                    inProgress
                                        ? <CircularProgress color={'secondary'}/>
                                        : <>Close this account</>
                                }
                            </Button>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                </DialogActions>
            </Dialog>
            <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />
        </React.Fragment>
    );

}
