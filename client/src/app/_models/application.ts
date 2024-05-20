import { Certificate } from './certificate';
import { University } from './university';

export interface Application {
  [key: string]: any;
  email: string;
  nationalId: string;
  passport: string;
  guardianName: string;
  guardianProfession: string;
  guardianCompany: string;
  guardianNumber: string;
  guardianEmail: string;
  schoolCountry: string;
  schoolCity: string;
  schoolName: string;
  yearOfGraduation: number | null;
  birthCertificate: string;
  transcript: string;
  recommendationLetter: string;
  personalPhoto: string;
  militaryForm2: string;
  militaryForm6: string;
  residencyCopy: string;
  personalStatement: string;
  certificates: Certificate[];
  universityChoices: University[];
}
