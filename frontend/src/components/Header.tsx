import React, { useContext } from "react";

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
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container-fluid">
        <NavLink className="navbar-brand" to="/">
          Movie App
        </NavLink>
        <div
          className="collapse navbar-collapse"
          style={{ display: "flex", justifyContent: "space-between" }}
        >
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink className="nav-link" to="/movies/filter">
                Filter Movies
              </NavLink>
            </li>
            <Authorized
              role="admin"
              authorized={
                // <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                <>
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
                  {/* // </ul> */}
                </>
              }
            />
          </ul>
          <div className="d-flex">
            <Authorized
              authorized={
                <>
                  <span className="nav-link">Hello, {getUserEmail()}</span>
                  <Button
                    onClick={() => {
                      logout();
                      update([]);
                    }}
                    className="nav-link btn btn-link"
                  >
                    Log out
                  </Button>
                </>
              }
              notAuthorized={
                <>
                  <Link to="/register" className="nav-link btn btn-link">
                    Register
                  </Link>
                  <Link to="/login" className="nav-link btn btn-link">
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
