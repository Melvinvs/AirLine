import { Component, OnInit } from '@angular/core';
import { FlightService } from 'src/app/Services/Flight/flight.service';
import { Flight } from 'src/app/Models/Flight';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  flights:Flight[] = [];
  constructor(private _flight: FlightService) { }

  ngOnInit(): void {
  }

  searchformSubmit(data:any){
    
    this._flight.search(data.Departure, data.Destination, data.StartTime)
    .subscribe((result) => {
      
       this.flights=result;
       console.log(this.flights)
    })
  }

}
