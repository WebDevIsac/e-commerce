import React, { useState } from 'react';
import CartIcon from '../CartIcon';

import './ProductContainer.css';


const ProductContainer = (props) => {
	
	// const [cart, setCart] = useState(props.cart);
	const [quantity, setQuantity] = useState(1);
	
	const handleQuantity = (e) => {
		setQuantity(e.target.value);
	}

	const product = props.product;
	
	return (
		<div className="product-container">
			<h2>{product.name}</h2>
			<img src={product.image} alt={product.name}></img>
			<h4>{product.info}</h4>
			<div className="product-price">
				<h2>{product.price}:-</h2>
				<input onChange={handleQuantity} type="number" name="quantity" id="quantity" min="1" max="10" value={quantity}></input>
				<button onClick={props.update} data-id={product.id} data-quantity={quantity}>
					<CartIcon width="24px" height="24px"/>
				</button>
			</div>
		</div>
	);
}

export default ProductContainer;