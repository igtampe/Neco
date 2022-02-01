import { Box, Tab, Tabs, Typography } from "@mui/material";
import React, { useState } from "react";
import IncomeItemDisplay from "./IncomeItemDisplay";
import IncomeSummaryDisplay from "./IncomeSummaryDisplay";

//Maybe this should've  been a default component? strange....
function TabPanel(props) {
    const { children, value, index, ...other } = props;

    return (
        <div role="tabpanel" hidden={value !== index} id={`vertical-tabpanel-${index}`} aria-labelledby={`vertical-tab-${index}`} {...other} style={{ width: "100%" }}>
            <Box sx={{ p: 3 }}> <Typography>{children}</Typography> </Box>
        </div>
    );
}

function IncomeTabGroup(props) {
    
    return (
        <Tabs variant="scrollable" value={props.value} onChange={props.handleChange} sx={{ borderRight: 1, borderColor: 'divider' }}>
            <Tab label={"Summary"}/>
            <Tab label={"Airlines"} />
            <Tab label={"Corporations"} />
            <Tab label={"Businesses"} />
            <Tab label={"Hotels"} />
            <Tab label={"Apartments"} />
        </Tabs>

    )
}

export default function IncomeTabs(props) {

    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => { setValue(newValue); };
    
    if (!props.account) {
        return <React.Fragment>
            <div style={{ textAlign: "center", verticalAlign: "middle", lineHeight: "100%", Height: "100%" }}>
                Select an account to get started
            </div>
        </React.Fragment>
    }

    return (
        <React.Fragment>
            <IncomeTabGroup orientation="horizontal" value={value} handleChange={handleChange}/>
            <Box sx={{ bgcolor: 'background.paper', display: 'flex' }} >
                <TabPanel value={value} index={0}>
                    <IncomeSummaryDisplay {...props} open={value===0}/>
                </TabPanel>
                <TabPanel value={value} index={1}>
                    <IncomeItemDisplay {...props} airline open={value===1}/>
                </TabPanel>
                <TabPanel value={value} index={2}>
                    <IncomeItemDisplay {...props} corporation open={value===2}/>
                </TabPanel>
                <TabPanel value={value} index={3}>
                    <IncomeItemDisplay {...props} business open={value===3}/>
                </TabPanel>
                <TabPanel value={value} index={4}>
                    <IncomeItemDisplay {...props} hotel open={value===4}/>
                </TabPanel>
                <TabPanel value={value} index={5}>
                    <IncomeItemDisplay {...props} apartment open={value===5}/>
                </TabPanel>
            </Box>

        </React.Fragment>
    );

}

