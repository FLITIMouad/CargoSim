import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/notificationHub")
    .build();

connection.start()
    .then(() => console.log('Connected to SignalR'))
    .catch(err => console.error('SignalR Connection Error: ', err));

export default connection;
