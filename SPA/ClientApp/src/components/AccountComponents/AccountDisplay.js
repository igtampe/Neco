import { ExpandMore } from "@mui/icons-material";
import { Accordion, AccordionDetails, AccordionSummary, Box, Button, Divider, IconButton, Menu, MenuItem, CircularProgress} from "@mui/material";
import React, { useState } from "react";
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { GenerateGet } from "../../RequestOptionGenerator";
import AccountForm from "./AccountForm";
import TransactionDisplay from "./TransactionDisplay";
import TransactionsDialog from "./TransactionsDialog";
import SendMonet from "./SendMonet";

const Accountheaders = [
    "/AccountHeaders/Personal.png", "/AccountHeaders/Corp.png",
    "/AccountHeaders/Gov.png", "/AccountHeaders/Charity.png"
]

export function AccountRow(props) {

    const [Account, setAccount] = useState(props.account);

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const [editorOpen, setEditorOpen] = useState(false);
    const [transactionsOpen, setTransactionsOpen] = useState(false);
    const [ownersOpen, setOwnersOpen] = useState(false);
    const [receiptOpen, setReceiptOpen] = useState(false);

    const [sendMoneyOpen, setSendMoneyOpen] = useState(false)

    const [delOpen, setDelOpen] = useState(false);
    const [delInProgress, setDelInProgress] = useState(false);

    const handleClick = (event) => { setAnchorEl(event.currentTarget); };
    const handleClose = () => { setAnchorEl(null); };

    return (
        <>
            <Accordion expanded={props.openAccordion === props.index} onChange={props.handleOpenAccordion(props.index)}>
                <AccordionSummary expandIcon={<ExpandMore />}>
                    <Box sx={{ width: '100%', alignItems: 'flex' }}>
                        <table width='100%'>
                            <tr>
                                <td width={'100px'} rowSpan={2}> <img src={Account.bank && Account.bank.imageURL === "" ? "/bank.png" : Account.bank.imageURL}
                                    alt={'Bank Logo'} height='50px' style={{ marginRight: '20px' }} /> </td>
                                <td> {Account.name} ({Account.id})</td>
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

            <Menu anchorEl={anchorEl} open={open} onClose={handleClose} >
                <MenuItem onClick={() => { handleClose(); setTransactionsOpen(true)}}>See More Transactions</MenuItem>
                <MenuItem onClick={() => { handleClose(); }}>Manage Owners</MenuItem>
                <MenuItem onClick={() => { handleClose(); setEditorOpen(true) }}>Edit Account Details</MenuItem>
                <Divider />
                <MenuItem onClick={() => { handleClose(); }}>Close Account</MenuItem>
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

    return (<>
        {props.accounts.map((a, i) => {

            return (<>
                <AccountRow account={a} index={i} openAccordion={openAccordion} handleOpenAccordion={handleOpenAccordion} {...props} />
            </>)

        })
        }

    </>)

}