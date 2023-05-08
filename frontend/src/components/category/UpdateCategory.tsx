import CategoryForm from './CategoryForm';
import { urlCategories } from '../common/endpoint';
import { CategoryDTO, CreateCategoryDTO } from '../types/category';
import UpdateEntity from '../utils/UpdateEntity';

const UpdateCategory = () => {
    return (
      <div>
        <UpdateEntity<CreateCategoryDTO, CategoryDTO>
          url={urlCategories}
          entityName="Categories"
          indexURL="/categories"
        >
          {(entity, edit) => (
            <CategoryForm
              model={entity}
              onSubmit={async (value) => {
                await edit(value);
              }}
            />
          )}
        </UpdateEntity>
      </div>
    );
};

export default UpdateCategory;