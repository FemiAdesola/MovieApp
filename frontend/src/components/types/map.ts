export interface MapProps {
  height: string;
  coordinates: CoordinateDTO[];
  handleMapClick(coordinates: CoordinateDTO): void;
//   readOnly: boolean;
}

export  interface MapClickProps {
  setCoordinates(coordinates: CoordinateDTO): void;
}

export interface CoordinateDTO {
  lng: number;
  lat: number;
  name?: string;
}


