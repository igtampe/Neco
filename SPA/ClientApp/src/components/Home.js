import { Button, Card, CardContent, Divider, Grid, Typography } from '@mui/material';
import React from 'react';
import { useHistory } from "react-router-dom";

function AdCard(props) {
  return (
  <Card sx={{ minWidth: '100%' }}>
    <CardContent>
      <img src={props.img} width='100%' alt='' style={props.fit ? {height:'200px', objectFit:'cover'} : {}}/>
      <Typography gutterBottom sx={{textAlign:'center', marginTop:'10px'}}> <h4>{props.title}</h4> </Typography>
      <Divider />
      <div style={{ minWidth: '100%', maxHeight: '200px', overflowY: 'auto' }} >
        {props.children}
      </div>
    </CardContent>
  </Card>)
}

export default function Home(props) {
  const history = useHistory();

  return (
    <div>

      <div style={{ marginBottom: '15px', position: 'relative' }}>
        <img src='/images/home.jpg' alt='Newpond Central' width='100%' /><br />
        <div style={{ position: 'absolute', bottom: '50%', left: '50%', transform: 'translate(-50%,50%)', textShadow: '2px 2px 8px #000000' }}>
          <img src='/necowhiteShadow.png' width='100%' alt='Neco Logo'/>
        </div>
      </div>

      <h2 style={{ textAlign: 'center' }}>Welcome to the future of UMS Banking</h2>
      <div style={{ textAlign: 'center', marginTop: '20px' }}>
        <Button onClick={()=>{history.push("/Login")}}variant={'contained'}> Get Started</Button>
      </div>
      <br />
      <Grid container style={{ minWidth: '100%' }} spacing={2}>
        <Grid item xs={props.Vertical ? 12 : 4}>
          <AdCard title='Powerful' img='/screenshots/necoshot1.png' fit={!props.Vertical}>
            When complete, Neco will have all the features ViBE had, and more. With powerful tools to verify land ownership, easy management of any and all income items direclty on the site, live statistics on the economy, and much more.
          </AdCard>
        </Grid>

        <Grid item xs={props.Vertical ? 12 : 4}>
          <AdCard title='Flexible' img='/screenshots/necoshot4.png' fit={!props.Vertical}>
            Neco is built with flexibility in mind, allowing for future expansion. Banks and jurisdictions are easily createable, editable, and manageable from the admin dashboard.
          </AdCard>
        </Grid>

        <Grid item xs={props.Vertical ? 12 : 4}>
          <AdCard title='Easier' img='/screenshots/necoshot2.png' fit={!props.Vertical}>
            With a web frontend, sharable bank accounts, and easier and fully integrated income management, Neco makes it easy and intuitive to join the UMS Economy. It'll do what ViBE did with TEBECON all over again.
          </AdCard>
        </Grid>

      </Grid>
    </div>
  );
}
