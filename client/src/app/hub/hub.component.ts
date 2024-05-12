import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hub',
  templateUrl: './hub.component.html',
  styleUrls: ['./hub.component.css'],
})
export class HubComponent implements OnInit {
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
