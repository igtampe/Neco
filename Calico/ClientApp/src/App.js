import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout }  from './components/Layout';
import Login from './components/Login'
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'

export default class App extends Component {
  static displayName = "NECO: .Net Economy Web Access";

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/Login' component={Login} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
