import { Card, CardContent, Divider, Grid, Skeleton } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet } from "../../RequestOptionGenerator";

function IncomeSummaryCard(props) {

    return (
        <Card sx={{ minWidth: '100%' }}>
            <CardContent>
                <table width={'100%'}>
                    <tr> <td colSpan={2}> <b>{props.type}</b> <Divider style={{marginBottom:'10px'}}/> </td> </tr>
                    <tr> <td width={'80px'}> Income </td> <td style={{textAlign:'right'}}> {props.summary ? <>{props.summary.totalIncome.toLocaleString()}p</> : <Skeleton variant="text" />} </td> </tr>
                    <tr> <td width={'80px'}> Count </td> <td style={{textAlign:'right'}}> {props.summary ? <>{props.summary.count.toLocaleString()}</> : <Skeleton variant="text" />} </td> </tr>
                </table>
            </CardContent>
        </Card>
    )

}

export default function IncomeSummaryDisplay(props) {

    const [loading, setLoading] = useState(undefined);

    //Discard data if we changed tabs
    if(props.summary && !props.open){ props.setSummary(undefined); }
    

    if(props.open && props.account && !props.summary && !loading ){

        setLoading(true)
        fetch('/API/Income?AccountID='+props.account.id,GenerateGet(props.Session))
        .then(response=>response.json())
        .then(data=>{

            if(data.error || data.errors){return;} //oops
            
            props.setSummary(data)
            setLoading(false);

        })

    }

    return (<>
        <Grid container spacing={2}>
            {props.Vertical ?
                <Grid item xs={12}>
                    The pie chart will go here
                </Grid>: <></>}
            <Grid item xs={props.Vertical ? 12 : 7}>
                <Grid container spacing={2}>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Airlines'} airline summary={props.summary ? props.summary.airline : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Corporations'} corporation summary={props.summary ? props.summary.corporation : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Businesses'} business summary={props.summary ? props.summary.business : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Hotels'} hotel summary={props.summary ? props.summary.hotel : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Apartments'} apartment summary={props.summary ? props.summary.apartment : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Totals'} summary={props.summary ? props.summary.total : undefined} /></Grid>
                </Grid>
            </Grid>
            {!props.Vertical ?
                <Grid item xs={5}>
                    The pie chart will go here
                </Grid> : <></>}
        </Grid>
    </>)
}