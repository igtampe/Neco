import { Dialog, DialogTitle, DialogContent, DialogContentText, TableContainer, Table, TableHead, TableRow, TableCell, IconButton, Paper, TableBody, CircularProgress, Alert } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet } from '../../RequestOptionGenerator'
import { FileOpen } from "@mui/icons-material";
import TaxReportForm from "./TaxReportForm";
import { APIURL } from "../../App";

function TaxReportRow(props) {

    var D = new Date(props.A.dateGenerated)

    return (<>
        <TableRow>
            <TableCell width={'180px'}>{D.toLocaleString()}</TableCell>
            <TableCell>{props.A.grandTotalIncome.toLocaleString()}p</TableCell>
            <TableCell>{props.A.grandTotalTax.toLocaleString()}p</TableCell>
            <TableCell width={'70px'}> <IconButton onClick={() => { props.setOpenReport(props.I) }} variant='contained'><FileOpen/></IconButton></TableCell>
        </TableRow>
    </>)


}


export default function PastTaxReportsForm(props) {

    const [reports, setReports] = useState(false)
    const [loading, setLoading] = useState(false)

    const [openReport, setOpenReport] = useState(false)

    if (!reports && !loading && props.open) {
        setLoading(true)

        var url = APIURL + '/API/Taxes/Reports?ID=' + props.account.id

        fetch(url, GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }
                setReports(data)
                setLoading(false);
            })


    }

    if(!props.open && reports){ setReports(false) }

    const handleClosing = () => { props.setOpen(false) }

    return (
        <>
            <Dialog maxWidth='sm' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> Pick a Tax Report</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        <Alert severity="warning">Tax reports older than 3 months are deleted automatically by Neco. 
                        If you want to preserve the data, consider downloading the Text or CSV copies of old reports.</Alert>

                        <TableContainer component={Paper} style={{marginTop:'15px'}}>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Date Generated</TableCell>
                                        <TableCell>Total Income</TableCell>
                                        <TableCell>Total Tax</TableCell>
                                        <TableCell></TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {
                                        !reports
                                            ? <TableRow> <TableCell colSpan={4} style={{ textAlign: "center" }}><CircularProgress /></TableCell> </TableRow>
                                            : <>
                                                {reports.length === 0
                                                    ? <TableRow><TableCell colSpan={4} style={{ textAlign: "center" }}> No reports were found </TableCell></TableRow>
                                                    : reports.map((A,I) => { return (<TaxReportRow A={A} I={I} setOpenReport={setOpenReport} />) })
                                                }
                                            </>
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </DialogContentText>
                </DialogContent>
            </Dialog>
            <TaxReportForm {...props} open={openReport!==false} setOpen={setOpenReport} report={reports[openReport]}/>

        </>
    )

}