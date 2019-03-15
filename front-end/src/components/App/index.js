import React, { Component } from "react";
import "./App.css";

import Header from '../Header';

class App extends Component {

	state = {
		products: []
	}

	componentDidMount() {
		fetch("http://localhost:63469/api/products")
			.then(respone => respone.json())
			.then(data => (
				this.setState({
					products: data
				})
			));

	}

	render() {
		console.log(this.state);


			
		return (
			<div>
				
				<div className="container">
					{this.state.products.map(item => (
						<ul key={item.id}>
							<li>Name: {item.name}</li>
							<li>Info: {item.info}</li>
							<li>Price: {item.price}</li>
						</ul>
					))}
				</div>
			</div>
		)
	}
}

export default App;
