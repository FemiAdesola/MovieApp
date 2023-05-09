import { CinemasDTO } from '../types/cinemas';
import { urlMovieCinemas } from '../common/endpoint';
import Entity from '../utils/Entity';

const Cinemas = () => {
    return (
      <div className="container">
        <Entity<CinemasDTO>
          url={urlMovieCinemas}
          createURL="/cinemas/create"
          title="Cinemas"
          entityName="Movie Cinemas"
        >
          {(cinemas, buttons) => (
            <>
              <thead>
                <tr>
                  <th></th>
                  <th>Name</th>
                </tr>
              </thead>
              <tbody>
                {cinemas?.map((cinema) => (
                  <tr key={cinema.id}>
                    <td>
                      {buttons(`/cinemas/update/${cinema.id}`, cinema.id)}
                    </td>
                    <td>{cinema.name}</td>
                  </tr>
                ))}
              </tbody>
            </>
          )}
        </Entity>
      </div>
    );
};

export default Cinemas;