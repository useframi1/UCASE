import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Governorate } from '../_models/governorate';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getGovernorates() {
    return this.http.get<Governorate[]>(this.baseUrl + 'data/governorates');
  }
}
