import { CategoryDTO } from '../types/category';
import { urlCategories } from "../common/endpoint";

import Entity from '../utils/Entity';

const Categories = () => {
  
  return (
    <div>
      <Entity<CategoryDTO>
        url={urlCategories}
        createURL="/categories/create"
        title="Categories"
        entityName="Category"
      >
        {(categories, buttons) => (
          <>
            <thead>
              <tr>
                <th>Options</th>
                <th>Name</th>
              </tr>
            </thead>
            <tbody>
              {categories?.map((category) => (
                <tr key={category.id}>
                  <td>
                    {buttons(`/categories/update/${category.id}`, category.id)}
                  </td>
                  <td>
                    <strong>{category.name}</strong>
                  </td>
                </tr>
              ))}
            </tbody>
          </>
        )}
      </Entity>
    </div>
  );
};

export default Categories;
