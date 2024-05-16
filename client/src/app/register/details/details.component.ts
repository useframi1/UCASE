import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/user';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
})
export class DetailsComponent implements OnInit {
  detailsForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();

  constructor(
    private memberService: MembersService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 16);
  }

  initializeForm() {
    this.detailsForm = this.fb.group({
      dob: ['', [Validators.required]],
      gender: ['Male'],
      phoneno: ['', [Validators.required, this.phoneNumberValidator()]],
      nationality: ['Egyptian'],
      startUni: ['2024'],
    });
  }

  phoneNumberValidator(): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === '' || /^01\d{9}$/.test(control.value)
        ? null
        : { invalidPhoneNumber: true };
    };
  }

  submitDetails() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    const dob = this.getDateOnly(this.detailsForm.controls['dob'].value);
    const startUni = Number(this.detailsForm.controls['startUni'].value);
    const gender = String(this.detailsForm.controls['gender'].value)[0];
    const values = {
      ...this.detailsForm.value,
      dob: dob,
      email: user.email,
      startUni: startUni,
      gender: gender,
    };

    this.memberService.updateMemberDetails(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/address');
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
