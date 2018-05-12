import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface EventState {
	id: number;
	name: string;
	city: string;
	date: Date;
	price: number;
}
interface CityState {
	id: number;
	name: string;
}
interface EventListState {
	events: EventState[];
	cities: CityState[];
	city: string;
	stDay: Date;
	endDay: Date;
	stPrice: number;
	endPrice: number;
	event: EventState;
	loading: boolean;
	error: boolean;

}

export class EventLIst extends React.Component<RouteComponentProps<{}>, EventListState>{
	constructor() {
		super();
		this.state = {
			events: [],
			loading: true,
			cities: [],
			city: '',
			stDay: new Date(1,0,0),
			endDay: new Date(1, 0, 0),
			stPrice: 0,
			endPrice: 0,
			error:false,
			event: { id: 0, name: '', city: 'lol', date: new Date(1, 0, 0), price:0}

		};
		fetch('api/city').then(res => res.json() as Promise<CityState[]>)
			.then(data => {
				this.setState({
					cities: data,
				});
			});
		fetch('api/event').then(res => res.json() as Promise<EventState[]>)
			.then(data => {
				
				this.setState({
					events: data,
					loading: false,
				});
			});
		
	}
	handleCity = (val: string) => {

		this.setState({
			city: val
		});
	}
	handleStDay = (e: any) => {

		this.setState({
			stDay: e.target.value
		});
	}
	handleEndDay = (e: any) => {

		this.setState({
			endDay: e.target.value
		});
	}
	handleStPrice = (e: any) => {

		this.setState({
			stPrice: e.target.value
		});
	}
	handleEndPrice = (e: any) => {

		this.setState({
			endPrice: e.target.value
		});
	}




	bookTicket = async(e:any, val: number) => {
		e.preventDefault();
		console.log(val);
		await fetch('api/event/' + val).then(res => res.json() as Promise<EventState[]>)
			.then(data => {
				console.log(data);
				this.setState({
					event: data[0],
				});
				console.log(this.state.event);
			});
		console.log(this.state.event.id)
		var newData = {
			"id": this.state.event.id,
			"name": this.state.event.name,
			"city": this.state.event.city,
			"date": this.state.event.date,
			"price": this.state.event.price,
			"numOfTickets":1
		};
		console.log("ssss"+newData)
		fetch('api/busket/',
			{
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(newData)
			}).then(res => {
				if (!res.ok) {
					this.setState({
						error: true
					});
				} else {
					this.setState({
						error: false
					});
				}
			});
	}




	handleFilter = (e: any) => {
		let city;
		if (this.state.city !== '') {
			city = this.state.city
		} else {
			city = null
		}
		let stDay;
		if (this.state.stDay !== new Date(1, 0, 0)) {
			stDay = this.state.stDay
		} else {
			stDay = new Date(1, 0, 0, 0, 0, 0)
		}
		console.log(stDay);
		let endDay;
		if (this.state.endDay !== new Date(1, 0, 0)) {
			endDay = this.state.endDay
		} else {
			endDay = new Date(1, 0, 0, 0, 0, 0)
		}
		let stPrice;
		if (this.state.stPrice !== 0) {
			stPrice = this.state.stPrice;
		} else {
			stPrice = 0;
		}
		let endPrice;
		if (this.state.endPrice !== 0) {
			endPrice = this.state.endPrice;
		} else {
			endPrice = 0;
		}
		fetch('api/filter/' + city + '/' + stDay + '/' + endDay + '/' + stPrice + '/' + endPrice).then(res => res.json() as Promise<EventState[]>)
			.then(data => {

				this.setState({
					events: data,
					loading: false,
				});
			});

		
	}
	public render() {
		let content = this.state.loading ? <p><em>Loading...</em></p> :
			this.renderTable(this.state.events);
		
		return <div>
			{this.errorRender()}
			{this.filter()}
			<h1>Events</h1>
			{content}
		</div>
	}
	private errorRender() {
		if (this.state.error) {
			return <div><em>Status: all events should be in one town</em></div>
		} else {
			return <div><em>Status: ok</em></div>
		}
	}
	private filter() {
		const list = this.state.cities.map((el) => <li key={el.id} onClick={() => this.handleCity(el.name)}>{el.name}</li>);
		return <div>
			<h3> Filter parameters </h3>
			<div className="dropdown">
				<label htmlFor="inputCity" className="sr-only">City</label>
				<button type="text" id="inputCity" className="form-control" placeholder="City" data-toggle="dropdown" value={this.state.city} >{this.state.city}<span className="caret"></span></button>
				<ul className="dropdown-menu">
					{list}
				</ul>
			</div>
			<br/>
			<input type="date" name="stDay" className="form-control" onChange={(e) => this.handleStDay(e)} />
			<br />
			<input type="date" name="endDay" className="form-control" onChange={(e) => this.handleEndDay(e)} />
			<br />
			
			<input type="text" name="stPrice" className="form-control" onChange={(e) => this.handleStPrice(e)} />
			<br />
			<input type="text" name="endPrice" className="form-control" onChange={(e) => this.handleEndPrice(e)} />
			<br />
			<button className ="btn btn-primary btn-lg btn-block" type="submit" onClick={(e)=>this.handleFilter(e)}> Filter</button>
		</div>
	}


	private renderTable(events: EventState[]) {
		return <table className='table'>
			<thead>
				<tr>

					<th>Name</th>
					<th>City</th>
					<th>Date</th>
					<th>Price</th>
					<th>Book a ticket</th>
					
					

				</tr>
			</thead>
			<tbody>
				{events.map(event =>
					<tr key={event.id}>
						<td>{event.name}</td>
						<td>{event.city}</td>
						<td>{event.date}</td>
						<td>{event.price}</td>
						<td><button type="submit" className="btn btn-primary btn-lg btn-block" onClick={(e) => this.bookTicket(e, event.id)}>Book</button></td>
					</tr>
				)}
			</tbody>
		</table>
	}
}