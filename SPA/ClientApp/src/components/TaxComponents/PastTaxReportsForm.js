import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button, TableContainer, Table, TableHead, TableRow, TableCell, Box, TextField, IconButton, Paper, TableBody, CircularProgress } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet } from '../../RequestOptionGenerator'
import { FileOpen } from "@mui/icons-material";
import TaxReportForm from "./TaxReportForm";

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

    const [reports, setReports] = useState(undefined)
    const [loading, setLoading] = useState(false)

    const [openReport, setOpenReport] = useState(false)

    if (!reports && !loading && props.open) {
        setLoading(true)

        var url = '/API/Taxes/Reports?ID=' + props.account.id

        fetch(url, GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }
                setReports(data)
                setLoading(false);
            })


    }

    const handleClosing = () => { props.setOpen(false) }

    return (
        <>
            <Dialog maxWidth='sm' fullWidth open={props.open} onClose={handleClosing}>
                <DialogTitle> Pick a Tax Report</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        <TableContainer component={Paper}>
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
            <TaxReportForm open={openReport!==false} setOpen={setOpenReport} report={reports[openReport]}/>

        </>
    )

}