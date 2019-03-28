import React, { Component } from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';

class ProductContainer extends Component {
	render() {
		const handleButtonClick = () => {
			const product = {
				productId: this.props.productId,
				cartId: 2,
				quantity: 1
			}
			console.log('Add ' + this.props.name);

			fetch("http://localhost:63469/api/cart", {
					method: 'POST',
					headers: {
						'Accept': 'application/json',
						'Content-Type': 'application/json'
					},
					body: JSON.stringify( product )
				})
				.then(response => response.json())
				.then(data => {
					console.log(data);
				});
		}
		return (
			<div className="product-container">
				<h2>{this.props.name}</h2>
				<img src={this.props.image} alt={this.props.name}></img>
				<h4>{this.props.info}</h4>
				<div className="product-price">
					<h2>{this.props.price}:-</h2>
					<button onClick={handleButtonClick}>
						<CartIcon width="24px" height="24px"/>
					</button>
				</div>
			</div>
		);
	}
}

export default ProductContainer;