import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface BusketState {
	id: number;
	name: string;
	city: string;
	date: Date;
	price: number;
	numOfTickets: number;
}

interface BusketListState {
	buskets: BusketState[],
	total: number,
	loading: boolean
}

export class Busket extends React.Component<RouteComponentProps<{}>, BusketListState>{
	constructor() {
		super();
		this.state = {
			buskets: [],
			total:0,
			loading: true,
		}
		fetch('api/busket').then(res => res.json() as Promise<BusketState[]>)
			.then(data => {
				let totalAmount = 0;
				data.forEach(val => totalAmount += val.price * val.numOfTickets);
				this.setState({
					total: totalAmount,
					buskets: data,
					loading: false,
				});
			});

	}

	handleDelete = async(e: number) => {
		let list = this.state.buskets.filter(x => x.id != e);
		await fetch('api/busket/' + e,
			{
				method: 'DELETE'

			});
		this.setState({
			buskets:list
		});
		await fetch('api/busket').then(res => res.json() as Promise<BusketState[]>)
			.then(data => {
				let totalAmount = 0;
				data.forEach(val => totalAmount += val.price * val.numOfTickets);
				this.setState({
					total: totalAmount,
					buskets: data,
					loading: false,
				});
			});
		
	}
	buy = async() => {
		this.state.buskets.forEach(
			e => {
				fetch('api/history',
					{
						method: 'POST',
						headers: {
							'Accept': 'application/json',
							'Content-Type': 'application/json'
						},
						body: JSON.stringify(e)
					});
				 fetch('api/busket/' + e.id,
					{
						method: 'DELETE'
						
					});

			}
		);
		this.setState({
			buskets:[],
		});
		 fetch('api/busket').then(res => res.json() as Promise<BusketState[]>)
			.then(data => {
				let totalAmount = 0;
				data.forEach(val => totalAmount += val.price * val.numOfTickets);
				this.setState({
					total: totalAmount,
					buskets: data,
					loading: false,
				});
			});
	}
	public render() {
		let content = this.state.loading ? <p><em>Loading...</em></p> :
			this.renderTable(this.state.buskets);

		return <div><h1>Busket</h1>
			
			{content}
			<h3>Total : {this.state.total}</h3>
			<br/>
			<button className ="btn btn-primary btn-lg btn-block" type="submit" onClick={this.buy}>Buy</button>
		</div>
	}
	private renderTable(buskets: BusketState[]) {
		return <table className='table'>
			<thead>
				<tr>

					<th>Name</th>
					<th>City</th>
					<th>Date</th>
					<th>Price</th>
					<th>Number of tickets</th>
					<th>Delete</th>



				</tr>
			</thead>
			<tbody>
				{buskets.map(busket =>
					<tr key={busket.id}>
						<td>{busket.name}</td>
						<td>{busket.city}</td>
						<td>{busket.date}</td>
						<td>{busket.price}</td>
						<td>{busket.numOfTickets}</td>
						<td><button className="btn btn-primary btn-lg btn-block" type="submit" onClick={() => this.handleDelete(busket.id)}>Delete</button></td>
					</tr>
				)}
			</tbody>
		</table>
	}
}