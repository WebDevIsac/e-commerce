import React, { Component } from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';

class ProductContainer extends Component {
	state = {
		cart: []
	}
	render() {

		const product = this.props.product;

		const handleButtonClick = () => {
			const newProduct = {
				productId: product.id,
				cartId: this.props.cartId,
				quantity: 1
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
					console.log(data);
					this.setState({
						cart: data
					});
				});
		}

		return (
			<div className="product-container">
				<h2>{product.name}</h2>
				<img src={product.image} alt={product.name}></img>
				<h4>{product.info}</h4>
				<div className="product-price">
					<h2>{product.price}:-</h2>
					<button onClick={handleButtonClick}>
						<CartIcon width="24px" height="24px"/>
					</button>
				</div>
			</div>
		);
	}
}

export default ProductContainer;