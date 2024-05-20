import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-hub',
  templateUrl: './hub.component.html',
  styleUrls: ['./hub.component.css'],
})
export class HubComponent implements OnInit {
  progress = 0;
  member: Member | undefined;

  constructor(
    private memberService: MembersService,
    public router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getMember();
  }

  getMember() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.memberService.getMember(user.email).subscribe({
      next: (response) => {
        this.member = response;
        if (this.member.application) this.setProgressBar();
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

  createApplication() {
    this.memberService.addApplication(this.member?.email!).subscribe({
      next: (response) => {
        if (response) {
          this.toastr.success('Created application successfully!');
          this.router.navigateByUrl('/application');
        }
      },
    });
  }

  goToUniPage(index: number) {
    localStorage.setItem(
      'uniData',
      JSON.stringify(this.member?.favoriteUniversities[index])
    );
    this.router.navigateByUrl('/university-details');
  }
}
