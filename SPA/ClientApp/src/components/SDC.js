import { Divider } from "@mui/material";
import React from "react";
import SDCFeedDisplay from "./SDCComponents/SDCFeedDisplay";

export default function SDCComponent(props) {
    return (
        <React.Fragment>
            <table width='100%'>
                <tr style={{height:'40vh'}}>
                    <td>
                        <h3>Corporations Pending Approval</h3>
                        <Divider/>
                        <SDCFeedDisplay {...props}/>
                    </td>
                </tr>
                <tr style={{height:'40vh'}}>
                    <td>
                        <h3>Recently approved items</h3>
                        <Divider/>
                        <SDCFeedDisplay {...props} approved/>
                    </td>
                </tr>
            </table>
        </React.Fragment>
    );

}
