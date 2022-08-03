import { Component, OnInit } from '@angular/core';
import { IAirline } from 'src/app/Interface/AirlineModel';
import { FlightService } from 'src/app/Services/Flight/flight.service';

@Component({
  selector: 'app-manage-airline',
  templateUrl: './manage-airline.component.html',
  styleUrls: ['./manage-airline.component.css']
})
export class ManageAirlineComponent implements OnInit {
  Airlines:IAirline[] = []
  constructor(private _flight: FlightService) { }

  ngOnInit(): void {
    this.loadAirLineList()
  }

  loadAirLineList(){
    this._flight.GetAllAirLine()
    .subscribe((result) => {
      
       this.Airlines=result;
    })
  }

}
