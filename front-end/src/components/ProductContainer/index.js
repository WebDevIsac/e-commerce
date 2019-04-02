import React, { Component } from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';

class ProductContainer extends Component {
	state = {
		cart: this.props.cart,
	}

	componentDidMount() {
		setTimeout(() => {
			this.setState({
				cart: this.props.cart
			})
			console.log(this.props.cart);
		}, 200);
	}

	handleQuantity = (e) => {
		this.setState({
			quantity: e.target.value
		});
	}
	
	handleButtonClick = () => {
		const product = this.props.product;
		const newProduct = {
			productId: product.id,
			cartId: this.props.cart.id,
			quantity: this.state.quantity
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
				cart: data
			}, () => {
				this.props.update();
			});
		});
	}

	render() {
		const product = this.props.product;
		return (
			<div className="product-container">
				<h2>{product.name}</h2>
				<img src={product.image} alt={product.name}></img>
				<h4>{product.info}</h4>
				<div className="product-price">
					<h2>{product.price}:-</h2>
					<input onChange={this.handleQuantity} type="number" name="quantity" id="quantity" min="1" max="10" value={this.state.quantity}></input>
					<button onClick={this.handleButtonClick}>
						<CartIcon width="24px" height="24px"/>
					</button>
				</div>
			</div>
		);
	}
}

export default ProductContainer;