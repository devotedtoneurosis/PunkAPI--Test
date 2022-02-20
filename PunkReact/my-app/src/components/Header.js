import React from 'react';
import { NavLink } from 'react-router-dom';

const Header = () => {
    return (
      <header>
        <h1>Punk Review App</h1>
        <hr />
        <div className="links">
          <NavLink to="/" className="link" activeClassName="active" exact>
            Reviews
          </NavLink>
          <NavLink to="/add" className="link" activeClassName="active">
            Add Review
          </NavLink>
        </div>
      </header>
    );
  };
  
  export default Header;