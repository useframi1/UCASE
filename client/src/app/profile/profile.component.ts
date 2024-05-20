import { Component, OnInit } from '@angular/core';
import { MembersService } from '../_services/members.service';
import { DataService } from '../_services/data.service';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Governorate } from '../_models/governorate';
import { User } from '../_models/user';
import { Member } from '../_models/member';
import { DatePipe, Location } from '@angular/common';
import { forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  member?: Member | undefined;
  profileForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  governorates: Governorate[] = [];
  governoratesString: string[] = [];
  areas: string[] = [];
  subjects: string[] = [];
  industries: string[] = [];
  initialFormValues: any;
  formChanged: boolean = false;
  formIsInitialized: boolean = false;

  constructor(
    private accountService: AccountService,
    private memberService: MembersService,
    private dataService: DataService,
    private fb: FormBuilder,
    private location: Location,
    private toastr: ToastrService,
    private router: Router,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.getMember();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 16);
  }

  getMember() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.memberService.getMember(user.email).subscribe({
      next: (response) => {
        this.member = response;
        this.initializeData();
      },
    });
  }

  initializeForm() {
    let formattedDate = this.datePipe.transform(
      this.member?.dob,
      'dd MMMM yyyy'
    );
    this.profileForm = this.fb.group({
      firstName: [this.member?.firstName, Validators.required],
      lastName: [this.member?.lastName],
      dob: [formattedDate, [Validators.required]],
      gender: [this.member?.gender == 'M' ? 'Male' : 'Female'],
      phoneNo: [
        this.member?.phoneNo,
        [Validators.required, this.phoneNumberValidator()],
      ],
      nationality: [this.member?.nationality],
      startUni: [String(this.member?.startUni)],
      govName: [
        this.member?.govName,
        [
          Validators.required,
          this.checkIfAddressExists('govName', () => this.governoratesString),
        ],
      ],
      area: [
        this.member?.area,
        [
          Validators.required,
          this.checkIfAddressExists('area', () => this.areas),
        ],
      ],
      addressLine1: [this.member?.addressLine1, [Validators.required]],
      addressLine2: [this.member?.addressLine2],
      subject: [
        this.member?.preferredSubjects.map((ps) => ps.subject).join(','),
        [
          Validators.required,
          this.checkIfInterestExists('subject', () => this.subjects),
        ],
      ],
      industry: [
        this.member?.preferredIndustries.map((pi) => pi.industry).join(','),
        [
          Validators.required,
          this.checkIfInterestExists('industry', () => this.industries),
        ],
      ],
    });

    this.initialFormValues = this.profileForm.value;
    this.formIsInitialized = true;

    this.profileForm.valueChanges.subscribe(() => {
      const currentFormValues = this.profileForm.value;
      if (
        JSON.stringify(currentFormValues) !==
        JSON.stringify(this.initialFormValues)
      ) {
        this.formChanged = true;
      } else {
        this.formChanged = false;
      }
    });

    this.profileForm.controls['govName'].statusChanges.subscribe((status) => {
      if (status === 'INVALID') {
        this.profileForm.controls['area'].disable();
        this.profileForm.controls['area'].setValue('');
      } else {
        this.areas = [];
        this.profileForm.controls['area'].enable();
        var index = 0;
        for (let i = 0; i < this.governorates.length; i++) {
          if (
            this.governorates[i].govName ===
            this.profileForm.controls['govName'].value
          ) {
            this.areas[index] = this.governorates[i].area;
            index++;
          }
        }
      }
    });
  }

  initializeData() {
    this.governoratesString = [];
    const observables = [
      this.dataService.getGovernorates(),
      this.dataService.getSubjects(),
      this.dataService.getIndustries(),
    ];

    forkJoin({
      governorates: observables[0],
      subjects: observables[1],
      industries: observables[2],
    }).subscribe(({ governorates, subjects, industries }) => {
      this.governorates = governorates as Governorate[];
      for (let i = 0; i < this.governorates.length; i++) {
        this.governoratesString[i] = this.governorates[i].govName;
      }
      this.governoratesString = [...new Set(this.governoratesString)];

      this.areas = [];
      var index = 0;
      for (let i = 0; i < this.governorates.length; i++) {
        if (this.governorates[i].govName === this.member?.govName) {
          this.areas[index] = this.governorates[i].area;
          index++;
        }
      }

      this.subjects = subjects as string[];
      this.industries = industries as string[];

      this.initializeForm();
    });
  }

  phoneNumberValidator(): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === '' || /^01\d{9}$/.test(control.value)
        ? null
        : { invalidPhoneNumber: true };
    };
  }

  checkIfAddressExists(
    fieldToCheck: string,
    fieldValues: () => string[]
  ): ValidatorFn {
    return (control: AbstractControl) => {
      return !fieldValues() ||
        fieldValues().includes(control.parent?.get(fieldToCheck)?.value) ||
        control.parent?.get(fieldToCheck)?.value == ''
        ? null
        : { notExists: true };
    };
  }

  checkIfInterestExists(
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

  goBack() {
    this.location.back();
  }

  submitForm() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    var user: User = JSON.parse(userString);
    user.firstName = String(this.profileForm.controls['firstName'].value);
    user.lastName = String(this.profileForm.controls['lastName'].value);
    localStorage.setItem('user', JSON.stringify(user));
    this.accountService.setCurrentUser(user);

    const addressLine2 =
      this.profileForm.controls['addressLine2'].value === ''
        ? null
        : this.profileForm.controls['addressLine2'].value;
    const dob = this.getDateOnly(this.profileForm.controls['dob'].value);
    const startUni = Number(this.profileForm.controls['startUni'].value);
    const gender = String(this.profileForm.controls['gender'].value)[0];
    const subjects = [
      ...new Set(String(this.profileForm.controls['subject'].value).split(',')),
    ];
    const industries = [
      ...new Set(
        String(this.profileForm.controls['industry'].value).split(',')
      ),
    ];
    const values = {
      ...this.profileForm.value,
      email: this.member?.email,
      addressLine2: addressLine2,
      dob: dob,
      startUni: startUni,
      gender: gender,
      subjects: subjects,
      industries: industries,
    };

    delete values.industry;
    delete values.subject;

    this.memberService.editProfile(values).subscribe({
      next: (response) => {
        if (response) {
          this.toastr.success('Profile updated successfully!');
          this.goBack();
        }
      },
    });
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob) return;
    let theDob = new Date(dob);
    return new Date(
      theDob.setMinutes(theDob.getMinutes() - theDob.getTimezoneOffset())
    )
      .toISOString()
      .slice(0, 10);
  }
}
