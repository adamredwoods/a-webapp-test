import React, { Component } from 'react';
import axios from 'axios';

export class Login extends Component {
  displayName = Login.name

  constructor(props) {
    super(props);
      this.state = {
          username: '',
          password: ''
      };
  }

    validateUser = (e) => {
        e.preventDefault();
        var user = axios.post('/user/login', {
            username: this.state.username,
            password: this.state.password
        }).then(function (res) {
            console.log(res.data);
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
    return (
      <div>
            <h1>Login</h1>
            <form action="/Login" method="post" onSubmit={this.validateUser}>
                <label>
                    Username:
                    <input type="text" name="username" onChange={this.updateState} value={this.state.username} />
                </label>
                <label>
                    Password:
                    <input type="password" name="password" onChange={this.updateState} />
                </label>
                <button>Login</button>
            </form>
      </div>
    );
  }
}
