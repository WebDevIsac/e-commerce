import React, { Component } from 'react';
import ProductContainer from '../ProductContainer'

import './ProductsList.css';

class ProductsList extends Component {

	state = {
		products: []
	}

	componentDidMount() {
		const api = "http://localhost:63469/api/products";
		fetch(api)
			.then(response => response.json())
			.then(data => {
				this.setState({
					products: data
				});
			});
	}

	render() {
		return (
			<div className="products-list">
				{
					this.state.products.map(product => (
						<ProductContainer 
							key={product.id} product={product} cartId={this.props.cartId}
						/>
					))
				}
			</div>
		);
	}
}

export default ProductsList;