import React from 'react';

import './CartButton.css';
import CartIcon from '../CartIcon';

const CartButton = ({amount}) => {
	return (
		<button className="cart-button">
			<CartIcon width="60px" height="60px"/>
			<small>{amount ? amount : 0}</small>
		</button>
	);
};

export default CartButton;