import { CinemasDTO, CreateCinemaDTO } from '../types/cinemas';
import UpdateEntity from '../utils/UpdateEntity';
import { urlMovieCinemas } from '../common/endpoint';
import CinemasForm from './CinemasForm';

const UpdateCinema = () => {
    return (
      <div className="container">
        <UpdateEntity<CreateCinemaDTO, CinemasDTO>
          url={urlMovieCinemas}
          indexURL="/cinemas"
          entityName="Cinemas"
        >
          {(entity, update) => (
            <CinemasForm
              model={entity}
              onSubmit={async (values) => await update(values)}
            />
          )}
        </UpdateEntity>
      </div>
    );
};

export default UpdateCinema;