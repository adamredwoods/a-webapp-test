import React, { Component } from 'react';
import axios from 'axios';

export class CreateTicket extends Component {

    constructor(props) {
        super(props);
        this.state = {
            status: 'open',
            words: '',
            disabled: ''
        };
    }

    componentDidMount = () => {

    }

    updateState = (e) => {
        if (e.target.name === 'status') {
            this.setState({
                status: e.target.value
            });

        } else if (e.target.name === 'words') {
            this.setState({
                words: e.target.value
            });
        }
    }

    onSubmit = (e) => {
        this.setState({
            disabled: 'disabled'
        });
        console.log(this.state.words);
        axios.post('/tickets/create',
            {
                status: this.state.status,
                words: this.state.words
            }
        ).then((res) => {
            if (res.status === 200) {
                this.props.history.push('/viewtickets');
            }
        }).catch(
            (err) => {
                console.log(err);
            });
        
        e.preventDefault();
    }

    render() {
        return (
            <div>
                <h1>Create Ticket</h1>
                <form onSubmit={this.onSubmit} method="post">
                    <h5>
                        Status:
                    </h5>
                    <p>
                        <input type="text" name="status" onChange={this.updateState} value={this.state.status} />
                    </p>
                    <h5>
                        Words:
                    </h5>
                    <textarea name="words" rows="4" cols="40" onChange={this.updateState} value={this.state.words} />
                    <p>
                        <button type="submit" disabled={this.state.disabled}>Submit</button>
                    </p>
                </form>        
            </div>
        );
    }
}