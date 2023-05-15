import React, { useContext } from "react";
import "bootstrap/js/src/collapse.js";


import { Link, NavLink } from "react-router-dom";
import Authorized from "./auth/Authorized";
import Button from "./features/Button";
import { logout } from "./auth/Jwt";
import AuthenticationContext from "./auth/AuthenticationContext";

const Header = () => {
  const { update, claims } = useContext(AuthenticationContext);

  const getUserEmail = (): string => {
    return claims.filter((x) => x.name === "email")[0]?.value;
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
      <div className="container-fluid">
        <NavLink className="navbar-brand" to="/">
          Movie App
        </NavLink>
        <div className="collapse navbar-collapse"
        style={{display: 'flex', justifyContent: 'space-between'}}>
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink to="/movies/filter" className="nav-link">
                Filter Movies
              </NavLink>
            </li>
            <Authorized
              role="admin"
              authorized={
             <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink className="nav-link" to="/categories">
                Categories
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
            <li className="nav-item">
              <NavLink className="nav-link" to="/users">
                Users
              </NavLink>
            </li>
            </ul>
              }
            />
          </ul>
          <div className="d-flex">
            <Authorized
              authorized={
                <>
                  <span className="navbar-brand mb-0">
                    Hello, {getUserEmail()}
                  </span>
                  <Button
                    onClick={() => {
                      logout();
                      update([]);
                    }}
                    className="btn btn-light m-lg-2"
                  >
                    Log out
                  </Button>
                </>
              }
              notAuthorized={
                <>
                  <Link
                    to="/register"
                    className="btn btn-light m-lg-2"
                  >
                    Register
                  </Link>
                  <Link to="/login" className="btn btn-light m-lg-2">
                    Login
                  </Link>
                </>
              }
            />
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Header;
