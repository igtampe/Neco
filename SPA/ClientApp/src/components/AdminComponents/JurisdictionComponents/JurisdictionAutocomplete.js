import React, { useState } from "react";
import {
    Autocomplete,
    FormControl, InputLabel, MenuItem, Select, TextField
} from "@mui/material";
import { CircularProgress } from "@mui/material";
import { GenerateGet } from "../../../RequestOptionGenerator";

export const JurisdictionTypes = [ "Countries", "States", "Counties", "Cities" ]

//Requires jurisdictionID, setJurisdictionID.
export function JurisdictionAutoComplete(props) {

    const [loading, setLoading] = useState(false);
    const [jurisdictions, setJurisdictions] = useState(false)
    const [inputValue, setInputValue] = useState(props.jurisdictionID)

    if (!jurisdictions && !loading) {
        setLoading(true)

        var URL = '/API/Taxes/Jurisdictions'

        fetch(URL, GenerateGet(props.Session)) //This actually isn't authenticated pero sabes que zoop.
            .then(response => { return (response.json()) })
            .then(data => {
                //if there was an error then oops
                if (data.error) { return; }

                setJurisdictions(data)
                setLoading(false)
            })
    }

    if (!jurisdictions) { return (<CircularProgress />) }

    return (
        <> 
            <Autocomplete
                value={props.jurisdictionID} onChange={(event, newValue) => { props.setJurisdictionID(newValue.value);}}
                inputValue={inputValue} onInputChange={(event, newInputValue) => { setInputValue(newInputValue);}}
                fullWidth renderInput={(params) => <TextField {...params} label="Jurisdiction" />}
                options={jurisdictions.map(j=>{ return ({
                    label : j.id + ': ' + j.name,
                    value : j.id, type : JurisdictionTypes[j.type]
                })})}
                groupBy={o=>o.type}
            />        
        </>

    )

}