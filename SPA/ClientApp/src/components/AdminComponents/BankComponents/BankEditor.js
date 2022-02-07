import React, { useState } from "react";
import {
    Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField
} from "@mui/material";
import AlertSnackbar from "../../AlertSnackbar";
import PicturePicker from "../../PicturePicker";

export default function BankEditor(props) {

    const [id, setID] = useState("")
    const [name, setName] = useState("");
    const [imageURL, setImageURL] = useState("");

    const [pickerOpen, setPickerOpen] = useState(false)
    const [populated, setPopulated] = useState(false);

    const [result, setResult] = useState({ severity: "success", text: "idk"})
    const [SnackOpen, setSnackOpen] = useState(false);

    if (props.bank && !populated && props.open) {
        //Populate
        setPopulated(true);
        setName(props.bank.name)
        setImageURL(props.bank.imageURL)
        setID(props.bank.id)
    }

    const ClearForm = () => {
        setName("")
        setImageURL("")
        setID("")

        setPopulated(false)
    }

    const handleClosing = (event) => {
        props.setOpen(false)
        ClearForm();
    }

    const handleOK = (event) => {

        if (name === "" || id==="") {
            setResult({ severity: "danger", text: "Name and ID cannot be empty!" })
            setSnackOpen(true);
            return;
        }

        //Send the request 
        var requestOptions = { 
            method: (props.bank ? 'PUT' : 'POST'),
            headers: { 'Content-Type': 'application/json', 'SessionID': props.Session },
            body: JSON.stringify({ name: name, imageURL: imageURL })        
        };

        var url= "API/Bank/" + id;
        
        console.log(requestOptions.body)

        fetch(url,requestOptions)
            .then(response => { return response.json() })
            .then(data => {
                if (data.error) {

                    setResult({
                        severity: "danger",
                        text: "Could not " + (props.brank ? "update" : "create") + " this bank: " + data.reason
                    })

                    setSnackOpen(true);

                } else {
                    setResult({
                        severity: "success",
                        text: name + " has been " + (props.bank ? "updated" : "created") + "!"
                    })

                    if (props.setBanks) { props.setBanks(undefined) }
                    if (props.setBank) { props.setBank(data) }

                    setSnackOpen(true);

                    handleClosing();
                }
            })

    }

    return (
        <React.Fragment>
            <Dialog fullWidth maxWidth="md" open={props.open} onClose={handleClosing}>
                <DialogTitle>{props.bank ? 'Edit a Bank' : 'Create a new Bank'}</DialogTitle>
                <DialogContent>
                    <table style={{ width: "100%" }}>
                        <tr>
                            <td><TextField label="ID (5 Chars)" value={id} onChange={(event) => setID(event.target.value)} fullWidth disabled={props.bank} inputProps={{ maxLength: 5 }}
                                    style={{ marginTop: "5px", marginBottom: "5px" }} /></td>
                            <td rowSpan="2" style={{ width: "135px" }}>
                                <Button onClick={() => setPickerOpen(true)}>
                                    <img src={imageURL === "" ? "/Bank.png" : imageURL} alt="Flag" height="100px" style={{ marginLeft: "25px", marginRight: "10px" }} />
                                </Button>
                            </td></tr><tr>
                            <td><TextField label="Name" value={name} onChange={(event) => setName(event.target.value)} fullWidth
                                    style={{ marginTop: "5px", marginBottom: "5px" }} /></td>
                        </tr>
                    </table>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleOK}>OK</Button>
                    <Button onClick={handleClosing}>Cancel</Button>
                </DialogActions>
            </Dialog>

            <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

            <PicturePicker open={pickerOpen} setOpen={setPickerOpen} imageURL={imageURL} setImageURL={setImageURL} defaultImage={"/bank.png"} />

        </React.Fragment>
    );

}
