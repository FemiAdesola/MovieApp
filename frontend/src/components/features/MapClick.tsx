import { useMapEvent } from "react-leaflet";

import { MapClickProps } from "../types/map";

const MapClick = (props: MapClickProps) => {
  useMapEvent("click", (eventArgs) => {
    props.setCoordinates({
      lat: eventArgs.latlng.lat,
      lng: eventArgs.latlng.lng,
    });
  });
  return null;
};

export default MapClick;
