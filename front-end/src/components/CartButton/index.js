import React from 'react';

import './CartButton.css';
import CartIcon from '../CartIcon';

const CartButton = () => {
	return (
		<button className="cart-button">
			<CartIcon width="34px" height="34px"/>
		</button>
	);
};

export default CartButton;