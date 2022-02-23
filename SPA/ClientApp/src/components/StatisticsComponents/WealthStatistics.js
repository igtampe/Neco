//Here the bank and jurisdiction wealths

import { EventTracker } from "@devexpress/dx-react-chart";
import { Chart, Legend, PieSeries, Title, Tooltip } from "@devexpress/dx-react-chart-material-ui";
import { Paper, Skeleton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import React, { useState } from "react";
import CommonStatisticsDisplay, { GraphAndListStatisticsDisplay } from "./CommonStatisticsDisplay";

function WealthOverallStatistics(props) {

    return (<>
        <table style={{marginLeft:'auto', marginRight:'auto'}}>
            <tr>
                <td>Total Bank Accounts:</td>
                <td style={{paddingLeft:'50px'}}>{props.banks ? props.banks.totalCount.toLocaleString() : <Skeleton variant="text" />}</td>
            </tr>
            <tr>
                <td>Total Pecunias in Circulation:</td>
                <td style={{paddingLeft:'50px'}}>{props.banks ? props.banks.totalMoney.toLocaleString() + 'p' : <Skeleton variant="text" />}</td>
            </tr>
        </table>
    </>)

}


function BankStatistics(props) {

    const list = (props) => {

        if (!props.banks) {
            return (
                <Skeleton variant="rectangular" width={'100%'} height={'200px'} />
            )
        }

        return (<>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Logo</TableCell>
                            <TableCell>Count</TableCell>
                            <TableCell>Funds</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {props.banks.accountSummary.map(b => {
                            return (<TableRow>
                                <TableCell width={'100px'}> <a href={b.imageURL === "" ? "/Bank.png" : b.imageURL}>
                                    <img alt={'Bank Logo'} src={b.imageURL === "" ? "/Bank.png" : b.imageURL} width={'100px'} /></a>
                                </TableCell>
                                <TableCell>{b.count.toLocaleString()}</TableCell>
                                <TableCell>{b.balance.toLocaleString()}p</TableCell>
                            </TableRow>)
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
        </>)

    }

    const graph = (props) => {

        if (!props.banks) {
            return (
                <Skeleton variant="rectangular" width={'100%'} height={'200px'} />
            )
        }

        return (<>
            <Chart data={props.banks.accountSummary} height={300}>
                <PieSeries
                    innerRadius={0.7}
                    outerRadius={1}
                    name='Funds'
                    valueField="balance"
                    argumentField="id"
                />
                <PieSeries
                    innerRadius={0.4}
                    outerRadius={0.7}
                    name='Count'
                    valueField="count"
                    argumentField="id"
                />
                <Title text="Banks"/>
                <EventTracker />
                <Tooltip />
            </Chart>
        </>)

    }

    return (<GraphAndListStatisticsDisplay {...props} graph={graph} list={list} />)


}

function JurisdictionStatistics(props) {

    const [stats, setStats] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!stats && !loading) {

        setLoading(true)

        fetch('/API/Statistics/Jurisdictions')
            .then(R => R.json())
            .then(data => {

                if (data.error || data.errors) { return; }
                setStats(data)
                setLoading(false)

            })

    }



    const list = (props) => {

        if (!stats) {
            return (
                <Skeleton variant="rectangular" width={'100%'} height={'200px'} />
            )
        }

        return (<>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Flag</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Funds</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {stats.map(b => {
                            return (<TableRow>
                                <TableCell width={'100px'}> <a href={b.imageURL === "" ? "/Flag.png" : b.imageURL}>
                                    <img alt={'Flag'} src={b.imageURL === "" ? "/Flag.png" : b.imageURL} width={'100px'} /></a>
                                </TableCell>
                                <TableCell>{b.name}</TableCell>
                                <TableCell>{b.wealth.toLocaleString()}p</TableCell>
                            </TableRow>)
                        })}
                    </TableBody>
                </Table>
            </TableContainer>
        </>)

    }

    const graph = (props) => {

        if (!stats) {
            return (
                <Skeleton variant="rectangular" width={'100%'} height={'200px'} />
            )
        }

        return (<>
            <Chart data={stats} height={300}>
                <PieSeries
                    innerRadius={0.4}
                    outerRadius={1}
                    name='Funds'
                    valueField="wealth"
                    argumentField="id"
                />
                <Title text="Jurisdictions"/>
                <EventTracker />
                <Tooltip />
            </Chart>
        </>)

    }

    return (<GraphAndListStatisticsDisplay {...props} graph={graph} list={list} />)
}

export default function WealthStatistics(props) {

    const [banks, setBanks] = useState(undefined)
    const [loading, setLoading] = useState(false)

    if (!banks && !loading) {

        setLoading(true)

        fetch('/API/Statistics/Banks')
            .then(R => R.json())
            .then(data => {

                if (data.error || data.errors) { return; }
                setBanks(data)
                setLoading(false)

            })

    }

    return (
        <CommonStatisticsDisplay {...props}
            title={'Wealth'} banks={banks}
            elementL={BankStatistics} elementR={JurisdictionStatistics}
            elementB={WealthOverallStatistics}
        />
    )

}