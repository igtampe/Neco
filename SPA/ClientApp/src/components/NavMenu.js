import React, { useState } from "react";
import { makeStyles } from "@mui/styles";
import {
  Dialog, DialogActions, DialogContent, DialogContentText,
  DialogTitle, IconButton, AppBar, CircularProgress, Toolbar, Typography, Button,
  Drawer, List, Divider, ListItem, ListItemIcon, ListItemText, Box, Switch, MenuItem, Menu, Badge, TableContainer, Paper, Table, TableHead, TableCell, TableRow, TableBody
} from "@mui/material";
import { useHistory } from "react-router-dom";
import LogoutButton from "./Subcomponents/LogoutButton"
import MenuIcon from "@mui/icons-material/Menu";
import PasswordChangeButton from "./Subcomponents/PasswordChangeButton";
import { GenerateDelete, GenerateGet, GenerateJSONPut } from "../RequestOptionGenerator";
import AlertSnackbar from "./AlertSnackbar";
import { Delete } from "@mui/icons-material";
import PicturePicker from "./PicturePicker";

// react.school/material-ui

const useStyles = makeStyles((theme) => ({
  menuButton: { marginRight: theme.spacing(2) },
  title: { flexGrow: 1 },
  customHeight: { minHeight: 200 },
  offset: theme.mixins.toolbar
}));

