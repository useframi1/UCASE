import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { University } from 'src/app/_models/university';
import { DataService } from 'src/app/_services/data.service';

@Component({
  selector: 'app-university-list',
  templateUrl: './university-list.component.html',
  styleUrls: ['./university-list.component.css'],
})
export class UniversityListComponent implements OnInit {
  unis: University[] = [];

  constructor(private dataService: DataService, private router: Router) {}

  ngOnInit(): void {
    this.dataService.getUniversities().subscribe({
      next: (response) => {
        this.unis = response;
      },
    });
  }

  goToUniPage(index: number) {
    localStorage.setItem('uniData', JSON.stringify(this.unis[index]));
    this.router.navigateByUrl('/university-details');
  }
}
