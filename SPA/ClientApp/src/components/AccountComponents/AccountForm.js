import { TextField, CircularProgress, Divider, Grid, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, FormControl, InputLabel, Select, MenuItem, Checkbox, FormControlLabel } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet, GenerateJSONPost, GenerateJSONPut } from "../../RequestOptionGenerator";
import { IncomeTypeSelect } from "../AdminComponents/JurisdictionComponents/BracketEditor";
import { JurisdictionAutoComplete } from "../AdminComponents/JurisdictionComponents/JurisdictionAutocomplete";
import AlertSnackbar from "../AlertSnackbar";

export function BankSelect(props){

    const [banks, setBanks] = useState(undefined)
    const [loading, setLoading] = useState(false);

    if (!banks && !loading) { 
        setLoading(true)

        var URL = '/API/Bank'

        fetch(URL, GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
            .then(response => { return (response.json()) })
            .then(data => {
                //if there was an error then oops
                if (data.error) { return; }

                setBanks(data)
                setLoading(false)
            })

    }

    if(!banks){ return(<CircularProgress/>) }

    return (
        <>
            <FormControl fullWidth style={{ margintop: "15px" }} disabled={props.type===0}>
                <InputLabel fullWidth>Bank</InputLabel>
                <Select disabled={props.disabled} fullWidth label="Label" value={props.bankID} onChange={(event) => { props.setBankID(event.target.value) }}>
                    { banks.map( B=>{ return( <MenuItem value={B.id}>{B.id}: {B.name}</MenuItem> )}) }
                </Select></FormControl>
        </>

    )

}

export default function AccountForm(props){

    const [request,setRequest] = useState({
        name:"", publiclyListed: false,
        address:'', jurisdictionID:'',
        bankID:'', incomeType:0
    })

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const [populated,setPopulated] = useState(false);

    if(props.account && !populated && props.open){
        setPopulated(true);
        setRequest({
            name:props.account.name,publiclyListed:props.account.publiclyListed,
            address:props.account.address, jurisdictionID:(props.account.jurisdiction ? props.account.jurisdiction.id : ''),
            bankID:(props.account.bank ? props.account.bank.id : ''), incomeType:props.account.incomeType
        })

    }

    const clearForm = () => {
        setRequest({ name:"", publiclyListed: false,
            address:'', jurisdictionID:'',
            bankID:'', incomeType:0 })

        setPopulated(false);
    }

    const handleClosing = () => { 
        clearForm();
        props.setOpen(false) 
    }

    const handleOK = () => {

        if(request.bankID === ""){
            setResult({severity:'danger', text:'Please select a bank'})
            setSnackOpen(true)
            return;
        }

        if(request.jurisdictionID === ""){
            setResult({severity:'danger', text:'Please select a jurisdiction'})
            setSnackOpen(true)
            return;
        }

        if(request.name===''){
            setResult({severity:'danger', text:'Name cannot be empty!'})
            setSnackOpen(true)
            return;
        }

        var RequestOptions;
        var URL = '/API/Bank/Accounts'
        if(props.account){
            RequestOptions=GenerateJSONPut(props.Session,request)
            URL=URL+'/'+props.account.id
        } else {
            RequestOptions=GenerateJSONPost(props.Session,request)
        }

        fetch(URL,RequestOptions)
        .then(response=>response.json())
        .then(data=>{
            if(data.error) {
                setResult({severity:'danger', text:data.reason})
                setSnackOpen(true)
                return;
            } else if (data.errors) {
                setResult({severity:'danger', text:'An unknown error occurred!'})
                setSnackOpen(true)
                return;
            } else {
                setResult({severity:'success', text:'Account ' + data.name + ' (' + data.id + ') has been ' + (props.account ? 'updated' : 'created') + '!'})
                setSnackOpen(true)

                //console.log(props)
                if(props.setAccount) {props.setAccount({...props.account,data})}
                if(props.setAccounts) {
                    //console.log("adios")
                    props.setAccounts(undefined)
                }
                handleClosing()

                return;
            }
        })

    }

return(
    <>
    
    <Dialog maxWidth='sm' fullWidth open={props.open} onClose={handleClosing}>
        <DialogTitle> {props.account ? 'Edit' : 'Open' } an Account </DialogTitle>
        <DialogContent>
            <DialogContentText>

                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <TextField label="Name" value={request.name} onChange={(event)=> setRequest({ ...request, name:event.target.value})} fullWidth 
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                    </Grid>
                    <Grid item xs={6}>
                        <BankSelect disabled={props.account} bankID={request.bankID} setBankID={(bankID)=>{setRequest({...request,bankID:bankID})}}/>
                    </Grid>
                    <Grid item xs={6}>
                        <IncomeTypeSelect type={request.incomeType} setType={(type)=>{setRequest({...request,incomeType:type})}}/>
                    </Grid>
                    <Grid item xs={12}> <Divider/> </Grid>
                    <Grid item xs={6}>
                        <TextField label="Address" value={request.address} onChange={(event) => setRequest({...request,address:event.target.value})} fullWidth
                                style={{ marginTop: "5px", marginBottom: "5px" }} />
                    </Grid>
                    <Grid item xs={6}>
                        <JurisdictionAutoComplete jurisdictionID={request.jurisdictionID} setJurisdictionID={(id)=>{setRequest({...request,jurisdictionID:id})}}/>
                    </Grid>
                    <Grid item xs={12}> <Divider/> </Grid>
                    <Grid item xs={12}> 
                        <FormControlLabel label="Publicly list this account on the Neco Directory" control={
                            <Checkbox checked={request.publiclyListed} onClick={()=>setRequest({...request,publiclyListed:!request.publiclyListed})}/> } 
                        />
                    </Grid>
                    
                </Grid>
            </DialogContentText>
        </DialogContent>
        <DialogActions>
            <Button onClick={handleOK}>Ok</Button>
            <Button onClick={handleClosing}>Cancel</Button>
        </DialogActions>

        <AlertSnackbar open={SnackOpen} setOpen={setSnackOpen} result={result} />

    </Dialog>

    </>
)

}