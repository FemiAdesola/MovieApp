import { useFormikContext } from 'formik';

import { CoordinateDTO } from '../../types/map';
import Map from '../../utils/Map'
import { MapFieldProps } from '../../types/field';

const MapField = (props:MapFieldProps) => {
    const { values } = useFormikContext<any>();

   const handleMapClick = (coordinates: CoordinateDTO) =>{
      values[props.latField] = coordinates.lat;
      values[props.lngField] = coordinates.lng;
    }
    return (
        <Map
            coordinates={props.coordinates}
            handleMapClick={handleMapClick}
        />
    );
};

export default MapField;

MapField.defaultProps = {
  coordinates: [],
};