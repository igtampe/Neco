//Here all the incomes including a big one del Income itself

import { Card, Grid } from "@mui/material";
import React, { useState } from "react";
import CommonStatisticsDisplay, { GraphAndListStatisticsDisplay } from "./CommonStatisticsDisplay";
import { EventTracker } from "@devexpress/dx-react-chart";
import { Chart, PieSeries, Title, Tooltip } from "@devexpress/dx-react-chart-material-ui";
import { Paper, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

const IncomeOverallPane = (props) => {
    return (<>
        <table style={{ marginLeft: 'auto', marginRight: 'auto' }}>
            <tr>
                <td>Total Income Items:</td>
                <td style={{ paddingLeft: '50px' }}>{props.overall ? props.overall.totalCount.toLocaleString() : <Skeleton variant="text" />}</td>
            </tr>
            <tr>
                <td>Total Income Generated:</td>
                <td style={{ paddingLeft: '50px' }}>{props.overall ? props.overall.totalIncome.toLocaleString() + 'p' : <Skeleton variant="text" />}</td>
            </tr>
        </table>
    </>)
}

function IncomeStatisticsPane(url, title, horizontal, setOverall) {

    const [stats, setStats] = useState(undefined)
    const [loading, setLoading] = useState(false)

    return (
        function (props) {

            if (!stats && !loading) {
                setLoading(true)
                fetch('/API/Statistics/Income' + (url ? `/${url}` : ''))
                    .then(r => r.json()).then(data => {

                        if (data.error || data.errors) { return; }

                        setStats(data)
                        if (setOverall) { setOverall(data) }
                        setLoading(false)
                    })
            }

            const listItem = (b) => {
                return (
                    <TableRow>
                        <TableCell width={'100px'}> <a href={b.imageURL === "" ? "/Flag.png" : b.imageURL}>
                            <img alt={'Flag'} src={b.imageURL === "" ? "/Flag.png" : b.imageURL} width={'100px'} /></a>
                        </TableCell>
                        <TableCell>{b.name}</TableCell>
                        <TableCell>{b.count.toLocaleString()}</TableCell>
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
                                    <TableCell>Flag</TableCell>
                                    <TableCell>Name</TableCell>
                                    <TableCell>Count</TableCell>
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

                return (<>
                    <Chart data={stats.breakdown} height={300}>
                        <PieSeries
                            innerRadius={0.7}
                            outerRadius={1}
                            name='Income'
                            valueField="income"
                            argumentField="id"
                        />
                        <PieSeries
                            innerRadius={0.4}
                            outerRadius={0.7}
                            name='Count'
                            valueField="count"
                            argumentField="id"
                        />
                        <Title text={title ? title : "set the title dumb dumb"} />
                        <EventTracker />
                        <Tooltip />
                    </Chart>
                </>)

            }

            return (<GraphAndListStatisticsDisplay {...props} graph={graph} list={list} horizontal={horizontal} />)
        }

    )

}

export default function IncomeStatistics(props) {

    const [overall, setOverall] = useState(undefined);

    return (<>
        <CommonStatisticsDisplay {...props}
            title={'Income'}
            elementT={IncomeStatisticsPane(undefined, 'All Income Items', true, setOverall)}
            elementL={IncomeStatisticsPane('Corporations', 'Corporations')} elementR={IncomeStatisticsPane('Businesses', 'Businesses')}
        />
        <CommonStatisticsDisplay {...props}
            overall={overall}
            elementL={IncomeStatisticsPane('Hotel', 'Hotels')} elementR={IncomeStatisticsPane('Apartments', 'Apartments')}
            elementB={IncomeOverallPane}
        />
    </>)
}