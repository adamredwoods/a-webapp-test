import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

export class ViewTickets extends Component {

    constructor(props) {
        super(props);
        this.state = {
            tickets: [],
            canDeleteFlag: false
        };
    }

    componentDidMount = () => {
        axios.get('/tickets/get').then(
            (res) => {
                //console.log(res.data);
                if (res.status === 200) {
                    this.setState({
                        tickets: res.data.value.tickets,
                        canDeleteFlag: res.data.value.canDelete
                    });
                }
        }).catch(
            (err) => {
                console.log('error', err);
            }
        );
    }

    handleDeleteClick = (e, deleteId) => {
        axios.post('/tickets/delete/' + deleteId).then(
            (res) => {
                if (res.status === 200) {
                    let newTickets = this.state.tickets.filter(t => t.id !== deleteId);
                    this.setState({
                        tickets: newTickets
                    });
                }
        }).catch(
            (err) => {
                console.log('error', err);
            }
        );
    }

    render() {
        
        let ticketList = this.state.tickets !== null && Array.isArray(this.state.tickets) ?
            this.state.tickets.map((t, i) => {
                let editPath = `/editticket/${t.id}`;
                let deleteOption = this.state.canDeleteFlag ?
                    <a onClick={(e) => this.handleDeleteClick(e, t.id)}> Delete </a> :
                    null;

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
                        <Link to={editPath}> Edit </Link>
                        {deleteOption}
                    </td>

                </tr>);
            }
            ) : <tr><td colSpan='4'>No Tickets</td></tr>;

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