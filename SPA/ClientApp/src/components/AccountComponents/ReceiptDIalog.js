import { Dialog, DialogContent, DialogContentText, Grid, Alert } from "@mui/material";
import React from "react";
import { APIURL } from "../../App";


export default function ReceiptDialog(props) {

    const handleClosing = () => { 
        if(props.handleClosing) {props.handleClosing()}
        if(props.setOpen) {props.setOpen(false) }
    }

    return (
        <>
            <Dialog maxWidth='sm' open={props.open} onClose={handleClosing}>
                <DialogContent>
                    <DialogContentText>
                        <Grid container spacing={2}>
                            {
                                props.sent
                                    ? <Grid item xs={12}>
                                        <Alert severity={'success'}>Money sent! This receipt is viewable later by looking at your transactions</Alert>
                                    </Grid>
                                    : <></>

                            }
                            <Grid item xs={12} textAlign={'center'}>
                                <img src={APIURL + '/API/Cert/Transaction/' + props.transactionID} alt={'Receipt for transaction ' + props.transactionID} />
                            </Grid>
                        </Grid>
                    </DialogContentText>
                </DialogContent>

            </Dialog>

        </>
    )

}