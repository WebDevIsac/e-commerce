import React, { Component } from "react";
import Header from '../Header';
import ProductsList from '../ProductsList';
import CartButton from "../CartButton";

import "./App.css";



class App extends Component {
	state = {
		id: 1,
		cart: [],
		isFetched: false
	}

	
	componentDidMount() {
		const api = `http://localhost:63469/api/cart/${this.state.id}`;
		fetch(api)
			.then(response => response.json())
			.then(data => {
				this.setState({
					cart: data,
					isFetched: true
				});
			});
		}
		
	
	render() {

		console.log(this.state.cart);
		return (
			<div className="page">
				<Header/>
				<ProductsList cartId={this.state.cart.id}/>
				<CartButton amount={this.state.isFetched ? this.state.cart.products.length : 0}/>
			</div>
		);
	}
}

export default App;
