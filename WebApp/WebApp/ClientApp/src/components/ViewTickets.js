import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

export class ViewTickets extends Component {

    constructor(props) {
        super(props);
        this.state = {
            tickets: []
        };
    }
    componentDidMount = () => {
        axios.get('/tickets/get').then(
            (res) => {
                console.log(res.data);
                this.setState({
                    tickets: res.data.value.tickets
                });
            },
            (err) => {
                console.log('error', err);
            }
        );
    }

    render() {
        
        const ticketList = Array.isArray(this.state.tickets) ? this.state.tickets.map((t, i) => {
            var editPath = `/editticket/${t.id}`;
            return (<tr key={i}>
                <td>
                    {t.id}
                </td>
                <td>
                    {t.status}
                </td>
                <td>
                    {t.words}
                </td>
                <td>          
                    <Link to={editPath}>Edit</Link>
                </td>

            </tr>);
        }) : <tr><td>No Tickets</td></tr> ;

        return (
            <div>
                <h1>View Tickets</h1>
                <div>
                    <table>
                        <tbody>
                            <tr>
                                <th>Num </th>
                                <th>Status </th>
                                <th>Description </th>
                                <th>&nbsp;</th>
                            </tr>
                            {ticketList}
                        </tbody>
                    </table>
                </div>

            </div>
        );
    }
}