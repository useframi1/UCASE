import { PreferredIndustry } from './preferredIndustry';
import { FavoriteUniversity } from './favoriteUniversity';
import { PreferredSubject } from './preferredSubject';

export interface Member {
  email: string;
  firstName: string;
  lastName: string;
  dob: Date;
  phoneno: string;
  addressline1: string;
  addressline2: string;
  nationality: string;
  gender: string;
  govName: string;
  area: string;
  favoriteUniversities: FavoriteUniversity[];
  preferredIndustries: PreferredIndustry[];
  preferredSubjects: PreferredSubject[];
}
