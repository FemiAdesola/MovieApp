import React from "react";
import CategoryForm from "./CategoryForm";

const CreateCategory = () => {
  return (
    // <>
    //       <h3>Create Categories</h3>
    //       {/* <DisplayErrors errors={errors} /> */}
    //       <CategoryForm model={{ name: '' }}
    //           onSubmit={async value => {
    //              await create(value);
    //           }}
    //       />
    //   </>
    <div className="container">
      <h3>Create Categories</h3>
      <CategoryForm
        model={{ name: "" }}
        onSubmit={async (value) => {
          await new Promise(r => setTimeout(r, 4000));
            console.log(value);
        }}
      />
    </div>
  );
};

export default CreateCategory;
