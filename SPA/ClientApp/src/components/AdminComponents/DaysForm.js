import React, { useState } from "react";
import {
    Alert,
    Button, Checkbox, Container, FormControlLabel, Grid, LinearProgress, Typography
} from "@mui/material";
import { GenerateJSONPost } from "../../RequestOptionGenerator";
import AccountIncomeStatistics from "../StatisticsComponents/AccountIncomeStatistics";
import TaxStatistics from "../StatisticsComponents/TaxStatistics";

export default function DaysForm(props) {

    const [incomeForce, setIncomeForce] = useState(false)
    const [taxForce, setTaxForce] = useState(false)

    const [result, setResult] = useState({ severity: "success", text: "idk" })
    const [SnackOpen, setSnackOpen] = useState(false);

    const [inProgress, setInProgress] = useState(false);

    const [taxResult, setTaxResult] = useState(undefined)
    const [incomeResult, setIncomeResult] = useState(undefined)


    const handleIncome = (event) => {

        setSnackOpen(false);
        setInProgress(true)

        //Send the request 
        var url = `API/Income/IncomeDay?Force=${incomeForce}`;

        fetch(url, GenerateJSONPost(props.Session))
            .then(response => { return response.json() })
            .then(data => {
                setInProgress(false);
                if (data.error) {
                    setResult({ severity: "error", text: "An error occured: " + data.reason })
                    setSnackOpen(true);

                } else {
                    setResult({ severity: "success", text: "Income day was successfully carried out!" })
                    setSnackOpen(true);
                    setIncomeResult(data)
                }
            })

    }

    const handleTax = (event) => {

        setSnackOpen(false);
        setInProgress(true)

        //Send the request 
        var url = `API/Taxes/TaxDay?Force=${taxForce}`;

        fetch(url, GenerateJSONPost(props.Session))
            .then(response => { return response.json() })
            .then(data => {
                setInProgress(false);
                if (data.error) {
                    setResult({ severity: "error", text: "An error occured: " + data.reason })
                    setSnackOpen(true);

                } else {
                    setResult({ severity: "success", text: "Tax day was successfully carried out!" })
                    setSnackOpen(true);
                    setTaxResult(data)
                }
            })

    }


    return (
        <React.Fragment>

            <Grid container spacing={0} direction="column" alignItems="center" justifyContent="center">
                <Grid item xs={2}>

                    <Container style={{ paddingLeft: '25%', paddingRight: '25%', paddingTop: '25px' }}>
                        <Typography>
                            <Typography variant="h5" style={{ fontFamily: "Outfit", textAlign: "center" }}> INCOME AND TAX DAYS</Typography><br />
                            <Typography style={{ color: 'red' }}><b>WARNING:</b></Typography><br />
                            The following buttons let you execute an Income or Tax day event. These happen on the 1st and the 15th of the month respectively.
                            You can also tick the box to force this to happen regardless of the day. Be careful not to do this more than once on the day of.
                            <br /><br />
                            <div> {inProgress
                                ? <LinearProgress />
                                : <> {!taxResult && !incomeResult
                                    ? <table style={{ marginLeft: 'auto', marginRight: 'auto' }}>
                                        <tr>
                                            <td> <Button variant='contained' color='primary' disabled={Date.UTC().getDay !== 1 && !incomeForce}
                                                onClick={handleIncome} style={{ margin: "10px", width: '150px' }}> INCOME DAY </Button> </td>
                                            <td> <FormControlLabel label="Force" control={<Checkbox checked={incomeForce} onClick={() => setIncomeForce(!incomeForce)} />} /></td>
                                        </tr>
                                        <tr>
                                            <td><Button variant='contained' color='secondary' disabled={Date.UTC().getDay !== 15 && !taxForce}
                                                onClick={handleTax} style={{ margin: "10px", width: '150px' }}> TAX DAY </Button></td>
                                            <td><FormControlLabel label="Force" control={<Checkbox checked={taxForce} onClick={() => setTaxForce(!taxForce)} />} /></td>
                                        </tr>
                                    </table>
                                    : <>{
                                        taxResult 
                                        ? <TaxStatistics {...props} stats={taxResult}/>
                                        : <AccountIncomeStatistics {...props} stats={incomeResult}/>

                                    }</>
                                }</>}</div>
                            <br />
                        </Typography>

                        <Alert severity={result.severity} hidden={!SnackOpen} style={{ marginTop: '20px' }}> {result.text} </Alert>
                    </Container>
                </Grid>
            </Grid>

        </React.Fragment>
    );

}