const Hamburger = (props) => {

  const GenerateListItem = (props) => {
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
                <GenerateListItem text='Accounts' url='/Accounts' image='bank.png' imageAlt='bank' DarkMode={props.DarkMode} PushTo={PushTo} />
                <GenerateListItem text='Income' url='/Income' image='Income.png' imageAlt='Income' DarkMode={props.DarkMode} PushTo={PushTo} />
                {
                  props.User && (props.User.isAdmin || props.UserisSdc)
                    ? <GenerateListItem text='SDC' url='/SDC' image='SDC.png' imageAlt='SDC' DarkMode={props.DarkMode} PushTo={PushTo} />
                    : <></>}
                {
                  props.User && (props.User.isAdmin || props.User.isGov)
                    ? <GenerateListItem text='Statistics' url='/Statistics' image='Statistics.png' imageAlt='Statistics' DarkMode={props.DarkMode} PushTo={PushTo} />
                    : <></>}
                {
                  props.User && (props.User.isAdmin)
                    ? <GenerateListItem text='Administrate' url='/Admin' image='Admin.png' imageAlt='Admin' DarkMode={props.DarkMode} PushTo={PushTo} />
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
              <GenerateListItem text='Login' url='/Login' image='BlankPerson.png' imageAlt='Person' DarkMode={props.DarkMode} PushTo={PushTo} />
              <GenerateListItem text='Register' url='/Register' image='Add.png' imageAlt='Add' DarkMode={props.DarkMode} PushTo={PushTo} />
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

const UserButton = (props) => {

  const [anchorEl, setAnchorEl] = React.useState(null);
  const [notifsOpen, setNotifsOpen] = useState(false);
  const [notifs, setNotifs] = useState();

  const [loading, setLoading] = useState(false);

  const [snackOpen, setSnackOpen] = useState(false)
  const [result, setResult] = useState({ text: 'a', severity: 'success' })
  const [deleting, setDeleting] = useState(false);

  const [pickerOpen, setPickerOpen] = useState(false)

  const open = Boolean(anchorEl);

  const handleClick = (event) => { setAnchorEl(event.currentTarget); };
  const handleClose = () => { setAnchorEl(null); };
  const handleNotifOpen = () => {
    setNotifsOpen(true)
    handleClose();
  }
  
  const handlePickerOpen = () => {
    setPickerOpen(true);
    handleClose();
  }

  if (!notifs && !loading && props.Session) {
    //begin loading the notifs!

    setLoading(true)

    fetch('API/Users/Notifs', GenerateGet(props.Session))
      .then(response => response.json())
      .then(data => {
        if (data.error) { } //do nothing
        else {
          setLoading(false)
          setNotifs(data)
        }
      })

  }

  const handleDeleteNotif = (notifid) => (event) => {

    setDeleting(true)

    fetch('/API/Users/Notifs/' + notifid, GenerateDelete(props.Session))
      .then(response => response.json())
      .then(data => {
        if (data.error) {
          setDeleting(false)
          setNotifs(undefined) //load them again

          setResult({ text: 'Could not delete notification', severity: 'error' })
          setSnackOpen(true);
        }
        else {
          setDeleting(false)
          setNotifs(undefined) //load them again

          setResult({ ...result, text: 'Notification deleted successfully' })
          setSnackOpen(true);
        }
      })
  }

  const setImageURL = (e) => {
     
    if(!e) {
      console.log("imageurl was not set")
      return;
    }

    //ok we have a new image
    
    fetch('/API/Users/image',GenerateJSONPut(props.Session,e))
    .then(response=> response.ok)
    .then( data=> {
      console.log("ok it happeneds")
      if (!data) {
        console.log("n o")
        setResult({ text: "An error occured while updating your picture", severity: 'error' })
        setSnackOpen(true);
      }
      else {
        console.log("y e s")
        props.RefreshUser();
        setResult({ ...result, text: 'Picture updated successfully!' })
        setSnackOpen(true);
        
      }
      setImageURL(undefined)
      }
    )
  }

  return (
    <div>
      <Button onClick={handleClick} style={{ textTransform: 'none' }}>
        <Badge badgeContent={notifs ? notifs.length : 0} color="secondary">
          <img src={props.User.imageURL === "" ? "/images/blankperson.png" : props.User.imageURL} alt="Profile" width="30px" style={{ margin: "5px", marginLeft: "10px" }} />
        </Badge>

      </Button>
      <Menu anchorEl={anchorEl} open={open} onClose={handleClose} >
        <MenuItem onClick={handleNotifOpen}> Notifications {notifs && notifs.length > 0 ? "(" + notifs.length + ")" : ""} </MenuItem>
        <MenuItem onClick={handlePickerOpen} disabled={!props.User.isAdmin && !props.User.isUploader}>Change Profile</MenuItem>
        <Divider />
        <ListItem key="AccountManagement">
          <PasswordChangeButton />
        </ListItem>
      </Menu>

      <Dialog maxWidth='sm' fullWidth open={notifsOpen} onClose={() => { setNotifsOpen(false) }}>
        <DialogTitle>Notifications</DialogTitle>
        <DialogContent>
          <TableContainer component={Paper}>
            <Table style={{ minWidth: '100%' }}>
              <TableHead>
                <TableRow>
                  <TableCell>Date and time</TableCell>
                  <TableCell>Text</TableCell>
                  <TableCell></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {
                  !notifs ?
                    <TableRow>
                      <TableCell colSpan={3} style={{ textAlign: "center" }}><CircularProgress /></TableCell>
                    </TableRow> : <>{

                      notifs.length === 0 ?
                        <TableRow>
                          <TableCell colSpan={3} style={{ textAlign: "center" }}>You have no notifications</TableCell>
                        </TableRow> : <>{

                          notifs.map(L => {

                            var D = new Date(L.date)

                            return (
                              <TableRow>
                                <TableCell style={{ maxWidth: '50px' }}>{D.toLocaleString()}</TableCell>
                                <TableCell style={{ maxWidth: '210px' }}>{L.text}</TableCell>
                                <TableCell style={{ maxWidth: '100%' }}>
                                  <IconButton onClick={handleDeleteNotif(L.id)} disabled={deleting}> <Delete /> </IconButton>
                                </TableCell>
                              </TableRow>
                            )
                          }
                          )
                        }
                        </>
                    }
                    </>
                }
              </TableBody>
            </Table>
          </TableContainer>




        </DialogContent>
      </Dialog>

      <AlertSnackbar open={snackOpen} setOpen={setSnackOpen} result={result} />

      <PicturePicker open={pickerOpen} setOpen={setPickerOpen} imageURL={props.User.imageUrl} setImageURL={setImageURL} defaultImage={"/images/Blankperson.png"}/>

    </div>
  );

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
                  <UserButton User={props.User} Session={props.Session} DarkMode={props.DarkMode} RefreshUser = {props.RefreshUser} />
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
        menuOpen={menuOpen} setMenuOpen={setMenuOpen} sendToLogin={sendToLogin} history={history} User={props.User} />

    </React.Fragment>
  );
}
