import React, { Component } from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';

class ProductContainer extends Component {
	render() {
		const handleButtonClick = () => {
			console.log('Add ' + this.props.name);
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