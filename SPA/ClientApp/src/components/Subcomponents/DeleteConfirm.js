import React from "react";
import {Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle} from "@mui/material";

export default function DeleteConfirm(props) {

    const handleClosing=()=>{ props.setOpen(false) }
    const RealDelete =()=>{

        handleClosing();
        props.delete()

    }

    return (
        <React.Fragment>
            <Dialog maxWidth="sm" open={props.open} onClose={handleClosing}>
                <DialogTitle>Are you sure you want to delete?</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        {props.children}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={RealDelete}>Yes</Button>
                    <Button onClick={handleClosing}>No</Button>
                </DialogActions>
            </Dialog>
        </React.Fragment>
    );

}
