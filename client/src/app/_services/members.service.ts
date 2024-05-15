import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { Details } from '../_models/details';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMembers() {
    return this.http.get<Member[]>(
      this.baseUrl + 'users',
      this.getHttpOptions()
    );
  }

  getMember(email: string) {
    return this.http.get<Member>(
      this.baseUrl + 'users/' + email,
      this.getHttpOptions()
    );
  }

  updateMemberDetails(details: Details) {
    return this.http.put<boolean>(
      this.baseUrl + 'users/updateDetails',
      details
    );
  }

  getHttpOptions() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token,
      }),
    };
  }
}
