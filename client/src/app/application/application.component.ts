import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { DataService } from '../_services/data.service';
import { University } from '../_models/university';
import { MembersService } from '../_services/members.service';
import { Member } from '../_models/member';
import { User } from '../_models/user';
import { UniversityChoice } from '../_models/universityChoice';
import { ToastrService } from 'ngx-toastr';
import { Application } from '../_models/application';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.css'],
})
export class ApplicationComponent implements OnInit {
  progress = 0;
  member: Member | undefined;
  chosenUniversity: University | undefined;
  universityNames: string[] = [];
  uniChoiceForm: FormGroup = new FormGroup({});
  guardianForm: FormGroup = new FormGroup({});
  educationForm: FormGroup = new FormGroup({});
  guardiansFormComplete: boolean = false;
  educationFormComplete: boolean = false;

  constructor(
    public router: Router,
    private dataService: DataService,
    private memberService: MembersService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeUniChoiceForm();
    this.initializeGuardianForm();
    this.initializeEducationForm();

    this.getMember();

    this.dataService.getUniversityNames().subscribe({
      next: (response) => {
        this.universityNames = response;
      },
    });
  }

  getMember() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.memberService.getMember(user.email).subscribe({
      next: (response) => {
        this.member = response;
        this.setProgressBar();
        this.guardiansFormComplete = this.guardianFieldsAreNull(
          this.member.application
        );
        this.educationFormComplete = this.educationFieldsAreNull(
          this.member.application
        );
      },
    });
  }

  setProgressBar() {
    const incomplete = Object.values(this.member?.application!).filter(
      (value) => {
        if (Array.isArray(value)) {
          return value.length === 0;
        } else {
          return value === null;
        }
      }
    ).length;

    this.progress = Math.floor(((21 - incomplete) / 21) * 100);
  }

  initializeUniChoiceForm() {
    this.uniChoiceForm = this.fb.group({
      university: [
        '',
        [
          Validators.required,
          this.checkIfExists('university', () => this.universityNames),
        ],
      ],
    });
  }

  initializeGuardianForm() {
    this.guardianForm = this.fb.group({
      guardianName: ['', [Validators.required]],
      guardianEmail: ['', [Validators.required, Validators.email]],
      guardianNumber: ['', [Validators.required, this.phoneNumberValidator()]],
      guardianProfession: ['', [Validators.required]],
      guardianCompany: ['', [Validators.required]],
    });
  }

  initializeEducationForm() {
    this.educationForm = this.fb.group({
      schoolName: ['', [Validators.required]],
      schoolCountry: ['', [Validators.required]],
      schoolCity: ['', [Validators.required]],
      yearOfGraduation: ['', [Validators.required]],
    });
  }

  phoneNumberValidator(): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === '' || /^01\d{9}$/.test(control.value)
        ? null
        : { invalidPhoneNumber: true };
    };
  }

  checkIfExists(
    fieldToCheck: string,
    fieldValues: () => string[]
  ): ValidatorFn {
    return (control: AbstractControl) => {
      const values = String(control.parent?.get(fieldToCheck)?.value).split(
        ','
      );
      for (let i = 0; i < values.length; i++) {
        if (!fieldValues().includes(values[i])) {
          return { notExists: true };
        }
      }
      return null;
    };
  }

  addChosenUniversity() {
    this.dataService
      .getUniversityByName(
        String(this.uniChoiceForm.controls['university'].value)
      )
      .subscribe({
        next: (response) => {
          this.member?.application.universityChoices.push(response);
          this.uniChoiceForm.reset();
        },
      });

    const universityChoice: UniversityChoice = {
      uniName: String(this.uniChoiceForm.controls['university'].value),
      email: this.member?.email!,
    };
    this.memberService.addUniversityChoice(universityChoice).subscribe({
      next: (response) => {
        if (response)
          this.toastr.success('Added university choice successfully!');
      },
    });
  }

  updateGuardianInfo() {
    const values = { ...this.guardianForm.value, email: this.member?.email };
    this.memberService.updateGuardiansInfo(values).subscribe({
      next: (response) => {
        if (response) {
          this.toastr.success('Added guardians info successfully!');
          this.member!.application.guardianName =
            this.guardianForm.controls['guardianName'].value;
          this.member!.application.guardianEmail =
            this.guardianForm.controls['guardianEmail'].value;
          this.member!.application.guardianNumber =
            this.guardianForm.controls['guardianNumber'].value;
          this.member!.application.guardianProfession =
            this.guardianForm.controls['guardianProfession'].value;
          this.member!.application.guardianCompany =
            this.guardianForm.controls['guardianCompany'].value;
          this.setProgressBar();
          this.guardiansFormComplete = true;
        }
      },
    });
  }

  updateEducation() {
    const values = {
      ...this.educationForm.value,
      email: this.member?.email,
      yearOfGraduation: Number(
        this.educationForm.controls['yearOfGraduation'].value
      ),
    };
    this.memberService.updateEducation(values).subscribe({
      next: (response) => {
        if (response) {
          this.toastr.success('Added education successfully!');
          this.member!.application.schoolName =
            this.educationForm.controls['schoolName'].value;
          this.member!.application.schoolCountry =
            this.educationForm.controls['schoolCountry'].value;
          this.member!.application.schoolCity =
            this.educationForm.controls['schoolCity'].value;
          this.member!.application.yearOfGraduation =
            this.educationForm.controls['yearOfGraduation'].value;
          this.setProgressBar();
          this.educationFormComplete = true;
        }
      },
    });
  }

  guardianFieldsAreNull(application: Application): boolean {
    let fieldsToCheck = [
      'guardianName',
      'guardianProfession',
      'guardianCompany',
      'guardianNumber',
      'guardianEmail',
    ];
    return fieldsToCheck.every((field) => (application as any)[field] !== null);
  }

  educationFieldsAreNull(application: Application): boolean {
    let fieldsToCheck = [
      'schoolName',
      'schoolCountry',
      'schoolCity',
      'yearOfGraduation',
    ];
    return fieldsToCheck.every((field) => (application as any)[field] !== null);
  }

  goToUniPage(index: number) {
    localStorage.setItem(
      'uniData',
      JSON.stringify(this.member?.application.universityChoices[index])
    );
    this.router.navigateByUrl('/university-details');
  }
}
