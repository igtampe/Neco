//Here goes things

import { Card, CardContent, Grid } from "@mui/material";
import React from "react";

export function GraphAndListStatisticsDisplay(props) {
    //MAN I *LOVE* GRIDS!!!!
    return (
        <Grid container spacing={2}>
            <Grid item xs={ props.horizontal && !props.Vertical ? 6 : 12}>
                {props.graph ? props.graph(props) : nada('graph')}
            </Grid>
            <Grid item xs={props.horizontal && !props.Vertical ? 6 : 12}>
                {props.list ? props.list(props) : nada('list')}
            </Grid>
        </Grid>
    )
}

function nada(name) { return (<>Nothing. Use "{name}" </>) }

export default function CommonStatisticsDisplay(props) {

    return (
        <div style={{ marginTop: '10px', marginBottom: '10px', ...props.style }}>
            {props.title ? <><h2 style={{ marginBottom: '10px', marginTop: '20px', textAlign:'center' }}>{props.title}</h2></> : <></>}
            <Grid container spacing={2}>
                {props.elementT
                    ? <Grid item xs={12}>
                        <Card><CardContent>
                            {props.elementT ? props.elementT(props) : nada('elementT')}
                        </CardContent></Card>
                    </Grid>
                    : <></>}
                <Grid item xs={props.Vertical ? 12 : 6}>
                    <Card> <CardContent>
                            {props.elementL ? props.elementL(props) : nada('elementL')}
                    </CardContent></Card>
                </Grid>
                <Grid item xs={props.Vertical ? 12 : 6}>
                    <Card><CardContent>
                        {props.elementR ? props.elementR(props) : nada('elementR')}
                    </CardContent></Card>
                </Grid>
                {props.elementB
                    ? <Grid item xs={12}>
                        <Card><CardContent>
                            {props.elementB ? props.elementB(props) : nada('elementB')}
                        </CardContent></Card>
                    </Grid>
                    : <></>}
            </Grid>
        </div>
    )

}