import { Dialog, DialogContent, DialogContentText, Grid, Divider, CircularProgress, Card, CardContent, DialogTitle, Button } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet } from "../../RequestOptionGenerator";

function ReportCard(props) {

    return (
        <Card sx={{ minWidth: '100%' }}>
            <CardContent>
                <b>{props.name}</b> <Divider style={{ marginBottom: '10px' }} /> <br/>
                <h4 style={{textAlign:'center'}}>{props.val.toLocaleString()}p</h4>
            </CardContent>
        </Card>
    )

}

export default function TaxReportForm(props) {

    const [report, setReport] = useState(props.report)
    const [loading, setLoading] = useState(false);

    if (!report && !loading && props.open) {
        
        //if there's one specified use that one
        if(props.report){
            setReport(props.report)
            return;
        }
        
        //else its tiem to generate

        setLoading(true)

        fetch('/API/Taxes/GenerateReport?AccountID=' + props.account.id, GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {
                if (data.error || data.errors) { return; }

                setReport(data)
                setLoading(false)
            })
    }

    const downloadText=(text,filename)=>{
        const element = document.createElement("a");
        const file = new Blob([text], {type: 'text/plain'});
        element.href = URL.createObjectURL(file);
        element.download = filename ? filename : "File.txt";
        document.body.appendChild(element); // Required for this to work in FireFox. Good god though oops
        element.click();

    }

    if (report && !props.open) { setReport(false); }

    const handleClosing = () => {
        if (props.handleClosing) { props.handleClosing() }
        if (props.setOpen) { props.setOpen(false) }
    }

    return (
        <>
            <Dialog maxWidth='md' open={props.open} onClose={handleClosing}>
                <DialogTitle>
                    {report
                        ?<>Report generated on {new Date(report.dateGenerated).toLocaleString()}</>
                        :<>Generating report...</>
                    }
                </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        {
                            !report
                                ? <div style={{ textAlign: 'center' }}><CircularProgress /></div>
                                : <>
                                    <Grid container spacing={2}>
                                        <Grid item xs={4}>
                                            <ReportCard name={'Static Income'} val={report.staticIncome}/>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <ReportCard name={'Extra Income'} val={report.extraIncome}/>
                                        </Grid>
                                        <Grid item xs={4}>
                                            <ReportCard name={'Taxable Extra Income'} val={report.extraIncomeTaxable}/>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <ReportCard name={'Grand Total Income'} val={report.grandTotalIncome}/>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <ReportCard name={'Grand Total Tax'} val={report.grandTotalTax}/>
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Card sx={{ minWidth: '100%'}}>
                                                <CardContent>
                                                    <div style={{height:'300px', overflowY:'auto', fontFamily:'monospace'}}>
                                                        {report.textReport.split('\n').map(str => <>{str}<br/></>)}
                                                    </div>
                                                </CardContent>
                                            </Card>
                                        </Grid>
                                        <Grid item xs={2} textAlign={'center'}>
                                        </Grid>
                                        <Grid item xs={4} textAlign={'center'}>
                                            <Button onClick={()=>{downloadText(report.textReport,props.account.id + '_TaxReport.txt')}} variant='contained'>Download Text Report</Button>
                                        </Grid>
                                        <Grid item xs={4} textAlign={'center'}>
                                            <Button onClick={()=>{downloadText(report.csvReport,props.account.id + '_TaxReport.csv')}} variant='contained'>Download CSV Report</Button>
                                        </Grid>
                                        <Grid item xs={2} textAlign={'center'}>
                                        </Grid>
                                    </Grid>
                                </>
                        }
                    </DialogContentText>
                </DialogContent>

            </Dialog>

        </>
    )

}