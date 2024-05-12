import { Component, OnInit } from '@angular/core';
import * as L from 'leaflet';

@Component({
  selector: 'app-university-detail',
  templateUrl: './university-detail.component.html',
  styleUrls: ['./university-detail.component.css'],
})
export class UniversityDetailComponent implements OnInit {
  map: any;

  initMap(): void {
    this.map = L.map('map').setView([51.505, -0.09], 13);

    L.tileLayer('https://tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(this.map);

    L.marker([51.5, -0.09], {
      icon: L.icon({
        iconUrl: 'assets/leaflet/marker-icon.png',
        shadowUrl: 'assets/leaflet/marker-shadow.png',
        iconRetinaUrl: 'assets/leaflet/marker-icon-2x.png',
        iconSize: [25, 41], // size of the icon
        shadowSize: [41, 41], // size of the shadow
        iconAnchor: [12, 41], // point of the icon which will correspond to marker's location
        shadowAnchor: [13, 41], // the same for the shadow
        popupAnchor: [1, -34], // point from which the popup should open relative to the iconAnchor
      }),
    })
      .addTo(this.map)
      .openPopup();
  }

  constructor() {}

  ngOnInit(): void {
    this.initMap();
  }
}
