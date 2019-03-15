import React, { Component } from "react";
import "./App.css";

import Header from '../Header';

class App extends Component {
	render() {
		let products = [];

		fetch("http://localhost:63469/api/products")
			.then(respone => respone.json())
			.then(data => products.push(...data));
			
		return (
			<div>
				<Header/>
				<div className="container">
					<h1>Hello</h1>
				</div>
			</div>
		)
	}
}

export default App;
