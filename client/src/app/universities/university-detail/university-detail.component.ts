import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as L from 'leaflet';
import { University } from 'src/app/_models/university';
import { DataService } from 'src/app/_services/data.service';

@Component({
  selector: 'app-university-detail',
  templateUrl: './university-detail.component.html',
  styleUrls: ['./university-detail.component.css'],
})
export class UniversityDetailComponent implements OnInit, OnDestroy {
  map: any;
  uniData: University | undefined;
  lat: any;
  long: any;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    var uniDataString = localStorage.getItem('uniData');
    if (!uniDataString) return;
    this.uniData = JSON.parse(uniDataString);
    this.initMap();
  }

  ngOnDestroy(): void {
    localStorage.removeItem('uniData');
  }

  initMap() {
    this.dataService.getGeocodeData(this.uniData?.uniName!).subscribe({
      next: (response) => {
        if (response.status === 'OK') {
          const location = response.results[0].geometry.location;
          this.lat = location.lat;
          this.long = location.lng;
          this.setMap();
        }
      },
    });
  }

  setMap(): void {
    this.map = L.map('map').setView([this.lat, this.long], 16);

    L.tileLayer('https://tile.openstreetmap.fr/hot/{z}/{x}/{y}.png', {
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(this.map);

    L.marker([this.lat, this.long], {
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
}
