//Here the tax day statistics. Maybe show a little button to confirm that a user wants to get this

import React, { useState } from "react";
import CommonStatisticsDisplay from "./CommonStatisticsDisplay";
import { EventTracker } from "@devexpress/dx-react-chart";
import { Chart, PieSeries, Title, Tooltip } from "@devexpress/dx-react-chart-material-ui";
import { Paper, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

export default function AccountIncomeStatistics(props){

    const [stats, setStats] = useState(props.stats)
    const [loading, setLoading] = useState(false)

    if (!stats && !loading) {

        if(props.stats){
            setStats(props.stats)
            return;
        }

        setLoading(true)
        fetch('/API/Statistics/Income/Accounts')
            .then(r => r.json()).then(data => {

                if (data.error || data.errors) { return; }

                setStats(data)
                setLoading(false)
            })
    }

    const IncomeOverallPane = (props) => {
        return (<>
            <table style={{ marginLeft: 'auto', marginRight: 'auto' }}>
                <tr>
                    <td>Total Income Generated:</td>
                    <td style={{ paddingLeft: '50px' }}>{stats ? stats.totalIncome.toLocaleString() + 'p' : <Skeleton variant="text" />}</td>
                </tr>
            </table>
        </>)
    }
    
    const listItem = (b) => {
        return (
            <TableRow>
                <TableCell width={'100px'}>{b.id}</TableCell>
                <TableCell>{b.name}</TableCell>
                <TableCell>{b.income.toLocaleString()}p</TableCell>
            </TableRow>)
    }

    const list = (props) => {

        if (!stats) { return (<Skeleton variant="rectangular" width={'100%'} height={'200px'} />) }

        return (<>
            <TableContainer component={Paper} style={{ height:'350px', overflowY:'auto'}}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Income</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody> {stats.breakdown.map(A => listItem(A))} </TableBody>
                </Table>
            </TableContainer>
        </>)

    }

    const graph = (props) => {

        if (!stats) { return (<Skeleton variant="rectangular" width={'100%'} height={'200px'} />) }

        const graphToolTip = (info) => {
            const b = stats.breakdown[info.targetItem.point];
            return (<>
                <table>
                    <tr>
                        <td>{b.name}</td>
                    </tr>
                    <tr>
                        <td>{Number(b.income).toLocaleString()}p/month</td>
                    </tr>
                </table>
                </>);
        }

        return (<>
            <Chart data={stats.breakdown} height={350}>
                <PieSeries
                    innerRadius={0.4}
                    outerRadius={1}
                    name='Income'
                    valueField="income"
                    argumentField="id"
                />
                <Title text={"Income per Account"} />
                <EventTracker />
                <Tooltip contentComponent={graphToolTip}/>
            </Chart>
        </>)

    }

    return (
        <CommonStatisticsDisplay {...props} 
            title={'Income (by account)'}
            elementL={graph} elementR={list} 
            elementB={IncomeOverallPane}
        />
    )

}