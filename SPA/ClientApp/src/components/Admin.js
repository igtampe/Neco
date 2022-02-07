import React, { useState } from "react";
import { Tab, Tabs, Typography, Box } from "@mui/material";
import JurisdictionDisplay from "./AdminComponents/JurisdictionComponents/JurisdictionDisplay";
import BankDisplay from "./AdminComponents/BankComponents/BankDisplay";
import UserDisplay from './AdminComponents/UserComponents/UserDisplay';
import NTAForm from "./AdminComponents/NTAForm";

//Maybe this should've  been a default component? strange....
function TabPanel(props) {
    const { children, value, index, ...other } = props;

    return (
        <div role="tabpanel" hidden={value !== index} id={`vertical-tabpanel-${index}`} aria-labelledby={`vertical-tab-${index}`} {...other} style={{ width: "100%" }}>
            <Box sx={{ p: 3 }}> <Typography>{children}</Typography> </Box>
        </div>
    );
}

function ClosetTabs(props) {
    return (
        <Tabs orientation={props.orientation} variant="scrollable" value={props.value} onChange={props.handleChange} sx={{ borderRight: 1, borderColor: 'divider' }}>
            <Tab label={"Jurisdictions"} /> <Tab label={"Banks"} /> <Tab label={"Users"} /> <Tab label={"NTA"} />
        </Tabs>
    )
}

export default function AdminComponent(props) {


    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => { setValue(newValue); };


    return (
        <React.Fragment>
            <ClosetTabs orientation="horizontal" value={value} handleChange={handleChange} />
            <Box sx={{ bgcolor: 'background.paper', display: 'flex' }} >
                <TabPanel value={value} index={0}>
                    <JurisdictionDisplay Vertical={props.Vertical} Session={props.Session}/>
                </TabPanel>
                <TabPanel value={value} index={1}>
                    <BankDisplay {...props}/>
                </TabPanel>
                <TabPanel value={value} index={2}>
                    <UserDisplay {...props}/>
                </TabPanel>
                <TabPanel value={value} index={3}>
                    <NTAForm {...props}/>
                </TabPanel>
            </Box>

        </React.Fragment>
    );

}
