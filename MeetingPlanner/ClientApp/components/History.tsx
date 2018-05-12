import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface HistoryState {
	id: number;
	name: string;
	city: string;
	date: Date;
	price: number;
	numOfTickets: number;
}
interface CityState {
	id: number;
	name: string;
}
interface HistoryListState {
	histories: HistoryState[],
	cities: CityState[],
	stDay: Date;
	endDay: Date;
	loading: boolean;
	total: number;
}

export class History extends React.Component<RouteComponentProps<{}>, HistoryListState>{
	constructor() {
		super();
		this.state = {
			histories: [],
			cities:[],
			stDay: new Date(1, 0, 0),
			endDay: new Date(1, 0, 0),
			loading: true,
			total:1
		}
		fetch('api/history').then(res => res.json() as Promise<HistoryState[]>)
			.then(data => {
				let totalAm = 0;
				data.forEach(x =>
					totalAm += x.price * x.numOfTickets
				);	
				this.setState({
					total: totalAm,
					histories: data,
					loading: false,
				});
			});
		fetch('api/city').then(res => res.json() as Promise<CityState[]>)
			.then(data => {
				this.setState({
					cities: data,
				});
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
	handleFilter = (e: any) => {
		let stDay;
		if (this.state.stDay !== new Date(1, 0, 0)) {
			stDay = this.state.stDay
		} else {
			stDay = new Date(1, 1, 1, 0, 0, 0);
		}
		let endDay;
		if (this.state.endDay !== new Date(1, 0, 0)) {
			endDay = this.state.endDay
		} else {
			endDay = new Date(1, 1, 1, 0, 0, 0);
		}
		fetch('api/report/' + stDay + '/' + endDay).then(res => res.json() as Promise<HistoryState[]>)
			.then(data => {
				this.setState({
					histories:data
				});
			});
	}
	public getCity(e: string) {
		
		let val = 0;
		var list = this.state.histories.filter(x => x.city == e);
		list.forEach(x => val += (x.price * x.numOfTickets));
		
		console.log(this.state.total);
		return val
	}
	public getCityPer(e: string) {

		let val = 0;
		var list = this.state.histories.filter(x => x.city == e);
		list.forEach(x => val += (x.price * x.numOfTickets));

		return (val/ this.state.total) * 100
	}
	public  renderReport() {
		let val = 0;
		
		const list = this.state.cities.map((el) => <li key={el.id} >
			{el.name} : {this.getCity(el.name)}
		</li>);

		const listT = this.state.cities.map((el) => <li key={el.id} >
			{el.name} : {this.getCityPer(el.name) } %
		</li>);
		return <div><div> <h3>Report</h3><ul className="list-unstyled"> {list}</ul> </div> <div >
			<h3>Report in percentage</h3><ul className="list-unstyled">{listT}</ul></div></div>
	}
	public render() {
		let content = this.state.loading ? <p><em>Loading...</em></p> :
			this.renderTable(this.state.histories);

		return <div>
			<h3> Filter parameters</h3>
			<input type="date" className="form-control" name="stDay" onChange={(e) => this.handleStDay(e)} />
			<input type="date" className="form-control" name="endDay" onChange={(e) => this.handleEndDay(e)} />

			<br/>
			<button className="btn btn-primary btn-lg btn-block" type="submit" onClick={(e) => this.handleFilter(e)}> Filter</button>

			<h1>History</h1>
			{content}
			{this.renderReport()}
		</div>
	}
	private renderTable(histories: HistoryState[]) {
		return <table className='table'>
			<thead>
				<tr>

					<th>Name</th>
					<th>City</th>
					<th>Date</th>
					<th>Price</th>
					<th>Number of tickets</th>



				</tr>
			</thead>
			<tbody>
				{histories.map(history =>
					<tr key={history.id}>
						<td>{history.name}</td>
						<td>{history.city}</td>
						<td>{history.date}</td>
						<td>{history.price}</td>
						<td>{history.numOfTickets}</td>
					</tr>
				)}
			</tbody>
		</table>
	}
}