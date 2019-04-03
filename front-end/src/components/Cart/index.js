import React, { Component } from 'react';
import './Cart.css';

class Cart extends Component {
	state = {
		cart: [],
		products: [],
		cartProducts: []
	}
	
	componentDidUpdate() {
		const cart = document.querySelector('.cart');
		this.props.toggle ? cart.classList.add('toggle') : cart.classList.remove('toggle');
	}

	componentWillReceiveProps() {
		setTimeout(() => {
			this.setState({
				cart: this.props.cart,
				products: this.props.products
			}, () => {
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
			})
		}, 500);
	}

	
	render() {
		return (
			<div className="cart">
				<div className="cross" onClick={this.props.callFunction}>
					<div className="bar-one"></div>
					<div className="bar-two"></div>
				</div>
				<div className="cartProducts">
					{
						this.state.cartProducts.map(product => {
							return (
								<div className="cartProduct" key={product.id}>
									<img src={product.image}></img>
									<h4>Quantity: {product.quantity}</h4>
									<button onClick={}><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" 
									strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className="feather feather-trash-2">
									<polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2">
									</path><line x1="10" y1="11" x2="10" y2="17"></line><line x1="14" y1="11" x2="14" y2="17"></line></svg></button>
									<h3>{product.name}</h3>
								</div>
							)
						})
					}
				</div>
			</div>
		);
	}
}

export default Cart;