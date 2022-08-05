import { Component, OnInit } from '@angular/core';
import { FlightService } from 'src/app/Services/Flight/flight.service';
import { Flight } from 'src/app/Models/Flight';
import { IFlight } from 'src/app/Interface/FlightModel';
import { Router,NavigationExtras } from '@angular/router';
import { AppComponent } from '../app.component';
import { ListBookedTicketsComponent } from './list-booked-tickets/list-booked-tickets.component';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  flights:Flight[] = [];
  userOption:string ="SearchFlight"
  constructor(private _flight: FlightService, private _router: Router) { }

  ngOnInit(): void {
  }

  searchformSubmit(data:any){
    
    this._flight.search(data.Departure, data.Destination, data.StartTime)
    .subscribe((result) => {
      
       this.flights=result;

       
    })
  }

  BooKTicket(data:Flight){
    let navigationExtras: NavigationExtras = {
      state: {
          customdData: JSON.stringify(data)
      }
  };
    this._router.navigate(['/addPassenger'], {
      state: {
          customdData: data
      }
    })
  }

  get isLoggedIn() {
    return AppComponent.isLoggedIn;
  }

}
