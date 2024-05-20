import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';
import { User } from '../_models/user';
import { University } from '../_models/university';
import { Router } from '@angular/router';
import { DataService } from '../_services/data.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  searchForm: FormGroup = new FormGroup({});
  recommendedUnis: University[] | undefined;
  allUnis: string[] = [];

  constructor(
    public accountService: AccountService,
    private memberService: MembersService,
    private dataService: DataService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.getUniNames();
    this.getRecommendedUnis();
  }

  getUniNames() {
    this.dataService.getUniversityNames().subscribe({
      next: (response) => {
        this.allUnis = response;
      },
    });
  }

  getRecommendedUnis() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.memberService.getRecommendedUnis(user.email).subscribe({
      next: (response) => {
        this.recommendedUnis = response;
      },
    });
  }

  searchUni() {
    if (String(this.searchForm.controls['search'].value) === '') {
      this.router.navigateByUrl('/university-list');
    } else {
      this.dataService
        .getUniversityByName(String(this.searchForm.controls['search'].value))
        .subscribe({
          next: (response) => {
            if (!response) {
              this.router.navigateByUrl('/university-list');
              return;
            }
            localStorage.setItem('uniData', JSON.stringify(response));
            this.router.navigateByUrl('/university-details');
          },
        });
    }
  }

  initializeForm() {
    this.searchForm = this.fb.group({
      search: [''],
    });
  }

  goToUniPage(index: number) {
    localStorage.setItem(
      'uniData',
      JSON.stringify(this.recommendedUnis![index])
    );
    this.router.navigateByUrl('/university-details');
  }
}
