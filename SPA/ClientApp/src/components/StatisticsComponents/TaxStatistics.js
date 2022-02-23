//Here the tax day statistics. Maybe show a little button to confirm that a user wants to get this

import React, { useState } from "react";
import CommonStatisticsDisplay from "./CommonStatisticsDisplay";

export default function TaxStatistics(props){

    return (
        <CommonStatisticsDisplay {...props} 
            title={'Tax'}
            elementL={undefined} elementR={undefined}
        />
    )

}