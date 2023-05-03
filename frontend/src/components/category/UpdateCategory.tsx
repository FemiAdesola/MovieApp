import React from 'react';
import { useParams } from 'react-router-dom';
import CategoryForm from './CategoryForm';

const UpdateCategory = () => {
    const { id }: any = useParams();
    return (
      <div className="container">
        <h3>Update Categories</h3>
        <CategoryForm
          model={{ name: "Update" }}
          onSubmit={async (value) => {
            await new Promise((r) => setTimeout(r, 1000));
              console.log(value);
              console.log(id);
          }}
        />
      </div>
    );
};

export default UpdateCategory;