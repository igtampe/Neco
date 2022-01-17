import React from "react";
import Grid from "@mui/material/Grid"
import Typography from "@mui/material/Typography";
import RegisterForm from "./Subcomponents/RegisterForm";

export default function RegisterComponent(props) {
    return (
        <React.Fragment>
            <Typography>
                <Grid
                    container
                    spacing={0}
                    direction="column"
                    alignItems="center"
                    justifyContent="center"
                    style={{ minHeight: '50vh' }}
                >
                    <Grid item xs={12}>
                        <RegisterForm DarkMode={props.DarkMode} setSession={props.setSession}/>
                    </Grid>
                </Grid>
            </Typography>
        </React.Fragment>
    );

}
