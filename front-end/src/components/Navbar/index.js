import React from 'react';
import './Navbar.css';

const Navbar = () => {
	
	const toggleMenu = (e) => {
		document.querySelector('#burger-menu').classList.toggle('transform');
		document.querySelector('#menu').classList.toggle('toggle-menu');
	}

	return (
		<nav>
			<div onClick={toggleMenu} className="burger-menu" id="burger-menu">
				<div className="bar-one"></div>
				<div className="bar-two"></div>
				<div className="bar-three"></div>
			</div>
			<div className="menu" id="menu">
				<li><a href="https://google.com">News</a></li>
				<li><a href="https://google.com">Shop</a></li>
				<li><a href="https://google.com">Categories</a></li>
				<li><a href="https://google.com">Contact</a></li>
			</div>
		</nav>
	);
}

export default Navbar;