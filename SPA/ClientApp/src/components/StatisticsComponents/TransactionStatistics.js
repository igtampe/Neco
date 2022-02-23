//Here transactions totals and by month

import React, { useState } from "react";
import CommonStatisticsDisplay from "./CommonStatisticsDisplay";
import { Skeleton } from '@mui/material'
import { Chart, BarSeries, Tooltip, Title, Legend, ArgumentAxis, ValueAxis } from '@devexpress/dx-react-chart-material-ui';
import { EventTracker, Stack } from "@devexpress/dx-react-chart";

const Months = ['','Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec']

function TransactionOverallStatistics(props) {

    const [stats, setStats] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!stats && !loading) {

        setLoading(true)

        fetch('/API/Statistics/Transactions')
            .then(R => R.json())
            .then(data => {

                if (data.error || data.errors) { return; }
                setStats(data)
                setLoading(false)

            })

    }

    return (<>
        <table style={{marginLeft:'auto', marginRight:'auto'}}>
            <tr>
                <td>Total Transactions:</td>
                <td style={{paddingLeft:'50px'}}>{stats ? stats.count.toLocaleString() : <Skeleton variant="text" />}</td>
            </tr>
            <tr>
                <td>Total Transaction Volume:</td>
                <td style={{paddingLeft:'50px'}}>{stats ? stats.sum.toLocaleString() + 'p' : <Skeleton variant="text" />}</td>
            </tr>
        </table>
    </>)

}

function TransactionMonthlyCountStatistics(props) {
    if (!props.data) {
        return (<>
            <Skeleton variant='rectangular' width={'100%'} height={'250px'} />
        </>)
    }

    var ConvertedData = props.data.map(a => {
        return ({
            month: Months[a.month] + " " + a.year,
            count: a.count, sum: a.sum
        })
    }).reverse()

    const CountTooltip = (info) => {
        return (<>
            {Number(info.text).toLocaleString()} Transactions
        </>);
    }
    
    return (<>
        <Chart data={ConvertedData}>
            <ArgumentAxis/>

            <BarSeries
                valueField="count"
                argumentField="month"
                name='Count'
            />
            <ValueAxis />
            <Stack />
            <EventTracker />
            <Tooltip  contentComponent={CountTooltip} />
            <Title text="Transactions Month by Month" />
        </Chart>
    </>)

}

function TransactionMonthlyVolumeStatistics(props) {

    if (!props.data) {
        return (<>
            <Skeleton variant='rectangular' width={'100%'} height={'250px'} />
        </>)
    }

    var ConvertedData = props.data.map(a => {
        return ({
            month: Months[a.month] + " " + a.year,
            count: a.count, sum: a.sum
        })
    }).reverse()

    const VolumeToolTip = (info) => {
        return (<>
            {Number(info.text).toLocaleString()}p
        </>);
    }
    
    return (<>
        <Chart data={ConvertedData}>
            <ArgumentAxis />

            <BarSeries
                valueField="sum"
                argumentField="month"
                name='Volume'
            />
            <ValueAxis />
            <Stack />
            <EventTracker />
            <Tooltip contentComponent={VolumeToolTip} />
            <Title enabled text="Transaction Volume Month by Month" />
        </Chart>
    </>)

}

export default function TransactionStatistics(props) {

    const [monthly, setMonthly] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!monthly && !loading) {

        setLoading(true)

        fetch('/API/Statistics/Transactions/Monthly')
            .then(R => R.json())
            .then(data => {

                if (data.error || data.errors) { return; }
                setMonthly(data)
                setLoading(false)   

            })

    }

    return (
        <div>
            <CommonStatisticsDisplay {...props} data={monthly}
                title={'Transactions'}
                elementL={TransactionMonthlyCountStatistics} elementR={TransactionMonthlyVolumeStatistics}
                elementB={TransactionOverallStatistics}
            />
        </div>
    )

}