import React, { useState } from 'react';
import { Route, Redirect } from 'react-router';
import  Layout  from './components/Layout';
import { Home } from './components/Home';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import Cookies from 'universal-cookie/es6';
import {GenerateGet} from './RequestOptionGenerator'
import Login from "./components/Login.js"

import './custom.css'
import { CircularProgress, CssBaseline } from '@mui/material';
import RegisterComponent from './components/Register';
import AccountsComponent from './components/Accounts';
import IncomeManagementComponent from './components/IncomeManagement';
import StatisticsComponent from './components/Statistics';
import SDCComponent from './components/SDC';
import AdminComponent from './components/Admin';

const cookies = new Cookies();

const lightTheme = createTheme({
  palette: {
    mode: 'light',
    primary: { main: '#12bf48', },
    secondary: { main: '#f50057', },
  },
});

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
    primary: { main: '#1c5621', },
    secondary: { main: '#f50057', },
  },
})

export default function App() {

  const [Session, setSession] = useState(undefined)
  const [User, setUser] = useState(undefined)

  const [Loading, setLoading] = useState(false)
  const [InvalidSession, setInvalidSession] = useState(false);

  //Dark mode will not be a user saved preference. It'll be saved in a cookie
  const [darkMode, setDarkMode] = useState(undefined);

  //This is the set session that must be passed down
  const SetSession = (SessionID) => {

    //Set the cookie
    cookies.set("SessionID", SessionID)

    //set the usestates
    setSession(SessionID)
    setInvalidSession(false)
  }

  const ToggleDarkMode = () => {
    //What is !undefined? Please tell me it's "true" just to make my life easier.
    if(darkMode){
      //Delete the cookie
      cookies.remove("NecoDarkMode")
    } else {
      //Add the cookie
      cookies.set("NecoDarkMode",true)
    }

    setDarkMode(!darkMode);
    
  }

  //Assuming there's a valid session, this will automatically trigger a refresh
  const RefreshUser = () => { setUser(undefined); }

  //This runs at legitiately *EVERY* time we load and render ANY page in the app
  //So here we can set the session and user

  //Check that session reflects the cookie's state
  if (Session !== cookies.get("SessionID")) {  setSession(cookies.get("SessionID")) } 
  if (darkMode !== cookies.get("NecoDarkMode")) { setDarkMode(cookies.get("NecoDarkMode")) }

  //Check that the user is defined
  if (Session && !InvalidSession && !Loading && !User) {
    //If there is a session, and it's not invalid, and
    //we're not already loading a user, and the user is not set

    //Well, time to get the user

    setLoading(true);
    fetch("API/Users", GenerateGet(Session))
      .then(response => {
        if (!response.ok) {
          setInvalidSession(true)
          return undefined;
        }
        return response.json()
      }).then(data => {

        //Actually we don't need to check if it's undefined or not
        setUser(data)
        setLoading(false)
        //if it's invalid it'll just set undefin ed which is what user should be

      })

  }

  return (
    <ThemeProvider theme={darkMode ? darkTheme : lightTheme}>
       <CssBaseline />
      <Layout DarkMode={darkMode} ToggleDarkMode={ToggleDarkMode} Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}>
        <Route exact path='/'>
          <Home DarkMode={darkMode} Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/>
        </Route>
        <Route path='/Login'>
          {Session
          ? <Redirect to='/Accounts'/>
          : <Login DarkMode={darkMode}Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/> }
          
        </Route>
        <Route path='/Register'>
        {Session
          ? <Redirect to='/Accounts'/>
          : <RegisterComponent DarkMode={darkMode}Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/> }
        </Route>
        <Route path='/Accounts'>
        {Session
          ? <AccountsComponent DarkMode={darkMode}Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/>
          : <Redirect to='/Login'/> }
        </Route>
        <Route path='/Income'>
        {Session
          ? <IncomeManagementComponent DarkMode={darkMode}Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/>
          : <Redirect to='/Login'/> }
        </Route>
        <Route path='/Statistics'>
            { Session
            ? <>
              {
                User
                ? <>
                  {
                    User.roles && (User.roles.admin || User.roles.government) 
                    ? <StatisticsComponent DarkMode={darkMode} Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/>
                    : <>You do not have permission to access this resource</>
                  }
                </>
                : <CircularProgress/>
              }
            </>
            : <Redirect to='/Login'/> }
        </Route>
        <Route path='/SDC'>
        { Session
            ? <>
              {
                User
                ? <>
                  {
                    User.roles && (User.roles.admin || User.roles.sdc) 
                    ? <SDCComponent DarkMode={darkMode}Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/>
                    : <>You do not have permission to access this resource</>
                  }
                </>
                : <CircularProgress/>
              }
            </>
            : <Redirect to='/Login'/> }
        </Route>
        <Route path='/Admin'>
        { Session
            ? <>
              {
                User
                ? <>
                  {
                    User.roles && User.roles.admin 
                    ? <AdminComponent DarkMode={darkMode}Session={Session} InvalidSession={InvalidSession} setSession = {SetSession} RefreshUser = {RefreshUser} User={User}/>
                    : <>You do not have permission to access this resource</>
                  }
                </>
                : <CircularProgress/>
              }
            </>
            : <Redirect to='/Login'/> }
        </Route>
      </Layout>
    </ThemeProvider>
  );

}
