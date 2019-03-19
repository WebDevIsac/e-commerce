import React, { Component } from "react";
import Header from '../Header';
import ProductsList from '../ProductsList';
import CartButton from "../CartButton";

import "./App.css";


class App extends Component {

	render() {
		return (
			<div className="page">
				<Header/>
				<ProductsList/>
				<CartButton/>
			</div>
		);
	}
}

export default App;
