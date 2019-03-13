import React, { Component } from "react";
import "./App.css";

class App extends Component {
	render() {
		let products = [];

		fetch("http://localhost:63469/api/products")
			.then(respone => respone.json())
			.then(data => products.push(...data));
			
		return (
			<div className="App">
				<h1></h1>
			</div>
		);
	}
}

export default App;
