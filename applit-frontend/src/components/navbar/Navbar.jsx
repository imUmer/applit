

import React from 'react';
import './Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="logo">AppLit</div>
      <ul className="nav-links">
        <li className="nav-item"><a href="#">Home</a></li>
        <li className="nav-item"><a href="#">About</a></li>
        <li className="nav-item"><a href="#">Contact</a></li>
      </ul>
    </nav>
  );
};

export default Navbar;
