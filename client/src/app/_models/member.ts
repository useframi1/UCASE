import { Application } from './application';
import { PreferredIndustry } from './preferredIndustry';
import { PreferredSubject } from './preferredSubject';
import { University } from './university';

export interface Member {
  email: string;
  firstName: string;
  lastName: string;
  dob: Date;
  phoneNo: string;
  addressLine1: string;
  addressLine2: string;
  nationality: string;
  gender: string;
  govName: string;
  area: string;
  startUni: number;
  favoriteUniversities: University[];
  preferredIndustries: PreferredIndustry[];
  preferredSubjects: PreferredSubject[];
  application: Application;
}
