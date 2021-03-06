//Here the tax day statistics. Maybe show a little button to confirm that a user wants to get this

import React, { useState } from "react";
import CommonStatisticsDisplay from "./CommonStatisticsDisplay";
import { EventTracker } from "@devexpress/dx-react-chart";
import { Chart, PieSeries, Title, Tooltip } from "@devexpress/dx-react-chart-material-ui";
import { Paper, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { APIURL } from "../../App";

export default function TaxStatistics(props){

    const [stats, setStats] = useState(props.stats)
    const [loading, setLoading] = useState(false)

    if (!stats && !loading) {

        if(props.stats){
            setStats(props.stats)
            return;
        }

        setLoading(true)
        fetch(APIURL + '/API/Statistics/Tax')
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
                <TableCell width={'100px'}> <a href={b.flag === "" ? "/Flag.png" : b.flag}>
                    <img alt={'Flag'} src={b.flag === "" ? "/Flag.png" : b.flag} width={'100px'} /></a>
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

        const graphToolTip = (info) => {
            const b = stats.breakdown[info.targetItem.point];
            return (<>
                <table>
                    <tr>
                        <td rowSpan={2}>
                            <a href={b.flag === "" ? "/Flag.png" : b.flag}>
                                <img alt={'Flag'} src={b.flag === "" ? "/Flag.png" : b.flag} width={'100px'} style={{marginRight:'10px'}}/>
                            </a>
                        </td>
                        <td>{b.name}</td>
                    </tr>
                    <tr>
                        <td>{Number(b.taxCollected).toLocaleString()}p in funds</td>
                    </tr>
                </table>
                </>);
        }

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
                <Tooltip contentComponent={graphToolTip}/>
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