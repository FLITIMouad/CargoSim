
import { MapContainer,  TileLayer } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
const MapComponent = () => {
  const center = [51.505, -0.09]
  return (
      <MapContainer center={center}  className="mapContainer" zoom={13} scrollWheelZoom={true}>
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
      </MapContainer>
  );
}

export default MapComponent