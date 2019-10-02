import React, { Component } from 'react';
import axios from 'axios';

export class EditTicket extends Component {

    constructor(props) {
        super(props);
        this.state = {
            ticketId: -1,
            status: 'open',
            words: '',
            disabled: ''
        };
    }

    componentDidMount = () => {
        var idToFind = this.props.match.params.id;
        axios.get('/tickets/get/' + idToFind).then(
            (res) => {
                console.log(res.data);
                if (res.data.value.tickets !== null) {
                    this.setState({
                        ticketId: idToFind,
                        status: res.data.value.tickets[0].status,
                        words: res.data.value.tickets[0].words
                    });
                }
            },
            (err) => {
                console.log('error', err);
            }
        );
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
        
        axios.post('/tickets/update',
            {
                id: this.state.ticketId,
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
                <h1>Edit Ticket #{this.state.ticketId}</h1>
                <form onSubmit={this.onSubmit} method="post">
                    <h5>
                        Status:
                    </h5>
                    <p>
                        <input type="text" name="status" onChange={this.updateState} value={this.state.status}/>
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