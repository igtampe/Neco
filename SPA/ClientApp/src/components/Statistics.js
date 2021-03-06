import React from "react";
import IncomeStatistics from "./StatisticsComponents/IncomeStatistics";
import { Divider } from "@mui/material";
import TransactionStatistics from "./StatisticsComponents/TransactionStatistics";
import WealthStatistics from "./StatisticsComponents/WealthStatistics";
import TaxStatistics from "./StatisticsComponents/TaxStatistics";
import AccountIncomeStatistics from "./StatisticsComponents/AccountIncomeStatistics";

export default function StatisticsComponent(props) {
    return (
        <React.Fragment>
            <TransactionStatistics {...props}/>
            <Divider/>
            <WealthStatistics {...props}/>
            <Divider/>
            <IncomeStatistics {...props}/>
            <Divider/>
            <AccountIncomeStatistics {...props}/>
            <Divider/>
            <TaxStatistics {...props}/>
        </React.Fragment>
    );

}
