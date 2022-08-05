import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  adminOption:string = 'manageAirline';
  constructor(private route:Router) { }

  ngOnInit(): void {
    if(!AppComponent.isLoggedIn){
      this.route.navigateByUrl("")
    }
  }

}
