import React, { Component } from "react";
import Header from '../Header';
import CartButton from "../CartButton";
import ProductContainer from '../ProductContainer';

import "./App.css";



class App extends Component {
	state = {
		products: [],
		cartId: 1,
		cart: [],
		isFetched: false
	}
	
	componentDidMount() {
		const apiProducts = "http://localhost:63469/api/products";
		fetch(apiProducts)
			.then(response => response.json())
			.then(data => {
				this.setState({
					products: data
				});
			});
			this.updateCart();
		}
		
		updateCart = () => {
			const apiCart = `http://localhost:63469/api/cart/${this.state.cartId}`;
			fetch(apiCart)
				.then(response => response.json())
				.then(data => {
					this.setState({
						cart: data,
						isFetched: true
					});
				});
		}
		
		render() {
			
			// console.log(this.state.cart);
			
			return (
				<div className="page">
					<Header/>
					<div className="products-list">
					{
						this.state.products.map(product => (
							<ProductContainer 
								key={product.id} product={product} cart={this.state.cart} update={this.updateCart}
							/>
						))
					}
					</div>
					<CartButton amount={this.state.isFetched ? this.state.cart.products.length : 0}/>
				</div>
			);
	}
}

export default App;
