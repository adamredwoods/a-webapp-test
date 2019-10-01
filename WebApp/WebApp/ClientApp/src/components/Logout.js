import React, { Component } from 'react';
import axios from 'axios';

export class Logout extends Component {
    constructor(props) {
        super(props);
    }

    render() {

        axios.get('/user/logout');

        return (
            <div>
                Logged out.
            </div>
        );
    }
}