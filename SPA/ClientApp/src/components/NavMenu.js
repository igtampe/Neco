import React, { useState } from "react";
import { makeStyles } from "@mui/styles";
import {
  Dialog, DialogActions, DialogContent, DialogContentText,
  DialogTitle, IconButton, AppBar, CircularProgress, Toolbar, Typography, Button,
  Drawer, List, Divider, ListItem, ListItemIcon, ListItemText, Box, Switch
} from "@mui/material";
import Cookies from 'universal-cookie';
import { useHistory } from "react-router-dom";
import LogoutButton from "./Subcomponents/LogoutButton"
import MenuIcon from "@mui/icons-material/Menu";
import PasswordChangeButton from "./Subcomponents/PasswordChangeButton";

// react.school/material-ui

const useStyles = makeStyles((theme) => ({
  menuButton: { marginRight: theme.spacing(2) },
  title: { flexGrow: 1 },
  customHeight: { minHeight: 200 },
  offset: theme.mixins.toolbar
}));

const cookies = new Cookies();

const Hamburger = (props) => {

  const GenerateListItem = (props) =>{
    return (

      <ListItem button key={props.text} onClick={() => props.PushTo(props.url)}>
        <ListItemIcon><img src={"/images/clear/" + (props.DarkMode ? "white/" : "") + props.image} alt={props.imageAlt} width="30px" style={{ margin: "5px", marginLeft: "10px" }} /></ListItemIcon>
        <ListItemText>{props.text}</ListItemText>
      </ListItem>
    );
  } 

  const PushTo = (url) => {
    props.history.push(url); 
    props.setMenuOpen(false) 
  } 

  console.log(props)

  return (
    <Drawer open={props.menuOpen} onClose={() => props.setMenuOpen(false)}>
      <Box
        sx={{ width: 250 }}
        role="presentation"
      >
        <List>
          <ListItem key="Logo">
            <div style={{ textAlign: 'center', width: '100%' }}>
              <img src={props.DarkMode ? "necoborderlesswhite.png" : "necoborderlessblack.png"} alt="Neco logo" height="50" />
            </div>
          </ListItem>
        </List>
        <Divider />

        {
          props.Session
            ? <>
              <Divider />
              <List>
                <GenerateListItem text='Accounts' url='/Accounts' image='bank.png' imageAlt='bank' DarkMode={props.DarkMode} PushTo={PushTo}/>
                <GenerateListItem text='Income' url='/Income' image='Income.png' imageAlt='Income' DarkMode={props.DarkMode} PushTo={PushTo}/>
                {
                  props.User && props.User.roles && (props.User.roles.admin || props.User.roles.sdc) 
                  ? <GenerateListItem text='SDC' url='/SDC' image='SDC.png' imageAlt='SDC' DarkMode={props.DarkMode} PushTo={PushTo}/>
                  : <></>}
                {
                  props.User && props.User.roles && (props.User.roles.admin || props.User.roles.government) 
                  ?<GenerateListItem text='Statistics' url='/Statistics' image='Statistics.png' imageAlt='Statistics' DarkMode={props.DarkMode} PushTo={PushTo}/>
                  : <></>}
                {
                  props.User && props.User.roles && (props.User.roles.admin || props.User.roles.admin) 
                  ? <GenerateListItem text='Administrate' url='/Admin' image='Admin.png' imageAlt='Admin' DarkMode={props.DarkMode} PushTo={PushTo}/>
                  : <></>}

              </List>
              <Divider />
              <List>
                <ListItem key="AccountManagement">
                  <PasswordChangeButton />
                  <LogoutButton />
                </ListItem>
              </List>
            </>
            : <List>
              <GenerateListItem text='Login' url='/Login' image='BlankPerson.png' imageAlt='Person' DarkMode={props.DarkMode} PushTo={PushTo}/>
              <GenerateListItem text='Register' url='/Register' image='Add.png' imageAlt='Add' DarkMode={props.DarkMode} PushTo={PushTo}/>
            </List>
        }

        <Divider />
        <List>
          <ListItem>
            <ListItemText style={{ textAlign: "center" }}>
              Dark Mode:
              <Switch checked={props.DarkMode} onChange={() => props.ToggleDarkMode()} />
            </ListItemText>
          </ListItem>
        </List>

        {!process.env.NODE_ENV || process.env.NODE_ENV === 'development' ?
          <>
            <Divider />
            <List><a href="/Swagger">
              <ListItem>
                <ListItemText style={{ textAlign: "center" }}>Swagger</ListItemText>
              </ListItem></a>
            </List>
          </>
          : ""}

      </Box>
    </Drawer>
  )


}

export default function ButtonAppBar(props) {
  const classes = useStyles();
  const history = useHistory();
  const [menuOpen, setMenuOpen] = useState(false);

  const sendToLogin = (event) => {
    history.push("/Login")
    setMenuOpen(false);
  }

  const sendToRegister = (event) => {
    history.push("/Register")
    setMenuOpen(false);
  }

  return (
    <React.Fragment>
      <AppBar color={"primary"} enableColorOnDark>
        <Toolbar>
          <IconButton onClick={() => { setMenuOpen(true) }} style={{ marginRight: "15px" }}><MenuIcon /></IconButton>
          <a href="/">
            <img src={props.DarkMode ? "necoborderlesswhite.png" : "necoborderlessblack.png"} alt="Neco logo" height="50" /></a>
          <Typography variant="h6" className={classes.title} style={{ marginLeft: "10px", fontFamily: 'DM Serif Display' }}> </Typography>
          {
            props.Session
              ? <> {props.User
                ? <React.Fragment>
                  <img src={props.User.imageURL === "" ? "/images/blankperson.png" : props.User.imageURL} alt="Profile" width="30px" style={{ margin: "5px", marginLeft: "10px" }} /> {props.User.name + " (" + props.User.id + ") "}
                  <LogoutButton />
                </React.Fragment>
                : <CircularProgress color="secondary" />}</>

              : <React.Fragment>
                <Button color="inherit" onClick={sendToLogin}> Log In </Button>
                <Button color="inherit" onClick={sendToRegister}> Register </Button>
              </React.Fragment>}
        </Toolbar>
      </AppBar>
      <Toolbar style={{ marginBottom: "20px" }} />

      <Dialog open={props.InvalidSession} >
        <DialogTitle> Session Expired </DialogTitle>
        <DialogContent><DialogContentText>Your session was not found on the server, and has most likely expired. Please log in again.</DialogContentText> </DialogContent>
        <DialogActions> <LogoutButton /> </DialogActions>
      </Dialog>

      <Hamburger DarkMode={props.DarkMode} ToggleDarkMode={props.ToggleDarkMode} Session={props.Session}
        menuOpen={menuOpen} setMenuOpen={setMenuOpen} sendToLogin={sendToLogin} history={history} User={props.User}/>

    </React.Fragment>
  );
}
