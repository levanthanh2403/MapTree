import { AfterViewInit, Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import * as geojson from 'geojson';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.scss']
})
export class LocationsComponent implements AfterViewInit {

  private map;

  constructor() {
  }
  ngAfterViewInit(): void {
    this.map = L.map('map', {
      center: [21.027763, 105.834160],
      zoom: 12
    });

    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18,
      minZoom: 3,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(this.map);
  }
}
