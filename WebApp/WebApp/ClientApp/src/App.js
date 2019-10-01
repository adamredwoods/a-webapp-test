import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Login } from './components/Login';
import { Logout } from './components/Logout';
import { ViewTickets } from './components/ViewTickets';
import { CreateTicket } from './components/CreateTicket';
import { EditTicket } from './components/EditTicket';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/login' component={Login} />
                <Route path='/viewtickets' component={ViewTickets} />
                <Route path='/createticket' component={CreateTicket} />
                <Route path='/logout' component={Logout} />
                <Route path='/editticket/:id' component={EditTicket} />
            </Layout>
        );
    }
}
