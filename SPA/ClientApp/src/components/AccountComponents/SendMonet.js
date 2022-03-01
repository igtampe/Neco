import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, Grid, TextField, Box, IconButton } from "@mui/material";
import React, { useState } from "react";
import AccountPicker from "./AccountPicker";
import { LibraryBooks } from "@mui/icons-material";
import AlertSnackbar from "../AlertSnackbar";
import { GenerateJSONPost } from "../../RequestOptionGenerator";
import ReceiptDialog from "./ReceiptDIalog";
import { APIURL } from "../../App";


export default function SendMonet(props) {

    const [request, setRequest] = useState({
        origin: (props.account ? props.account.id : ''),
        destination: '', amount: 0,
        name: '', certify: false
    })

    const [selfOpen, setSelfOpen] = useState(false);
    const [destOpen, setDestOpen] = useState(false);

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const [receiptOpen, setReceiptOpen] = useState(false)
    const [receiptID, setReceiptID] = useState("");

    const handleClosing = () => {
        clearForm();
        props.setOpen(false)
    }

    const handleReceiptClosing = () => {
        setReceiptOpen(false);
        if (props.setAccounts) { props.setAccounts(undefined) }
        handleClosing()

    }

    const clearForm = () => {
        setRequest({
            origin: (props.account ? props.account.id : ''),
            destination: '', amount: 0,
            name: '', certify: false
        })
    }

    const handleOK = () => {

        if (request.name === '') {
            setResult({ severity: 'danger', text: 'Name cannot be empty!' })
            setSnackOpen(true)
            return;
        }

        if (request.origin === "" || request.destination === "") {
            setResult({ severity: 'danger', text: 'Origin and destination must not be empty!' })
            setSnackOpen(true)
            return;
        }

        if (request.origin === request.destination) {
            setResult({ severity: 'danger', text: 'Cannot send money to the same account!' })
            setSnackOpen(true)
            return;
        }

        if (request.amount < 0) {
            setResult({ severity: 'danger', text: 'You devious little thing. You cannot transact a negative amount' })
            setSnackOpen(true)
            return;
        }

        fetch(APIURL + '/API/Bank/Transaction', GenerateJSONPost(props.Session, request))
            .then(response => response.json())
            .then(data => {
                if (data.error) {
                    setResult({ severity: 'danger', text: data.reason })
                    setSnackOpen(true)
                    return;
                } else if (data.errors) {
                    setResult({ severity: 'danger', text: 'An unknown error occurred!' })
                    setSnackOpen(true)
                    return;
                } else {
                    setResult({ severity: 'success', text: 'Money was successfully sent!' })
                    setSnackOpen(true)

                    setReceiptID(data.id);
                    setReceiptOpen(true)

                    return;
                }
            })


    }

    return (
        <>
            <Dialog maxWidth='xs' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> Send money </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField label="Name" value={request.name} onChange={(event) => setRequest({ ...request, name: event.target.value })} fullWidth
                                    style={{ marginTop: "5px", marginBottom: "5px" }} />
                            </Grid>
                            <Grid item xs={6}>
                                <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                                    <TextField fullWidth label="Origin" value={request.origin} onChange={(event) => { setRequest({ ...request, origin: event.target.value }) }} />
                                    <IconButton style={{ marginBottom: '10px' }} onClick={() => { setSelfOpen(true) }}><LibraryBooks /></IconButton>
                                </Box>
                            </Grid>
                            <Grid item xs={6}>
                                <Box sx={{ display: 'flex', alignItems: 'flex-end' }}>
                                    <TextField fullWidth label="Destination" value={request.destination} onChange={(event) => { setRequest({ ...request, destination: event.target.value }) }} />
                                    <IconButton style={{ marginBottom: '10px' }} onClick={() => { setDestOpen(true) }}><LibraryBooks /></IconButton>
                                </Box>
                            </Grid>
                            <Grid item xs={12}>
                                <TextField label="Amount" value={request.amount} onChange={(event) => setRequest({ ...request, amount: event.target.value })} fullWidth type='number'
                                    inputProps={{ min: 0 }} style={{ marginTop: "5px", marginBottom: "5px" }} />
                            </Grid>
                        </Grid>
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleOK}>Ok</Button>
                    <Button onClick={handleClosing}>Cancel</Button>
                </DialogActions>

                <AccountPicker {...props} open={selfOpen} setOpen={setSelfOpen} setAccountID={(id) => { setRequest({ ...request, origin: id }) }} self />
                <AccountPicker {...props} open={destOpen} setOpen={setDestOpen} setAccountID={(id) => { setRequest({ ...request, destination: id }) }} />

            </Dialog>

            <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

            <ReceiptDialog {...props} open={receiptOpen} handleClosing={handleReceiptClosing} transactionID={receiptID} sent />

        </>
    )

}