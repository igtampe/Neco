import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from "@mui/material";
import React from "react";
import TransactionDisplay from "./TransactionDisplay";


export default function TransactionsDialog(props){

    const handleClosing = () => {  props.setOpen(false)  }

return(
    <>
        <Dialog maxWidth='lg' fullWidth open={props.open} onClose={handleClosing}>
        <DialogTitle> Transactions for {props.account ? props.account.name : ''} </DialogTitle>
        <DialogContent>
            <DialogContentText>
                <TransactionDisplay accountID={props.account ? props.account.id : ''} {...props}/>                
            </DialogContentText>
        </DialogContent>
        <DialogActions>
            <Button onClick={handleClosing}>Ok</Button>
        </DialogActions>

    </Dialog>

    </>
)

}