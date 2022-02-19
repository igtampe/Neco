import { ExpandMore } from "@mui/icons-material";
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, Divider, IconButton, Menu, MenuItem, CircularProgress} from "@mui/material";
import React, { useState } from "react";
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { GenerateGet } from "../../RequestOptionGenerator";
import AccountForm from "./AccountForm";
import TransactionDisplay from "./TransactionDisplay";
import TransactionsDialog from "./TransactionsDialog";
import SendMonet from "./SendMonet";
import OwnerForm from "./OwnerForm";
import CloseAccountForm from "./CloseAccountForm";

export const Accountheaders = [
    "/AccountHeaders/Personal.png", "/AccountHeaders/Corp.png",
    "/AccountHeaders/Gov.png", "/AccountHeaders/Charity.png"
]

export function FormatAccountID(ID){

    if(!ID || !ID.length || !ID.substring || ID.length !==9) {return ID;}
    var P1=ID.substring(0,3);
    var P2=ID.substring(3,6);
    var P3=ID.substring(6,9);
    
    return(P1 + '-' + P2 + '-' + P3)
    
}

export function AccountRow(props) {

    const [Account, setAccount] = useState(props.account);

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const [editorOpen, setEditorOpen] = useState(false);
    const [transactionsOpen, setTransactionsOpen] = useState(false);
    const [ownersOpen, setOwnersOpen] = useState(false);

    const [sendMoneyOpen, setSendMoneyOpen] = useState(false)

    const [delOpen, setDelOpen] = useState(false);

    const handleClick = (event) => { setAnchorEl(event.currentTarget); };
    const handleClose = () => { setAnchorEl(null); };
    const copyID = () => { navigator.clipboard.writeText(Account.id) }    

    if(Account.closed){ return (<></>)}

    return (
        <>
            <Accordion expanded={props.openAccordion === props.index} onChange={props.handleOpenAccordion(props.index)}>
                <AccordionSummary expandIcon={<ExpandMore />}>
                    <Box sx={{ width: '100%', alignItems: 'flex' }}>
                        <table width='100%'>
                            <tr>
                                <td width={'100px'} rowSpan={2}> <img src={Account.bank && Account.bank.imageURL === "" ? "/bank.png" : Account.bank.imageURL}
                                    alt={'Bank Logo'} height='50px' style={{ marginRight: '20px' }} /> </td>
                                <td> {Account.name} ({FormatAccountID(Account.id)})</td>
                                <td rowSpan={2} style={{ flexGrow: true, textAlign: 'right' }}>
                                    <img src={Accountheaders[Account.incomeType]} alt={'Account Type Header'} height='50px' /></td>
                            </tr>
                            <tr><td><b>{Account.balance.toLocaleString()}p</b></td></tr>
                        </table>
                    </Box>
                </AccordionSummary>
                <AccordionDetails>
                    <Divider />
                    <Box sx={{ display: 'flex', alignItems: 'flex-end' }} style={{ width: "100%" }}>
                                {
                                    props.openAccordion === props.index
                                        ? <TransactionDisplay accountID={Account.id} mini {...props} />
                                        : <></>
                                }
                    </Box><br />
                    <Divider /><br />
                    <Box sx={{ width: '100%', alignItems: 'flex-end', textAlign: 'right' }}>
                        <IconButton onClick={handleClick}><MoreVertIcon /></IconButton>
                        <Button variant='contained' onClick={()=>{setSendMoneyOpen(true)}}>Send Money</Button>
                    </Box>
                </AccordionDetails>
            </Accordion>

            <AccountForm account={Account} setAccount={setAccount} open={editorOpen} setOpen={setEditorOpen} {...props} />
            <TransactionsDialog account={Account} open={transactionsOpen} setOpen={setTransactionsOpen} {...props}/>
            <SendMonet {...props} open={sendMoneyOpen} setOpen={setSendMoneyOpen} account={Account}/>
            <OwnerForm {...props} open={ownersOpen} setOpen={setOwnersOpen} account={Account}/>
            <CloseAccountForm {...props} open={delOpen} setOpen={setDelOpen} setAccount={setAccount} accountID={Account.id}/>

            <Menu anchorEl={anchorEl} open={open} onClose={handleClose} >
                <MenuItem onClick={() => { handleClose(); copyID()}}>Copy Account ID</MenuItem>
                <MenuItem onClick={() => { handleClose(); setTransactionsOpen(true)}}>See More Transactions</MenuItem>
                <MenuItem onClick={() => { handleClose(); setOwnersOpen(true)}}>Manage Owners</MenuItem>
                <MenuItem onClick={() => { handleClose(); setEditorOpen(true) }}>Edit Account Details</MenuItem>
                <Divider />
                <MenuItem disabled={Account.balance > 0} onClick={() => { handleClose(); setDelOpen(true)}}>Close Account</MenuItem>
            </Menu>
        </>
    )

}

export default function AccountDisplay(props) {

    const [loading, setLoading] = useState(false);

    if (!props.accounts && props.setAccounts && !loading) {
        setLoading(true);

        fetch('/API/Bank/Accounts', GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.errors || data.error) { return; }
                props.setAccounts(data)
                setLoading(false)
            })


    }

    const [openAccordion, setOpenAccordion] = useState(-1);
    const handleOpenAccordion = (panel) => (event, isExpanded) => { setOpenAccordion(isExpanded ? panel : -1); };

    if (!props.accounts) { return (<><CircularProgress /></>) }
    if(props.accounts.length === 0){
        return(<div style={{textAlign:'center'}}>Welcome to Neco! It appears you have no accounts. Consider opening one!</div>)
    }

    return (<>
        {props.accounts.map((a, i) => {

            return (<>
                <AccountRow account={a} index={i} openAccordion={openAccordion} handleOpenAccordion={handleOpenAccordion} {...props} />
            </>)

        })
        }

    </>)

}