//Here all the incomes including a big one del Income itself

import { Card, Grid } from "@mui/material";
import React, { useState } from "react";
import CommonStatisticsDisplay from "./CommonStatisticsDisplay";

export default function IncomeStatistics(props){

    return (
        <CommonStatisticsDisplay {...props} 
            title={'Income'}
            elementL={undefined} elementR={undefined}
        />
    )

}