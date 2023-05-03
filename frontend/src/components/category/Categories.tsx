import React from "react";
import { Link } from "react-router-dom";

const Categories = () => {
  return (
    <div className="container">
      <h3>Categories</h3>
      <Link className="btn btn-primary" to="create">
        Create Category
      </Link>
    </div>
  );
};

export default Categories;
