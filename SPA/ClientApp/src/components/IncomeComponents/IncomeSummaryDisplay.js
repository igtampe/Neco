import { Card, CardContent, Divider, Grid, Skeleton } from "@mui/material";
import React, { useState } from "react";
import { GenerateGet } from "../../RequestOptionGenerator";
import { Chart, PieSeries, Tooltip } from '@devexpress/dx-react-chart-material-ui';
import { EventTracker } from "@devexpress/dx-react-chart";

function IncomeSummaryCard(props) {

    return (
        <Card sx={{ minWidth: '100%' }}>
            <CardContent>
                <table width={'100%'}>
                    <tr> <td colSpan={2}> <b>{props.type}</b> <Divider style={{ marginBottom: '10px' }} /> </td> </tr>
                    <tr> <td width={'80px'}> Income </td> <td style={{ textAlign: 'right' }}> {props.summary ? <>{props.summary.totalIncome.toLocaleString()}p</> : <Skeleton variant="text" />} </td> </tr>
                    <tr> <td width={'80px'}> Count </td> <td style={{ textAlign: 'right' }}> {props.summary ? <>{props.summary.count.toLocaleString()}</> : <Skeleton variant="text" />} </td> </tr>
                </table>
            </CardContent>
        </Card>
    )

}

function SummaryToChartData(summary) {

    return [
        { type: 'Corporations', income: summary.corporation.totalIncome, count: summary.corporation.count },
        { type: 'Businesses', income: summary.business.totalIncome, count: summary.business.count },
        { type: 'Hotels', income: summary.hotel.totalIncome, count: summary.hotel.count },
        { type: 'Apartments', income: summary.apartment.totalIncome, count: summary.apartment.count },
    ];

}

function IncomeSumamryCharts(props) {

    const GraphToolTip=(info)=>{
        const b = props.data[info.targetItem.point];
        return (<>
            <table>
                <tr>
                    <td style={{textAlign:'center'}}>{b.type}</td>
                </tr>
                <tr>
                    <td> {Number(b.count).toLocaleString()} Item(s)</td>
                </tr>
                <tr>
                    <td>{Number(b.income).toLocaleString()}p/month</td>
                </tr>
            </table>
            </>);
    }

    return (
        <Card sx={{ minWidth: '100%' }}>
            <CardContent>
                <b>Graph</b> <Divider style={{ marginBottom: '10px' }} /> 
                <Chart data={props.data} height={343}>
                    <PieSeries
                        innerRadius={0.7}
                        outerRadius={1}
                        name={'Income'}
                        valueField="income"
                        argumentField="type"
                    />
                    <PieSeries
                        innerRadius={0.4}
                        outerRadius={0.7}
                        name={'Count'}
                        valueField="count"
                        argumentField="type"
                    />
                    <EventTracker />
                    <Tooltip contentComponent={GraphToolTip}/>
                </Chart>
            </CardContent>
        </Card>
    )

}

export default function IncomeSummaryDisplay(props) {

    const [loading, setLoading] = useState(undefined);

    //Discard data if we changed tabs
    if (props.summary && !props.open) { props.setSummary(undefined); }


    if (props.open && props.account && !props.summary && !loading) {

        setLoading(true)
        fetch('/API/Income?AccountID=' + props.account.id, GenerateGet(props.Session))
            .then(response => response.json())
            .then(data => {

                if (data.error || data.errors) { return; } //oops

                props.setSummary(data)
                setLoading(false);

            })

    }

    return (<>
        <Grid container spacing={2}>
            {props.Vertical && props.summary ?
                <Grid item xs={12}>
                    <IncomeSumamryCharts data={SummaryToChartData(props.summary)} />
                </Grid> : <></>}
            <Grid item xs={props.Vertical ? 12 : 7}>
                <Grid container spacing={2}>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Corps & Airlines'} corporation summary={props.summary ? props.summary.corporation : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Businesses'} business summary={props.summary ? props.summary.business : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Hotels'} hotel summary={props.summary ? props.summary.hotel : undefined} /></Grid>
                    <Grid item xs={6}><IncomeSummaryCard {...props} type={'Apartments'} apartment summary={props.summary ? props.summary.apartment : undefined} /></Grid>
                    <Grid item xs={12}><IncomeSummaryCard {...props} type={'Totals'} summary={props.summary ? props.summary.total : undefined} /></Grid>
                </Grid>
            </Grid>
            {!props.Vertical && props.summary ?
                <Grid item xs={5}>
                    <IncomeSumamryCharts data={SummaryToChartData(props.summary)} />
                </Grid> : <></>}
        </Grid>
    </>)
}