import React from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';

const ProductContainer = (product) => {

	const handleButtonClick = () => {
		console.log('Add ' + product.name);
	}

	return (
		<div className="product-container">
			<h1>{product.name}</h1>
			<img src={product.image} alt={product.name}></img>
			<h3>{product.info}</h3>
			<div className="product-price">
				<h2>{product.price}:-</h2>
				<button onClick={handleButtonClick}>
					<CartIcon width="24px" height="24px"/>
				</button>
			</div>
		</div>
	);
};

export default ProductContainer;