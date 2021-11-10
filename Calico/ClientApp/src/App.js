import React, { Component } from 'react';
//import { Route, Routes } from 'react-router';
import { Layout }  from './components/Layout';
import Login from './components/Login'
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Routes,
  Link
} from "react-router-dom";

import './custom.css'

export default class App extends Component {
  static displayName = "NECO: .Net Economy Web Access";

  render () {
    return (
      <Router>
        <Layout>
          <Routes>
            <Route path="/home" element={<Home/>}/>
            <Route path="/Login" element={<Login/>}/>
            <Route path="/counter" element={<Counter/>}/>
            <Route path="/fetch-data" element={<FetchData/>}/>
          </Routes>  
        </Layout>
      </Router>      
    );
  } 
}

