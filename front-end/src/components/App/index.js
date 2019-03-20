import React, { Component } from "react";
import Header from '../Header';
import ProductsList from '../ProductsList';
import CartButton from "../CartButton";

import "./App.css";



class App extends Component {
	state = {
		cart: []
	}
	
	componentDidMount() {
		const api = "http://localhost:63469/api/cart";
		fetch(api)
			.then(response => response.json())
			.then(data => {
				this.setState({
					cart: data
				});
			});
	}
	
	render() {
		console.log(this.state.cart);
		return (
			<div className="page">
				<Header/>
				<ProductsList/>
				<CartButton/>
			</div>
		);
	}
}

export default App;
