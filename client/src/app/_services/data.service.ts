import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Governorate } from '../_models/governorate';
import { University } from '../_models/university';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  baseUrl = environment.apiUrl;
  api_key = 'AIzaSyBo845F377RqV1sV5v8gObydouh8L1iftA';

  constructor(private http: HttpClient) {}

  getGovernorates() {
    return this.http.get<Governorate[]>(this.baseUrl + 'data/governorates');
  }

  getSubjects() {
    return this.http.get<string[]>(this.baseUrl + 'data/subjects');
  }

  getIndustries() {
    return this.http.get<string[]>(this.baseUrl + 'data/industries');
  }

  getUniversities() {
    return this.http.get<University[]>(this.baseUrl + 'universities');
  }

  getUniversityNames() {
    return this.http.get<string[]>(this.baseUrl + 'data/universityNames');
  }

  getUniversityByName(uniName: string) {
    return this.http.get<University>(this.baseUrl + 'universities/' + uniName);
  }

  getGeocodeData(address: string) {
    const url = `https://maps.googleapis.com/maps/api/geocode/json?address=${encodeURIComponent(
      address
    )}&key=${this.api_key}`;

    return this.http.get<any>(url);
  }
}
