import React, {useState} from 'react';
import './Cart.css';

const Cart = (props) => {

	const [name, setName] = useState("Isac Larsson");
	const [country, setCountry] = useState("Sweden");
	const [address, setAddress] = useState("Gatan 10");
	const [city, setCity] = useState("Borås");
	const [zipcode, setZipcode] = useState("50050");

	const updateName = (e) => {
		setName(e.target.value);
	}
	const updateCountry = (e) => {
		setCountry(e.target.value);
	}
	const updateAddress = (e) => {
		setAddress(e.target.value);
	}
	const updateCity = (e) => {
		setCity(e.target.value);
	}
	const updateZipcode = (e) => {
		setZipcode(e.target.value);
	}

	return (
		<div>
			<div className="cart">
				<div className="cross" onClick={props.toggleCart}>
					<div className="bar-one"></div>
					<div className="bar-two"></div>
				</div>
				<form data-name={name} data-country={country} data-address={address} data-city={city} data-zipcode={zipcode}>
					<label htmlFor="name">Name</label>
					<input name="name" value={name} onChange={updateName}></input>
					<label htmlFor="country">Country</label>
					<input name="country" value={country} onChange={updateCountry}></input>
					<label htmlFor="address">Address</label>
					<input name="address" value={address} onChange={updateAddress}></input>
					<label htmlFor="city">City</label>
					<input name="city" value={city} onChange={updateCity}></input>
					<label htmlFor="zipcode">Zipcode</label>
					<input name="zipcode" value={zipcode} onChange={updateZipcode}></input>
					<button onClick={props.order}>Order Products</button>
				</form>
				<div className="cartProducts">
					<h1>Your products:</h1>
					{
						props.cartProducts.map(product => {
							return (
								<div className="cartProduct" key={product.id}>
									<img src={product.image}></img>
									<h4>Quantity: {product.quantity}</h4>
									<button onClick={props.deleteProduct} data-id={product.id}><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" 
									strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className="feather feather-trash-2">
									<polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2">
									</path><line x1="10" y1="11" x2="10" y2="17"></line><line x1="14" y1="11" x2="14" y2="17"></line></svg></button>
									<h3>{product.name}</h3>
								</div>
							)
						})
					}
				</div>
			</div>
			<div className="order">
				
			</div>
		</div>
	);
};

export default Cart;