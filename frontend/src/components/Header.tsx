import React from "react";

import { NavLink } from "react-router-dom";

const Header = () => {
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container-fluid">
        <NavLink className="navbar-brand" to="/">
          Movie App
        </NavLink>
        <div className="collapse navbar-collapse">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink className="nav-link" to="/categories">
                Categories
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/movies/filter">
                Filter Movies
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/actors">
                Actors
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/cinemas">
                Movie cinemas
              </NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/movies/create">
                Create a Movie
              </NavLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Header;
