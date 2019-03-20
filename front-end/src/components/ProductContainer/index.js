import React from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';

const ProductContainer = (props) => {

	const handleButtonClick = () => {
		console.log('Add ' + props.name);
	}

	return (
		<div className="product-container">
			<h1>{props.name}</h1>
			<img src={props.image} alt={props.name}></img>
			<h3>{props.info}</h3>
			<div className="product-price">
				<h2>{props.price}:-</h2>
				<button onClick={handleButtonClick}>
					<CartIcon width="24px" height="24px"/>
				</button>
			</div>
		</div>
	);
};

export default ProductContainer;