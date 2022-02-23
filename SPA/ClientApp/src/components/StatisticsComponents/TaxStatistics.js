//Here the tax day statistics. Maybe show a little button to confirm that a user wants to get this

import React, { useState } from "react";
import CommonStatisticsDisplay from "./CommonStatisticsDisplay";
import { EventTracker } from "@devexpress/dx-react-chart";
import { Chart, PieSeries, Title, Tooltip } from "@devexpress/dx-react-chart-material-ui";
import { Paper, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

export default function TaxStatistics(props){

    const [stats, setStats] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!stats && !loading) {
        setLoading(true)
        fetch('/API/Statistics/Tax')
            .then(r => r.json()).then(data => {

                if (data.error || data.errors) { return; }

                setStats(data)
                setLoading(false)
            })
    }

    const TaxOverallPane = (props) => {
        return (<>
            <table style={{ marginLeft: 'auto', marginRight: 'auto' }}>
                <tr>
                    <td>Total Tax Collected:</td>
                    <td style={{ paddingLeft: '50px' }}>{stats ? stats.totalTaxCollected.toLocaleString() + 'p' : <Skeleton variant="text" />}</td>
                </tr>
            </table>
        </>)
    }
    
    const listItem = (b) => {
        return (
            <TableRow>
                <TableCell width={'100px'}> <a href={b.imageURL === "" ? "/Flag.png" : b.imageURL}>
                    <img alt={'Flag'} src={b.imageURL === "" ? "/Flag.png" : b.imageURL} width={'100px'} /></a>
                </TableCell>
                <TableCell>{b.name}</TableCell>
                <TableCell>{b.taxCollected.toLocaleString()}p</TableCell>
            </TableRow>)
    }

    const list = (props) => {

        if (!stats) { return (<Skeleton variant="rectangular" width={'100%'} height={'200px'} />) }

        return (<>
            <TableContainer component={Paper} style={{ height:'350px', overflowY:'auto'}}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell>Flag</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Tax Collected</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody> {stats.breakdown.map(A => listItem(A))} </TableBody>
                </Table>
            </TableContainer>
        </>)

    }

    const graph = (props) => {

        if (!stats) { return (<Skeleton variant="rectangular" width={'100%'} height={'200px'} />) }

        return (<>
            <Chart data={stats.breakdown} height={350}>
                <PieSeries
                    innerRadius={0.4}
                    outerRadius={1}
                    name='Tax'
                    valueField="taxCollected"
                    argumentField="id"
                />
                <Title text={"Tax Collected by District"} />
                <EventTracker />
                <Tooltip />
            </Chart>
        </>)

    }

    return (
        <CommonStatisticsDisplay {...props} 
            title={'Tax'}
            elementL={graph} elementR={list} 
            elementB={TaxOverallPane}
        />
    )

}