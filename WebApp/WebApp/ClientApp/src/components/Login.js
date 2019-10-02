import React, { Component } from 'react';
import axios from 'axios';

export class Login extends Component {
    displayName = Login.name

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            currentUser: ''
        };
    }

    componentDidMount = () => {
        axios.get('/user/info').then(
            (res) => {
                this.setState({
                    currentUser: res.data.value.name
                });
            }
        );
    }

    validateUser = (e, username) => {
        e.preventDefault();
        const parent = this;
        axios.post('/user/login', {
            Name: this.state.username,
            Password: this.state.password
        }).then(function (res) {
            //console.log(res.data);
            parent.setState(prevState => ({
                currentUser: prevState.username
            }));
        }, function (err) {
            console.log('error', err);
        });
    }

    updateState = (e) => {
        if (e.target.name === 'username') {
            this.setState({
                username: e.target.value
            });

        } else if (e.target.name === 'password') {
            this.setState({
                password: e.target.value
            });
        }
    }

    render() {
        const LoginPrompt = this.state.currentUser === '' ? (
                <form action="/Login" method="post" onSubmit={this.validateUser}>
                    <label>
                        Username:
                            <input type="text" name="username" onChange={this.updateState} value={this.state.username} />
                    </label>
                    <br />
                    <label>
                        Password:
                            <input type="password" name="password" onChange={this.updateState} />
                    </label>
                    <br />
                    <button>Login</button>
                </form>
            ) : null;
        return (
            <div>
                <h1>Login</h1>
                <p>Current login name: {this.state.currentUser}</p>
                {LoginPrompt}
                
            </div>
        );
    }
}
