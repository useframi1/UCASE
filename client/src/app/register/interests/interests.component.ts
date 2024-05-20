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
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { DataService } from 'src/app/_services/data.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-interests',
  templateUrl: './interests.component.html',
  styleUrls: ['./interests.component.css'],
})
export class InterestsComponent implements OnInit {
  interestsForm: FormGroup = new FormGroup({});
  subjects: string[] = [];
  universities: string[] = [];
  industries: string[] = [];

  constructor(
    private memberService: MembersService,
    private dataService: DataService,
    public router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.dataService.getSubjects().subscribe({
      next: (response) => {
        this.subjects = response;
      },
    });

    this.dataService.getIndustries().subscribe({
      next: (response) => {
        this.industries = response;
      },
    });

    this.dataService.getUniversityNames().subscribe({
      next: (response) => {
        this.universities = response;
      },
    });

    this.initializeForm();
  }

  goBack() {
    this.location.back();
  }

  initializeForm() {
    this.interestsForm = this.fb.group({
      subject: [
        '',
        [
          Validators.required,
          this.checkIfExists('subject', () => this.subjects),
        ],
      ],
      university: [
        '',
        [
          Validators.required,
          this.checkIfExists('university', () => this.universities),
        ],
      ],
      industry: [
        '',
        [
          Validators.required,
          this.checkIfExists('industry', () => this.industries),
        ],
      ],
    });
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

  submitInterests() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    const subjects = [
      ...new Set(
        String(this.interestsForm.controls['subject'].value).split(',')
      ),
    ];
    const universities = [
      ...new Set(
        String(this.interestsForm.controls['university'].value).split(',')
      ),
    ];
    const industries = [
      ...new Set(
        String(this.interestsForm.controls['industry'].value).split(',')
      ),
    ];
    const values = {
      subjects: subjects,
      universities: universities,
      industries: industries,
      email: user.email,
    };

    this.memberService.updateInterests(values).subscribe({
      next: (response) => {
        if (response) {
          this.toastr.success("You're now all set!");
          this.router.navigateByUrl('/');
        }
      },
    });
  }
}
