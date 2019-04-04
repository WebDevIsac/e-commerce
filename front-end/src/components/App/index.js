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
		cartProducts: [],
		// order: {},
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

	updateCart = () => {
		let cartProducts = [];
			this.state.cart.products.filter(cartProduct => {
				for (let i = 0; i < this.state.products.length; i++) {
					if (cartProduct.id === this.state.products[i].id) {
						cartProducts.push(cartProduct);
					}
				}
			})

		this.setState({
			cartProducts: cartProducts
		})
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
			this.setState({
				cart: data,
				isFetched: true
			});
		});
	}

	deleteProduct = (e) => {
		let element = e.target;
		let elementName = element.tagName;
		
		while (elementName !== "BUTTON") {
			element = element.parentElement;
			elementName = element.tagName;
		}

		const productId = element.dataset.id;

		const productToDelete = {
			productId: productId,
			cartId: this.state.cartId,
			quantity: 1
		}

		fetch(`http://localhost:63469/api/cart`, {
			method: 'DELETE',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify( productToDelete )
		})
		.then(response => response.json())
		.then(data => {
			this.setState({
				cart: data
			});
		});
		setTimeout(() => {
			this.updateCart();
		}, 500);
	}

	order = (e) => {
		e.preventDefault();

		const element = e.target.parentElement;
		const newOrder = {
			name: element.dataset.name,
			country: element.dataset.country,
			address: element.dataset.address,
			city: element.dataset.city,
			zipcode: element.dataset.zipcode
		}

		fetch(`http://localhost:63469/api/order/${this.state.cartId}`, {
			method: 'POST',
			headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify( newOrder )
		})
		.then(response => response.json())
		.then(data => {
			console.log(data);
		});
	}
		

	toggleCart = () => {
		!this.state.showCart && this.updateCart();
		const cart = document.querySelector('.cart');
		cart.classList.toggle('toggle');
		this.setState({
			showCart: !this.state.showCart
		});
	}
		
	render() {
		return (
			<div className="page">
				{this.state.isFetched && <Cart showCart={this.state.showCart} toggleCart={this.toggleCart} deleteProduct={this.deleteProduct} 
				cart={this.state.cart} products={this.state.products} cartProducts={this.state.cartProducts} order={this.order}/>}
				<Header/>
				{this.state.isFetched && 
					<div className="products-list">
					{
						this.state.products.map(product => (
							<ProductContainer
								key={product.id} product={product} cart={this.state.cart} update={this.addProduct}
							/>
						))
					}
					</div>
				}
				<CartButton toggleCart={this.toggleCart} amount={this.state.isFetched ? this.state.cart.products.length : 0}/>
			</div>
		);
	}
}

export default App;
