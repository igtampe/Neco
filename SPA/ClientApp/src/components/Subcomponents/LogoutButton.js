import React from "react";
import { useHistory } from "react-router-dom";
import { IconButton } from "@mui/material";
import Cookies from 'universal-cookie';
import {Logout} from '@mui/icons-material'
import { APIURL } from "../../App";

const cookies = new Cookies();

export default function LogoutButton() {

    const history = useHistory();

    const handleLogout = (event) => {

        const requestOptions = {
          method: 'POST',
          headers: { 'Content-Type': 'application/json', 'SessionID' : cookies.get('SessionID') },
          body : "\"" + cookies.get('SessionID') + "\""
        };
    
        fetch(APIURL + "/API/Users/Out",requestOptions).then( response => {
          cookies.remove("SessionID")
          history.go("/Login")
        })
    
      }
    
    return (
        <React.Fragment>
            <IconButton color="inherit" onClick={handleLogout}><Logout/></IconButton>
        </React.Fragment>
    );

}
