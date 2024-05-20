import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { Details } from '../_models/details';
import { Address } from '../_models/address';
import { Interests } from '../_models/interests';
import { Profile } from '../_models/profile';
import { University } from '../_models/university';
import { UniversityChoice } from '../_models/universityChoice';
import { GuardiansInfo } from '../_models/guardiansInfo';
import { Education } from '../_models/education';

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
      details,
      this.getHttpOptions()
    );
  }

  updateMemberAddress(address: Address) {
    return this.http.put<boolean>(
      this.baseUrl + 'users/updateAddress',
      address,
      this.getHttpOptions()
    );
  }

  updateInterests(interests: Interests) {
    return this.http.put<boolean>(
      this.baseUrl + 'users/updateInterests',
      interests,
      this.getHttpOptions()
    );
  }

  editProfile(profile: Profile) {
    return this.http.put<boolean>(
      this.baseUrl + 'users/editProfile',
      profile,
      this.getHttpOptions()
    );
  }

  getRecommendedUnis(email: string) {
    return this.http.get<University[]>(
      this.baseUrl + 'users/recommendedUnis?email=' + email,
      this.getHttpOptions()
    );
  }

  addApplication(email: string) {
    return this.http.get<boolean>(
      this.baseUrl + 'application/addApplication?email=' + email,
      this.getHttpOptions()
    );
  }

  addUniversityChoice(universityChoice: UniversityChoice) {
    return this.http.post<boolean>(
      this.baseUrl + 'application/addUniversityChoice',
      universityChoice,
      this.getHttpOptions()
    );
  }

  updateGuardiansInfo(guardiansInfo: GuardiansInfo) {
    return this.http.put<boolean>(
      this.baseUrl + 'users/updateGuardianInfo',
      guardiansInfo,
      this.getHttpOptions()
    );
  }

  updateEducation(education: Education) {
    return this.http.put<boolean>(
      this.baseUrl + 'users/updateEducation',
      education,
      this.getHttpOptions()
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
