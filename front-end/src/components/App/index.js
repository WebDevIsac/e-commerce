import React, { Component } from "react";
import Header from '../Header';
import CartButton from "../CartButton";
import ProductContainer from '../ProductContainer';

import "./App.css";
import Cart from "../Cart";



class App extends Component {
	state = {
		products: [],
		cartId: 40,
		cart: [],
		isFetched: false,
		showCart: false
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

	addProduct = (e) => {
		let element = e.target;
		let elementName = element.tagName;
		
		while (elementName !== "BUTTON") {
			element = element.parentElement;
			elementName = element.tagName;
		}
		const newProduct = {
			productId: element.dataset.id,
			cartId: this.state.cartId,
			quantity: element.dataset.quantity
		}
		console.log(newProduct);
		
		fetch("http://localhost:63469/api/cart", {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify( newProduct )
		})
		.then(response => response.json())
		.then(data => {
			console.log(data);
			this.setState({
				cart: data,
				isFetched: true
			});
		});
	}

	deleteProduct = (e) => {
			const productToDelete = {
				productId: productId,
				cartId: this.state.cart.id,
				quantity: 1
			}
			fetch(`http://localhost:63469/api/cart`, {
				method: 'DELETE',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				// body: JSON.stringify( productToDelete )
			})
		}
	}
		

	handleButton = () => {
		this.setState({
			showCart: !this.state.showCart
		});
	}
		
	render() {
		
		return (
			<div className="page">
				<Cart toggle={this.state.showCart} callFunction={this.handleButton} cart={this.state.cart} products={this.state.products}/>
				<Header/>
				{this.state.isFetched && <div className="products-list">
					{
						this.state.products.map(product => (
							<ProductContainer
								key={product.id} product={product} cart={this.state.cart} update={this.addProduct}
							/>
						))
					}
					</div>
				}
				<CartButton callFunction={this.handleButton} amount={this.state.isFetched ? this.state.cart.products.length : 0}/>
			</div>
		);
	}
}

export default App;
