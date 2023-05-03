import React, { useState } from 'react';
import icon from "leaflet/dist/images/marker-icon.png";
import iconShadow from "leaflet/dist/images/marker-shadow.png";
import L from "leaflet";
import "leaflet/dist/leaflet.css";
import { MapContainer, Marker, TileLayer } from "react-leaflet";

import { CoordinateDTO, MapProps } from "../types/map";
import MapClick from '../features/MapClick';


let defaultIcon = L.icon({
  iconUrl: icon,
  shadowUrl: iconShadow,
  iconAnchor: [16, 37],
});

L.Marker.prototype.options.icon = defaultIcon;


const Map = (props: MapProps) => {
    const [coordinates, setCoordinates] = useState<CoordinateDTO[]>(
      props.coordinates
    );
    return (
      <MapContainer
        center={[60.1699, 24.9384]}
        zoom={14}
        style={{ height: props.height }}
      >
        <TileLayer
          attribution="Movie App"
          url="https://tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        {/* {props.readOnly ? null : ( */}
        <MapClick
          setCoordinates={(coordinates) => {
            setCoordinates([coordinates]);
            props.handleMapClick(coordinates);
          }}
        />
        {/* )} */}

        {coordinates.map((coordinate, index) => (
          <Marker key={index} position={[coordinate.lat, coordinate.lng]}>
            {/* {coordinate.name ? <Popup>{coordinate.name}</Popup> : null} */}
          </Marker>
        ))}
      </MapContainer>
    );
};

export default Map;

Map.defaultProps = {
    height: '500px'
}

