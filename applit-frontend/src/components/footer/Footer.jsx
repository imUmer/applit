

import React from 'react'; 
import { Link } from 'react-router-dom';
import './Footer.css'  

export default function Footer(props) {
  return (
    <>
    <div className='footer-container'>
    <nav className="footer-navbar">
      <Link className='logo colors' to={'/'}><div >AppLit</div></Link>
        <div className="nav-links">
          <ul className="nav-links">
            <li className="nav-item"><Link to={'/'}>Home</Link></li>
            <li className="nav-item"><Link to={'/editor'}>IDE</Link></li>
            <li className="nav-item"><Link to={'/about'}>About</Link></li>
              
              {/* <li className="nav-item"><a href="#">IDE</a></li>
              <li className="nav-item"><a href="#">Contact</a></li> */}
           </ul> 
        </div>
        <div className="footer-social">
            <h3>Contact us</h3>
            <a href="#" class="fa fa-facebook"></a>
            <a href="#" class="fa fa-instagram"></a>
            <a href="#" class="fa fa-youtube"></a>
        </div>
      

       </nav>
    </div>
   </>
  );
}