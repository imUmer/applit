

import React from 'react';
import './Navbar.css';
import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav className="navbar">
      <Link to={'/'}><div className="logo">AppLit</div></Link>
      
      <ul className="nav-links">
      <li className="nav-item"><Link to={'/'}><a href="/">Home</a></Link></li>
      <li className="nav-item"><Link to={'/editor'}><a href="/">IDE</a></Link></li>
      <li className="nav-item"><Link to={'/about'}><a href="/">About</a></Link></li>
        
        {/* <li className="nav-item"><a href="#">IDE</a></li>
        <li className="nav-item"><a href="#">Contact</a></li> */}
      </ul>
    </nav>
  );
};

export default Navbar;
