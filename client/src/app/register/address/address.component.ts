import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Governorate } from 'src/app/_models/governorate';
import { User } from 'src/app/_models/user';
import { DataService } from 'src/app/_services/data.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css'],
})
export class AddressComponent implements OnInit {
  addressForm: FormGroup = new FormGroup({});
  governorates: Governorate[] = [];
  governoratesString: string[] = [];
  areas: string[] = [];

  constructor(
    private memberService: MembersService,
    private dataService: DataService,
    public router: Router,
    private fb: FormBuilder,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.governoratesString = [];
    this.dataService.getGovernorates().subscribe({
      next: (response) => {
        this.governorates = response;
        for (let i = 0; i < this.governorates.length; i++) {
          this.governoratesString[i] = this.governorates[i].govName;
        }
        this.governoratesString = [...new Set(this.governoratesString)];
      },
    });

    this.initializeForm();
  }

  goBack() {
    this.location.back();
  }

  initializeForm() {
    this.addressForm = this.fb.group({
      govName: [
        '',
        [
          Validators.required,
          this.checkIfExists('govName', () => this.governoratesString),
        ],
      ],
      area: [
        { value: '', disabled: true },
        [Validators.required, this.checkIfExists('area', () => this.areas)],
      ],
      addressLine1: ['', [Validators.required]],
      addressLine2: [''],
    });

    this.addressForm.controls['govName'].statusChanges.subscribe((status) => {
      if (status === 'INVALID') {
        this.addressForm.controls['area'].disable();
        this.addressForm.controls['area'].setValue('');
      } else {
        this.areas = [];
        this.addressForm.controls['area'].enable();
        var index = 0;
        for (let i = 0; i < this.governorates.length; i++) {
          if (
            this.governorates[i].govName ===
            this.addressForm.controls['govName'].value
          ) {
            this.areas[index] = this.governorates[i].area;
            index++;
          }
        }
      }
    });
  }

  checkIfExists(
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

  submitAddress() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    const addressLine2 =
      this.addressForm.controls['addressLine2'].value === ''
        ? null
        : this.addressForm.controls['addressLine2'].value;
    const values = {
      ...this.addressForm.value,
      email: user.email,
      addressLine2: addressLine2,
    };

    this.memberService.updateMemberAddress(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/interests');
      },
    });
  }
}
