import React from 'react';
import Logo from '../Logo';
import Navbar from '../Navbar';

import './Header.css';

const Header = () => {
	return (
		<header>
			<Logo/>
			<Navbar/>
		</header>
	);
}

export default Header;