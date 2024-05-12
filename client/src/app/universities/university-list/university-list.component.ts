import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-university-list',
  templateUrl: './university-list.component.html',
  styleUrls: ['./university-list.component.css'],
})
export class UniversityListComponent implements OnInit {
  unis: any;

  constructor() {}

  ngOnInit(): void {
    this.unis = [
      {
        name: 'AUC (The American university in Cairo)',
        imgsrc: 'https://picsum.photos/200/200',
        city: 'Cairo',
        location: 'New Cairo',
        website: 'https://www.aucegypt.edu',
      },
      {
        name: 'GUC (The German university in Cairo)',
        imgsrc: 'https://picsum.photos/200/200',
        city: 'Cairo',
        location: 'New Cairo',
        website: 'https://www.guc.edu.eg/',
      },
      {
        name: 'Cairo university',
        imgsrc: 'https://picsum.photos/200/200',
        city: 'Giza',
        location: 'Al Giza',
        website: 'https://cu.edu.eg/Home',
      },
    ];
  }
}
