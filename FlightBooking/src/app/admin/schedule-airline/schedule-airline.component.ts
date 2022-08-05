import { Component, OnInit } from '@angular/core';
import { FlightService } from 'src/app/Services/Flight/flight.service';
import { IFlight } from 'src/app/Interface/FlightModel';
import { IAirline } from 'src/app/Interface/AirlineModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-schedule-airline',
  templateUrl: './schedule-airline.component.html',
  styleUrls: ['./schedule-airline.component.css']
})
export class ScheduleAirlineComponent implements OnInit {
  ManageScheduleOptions:number = 0;
  Messsage:string=""

  Airlines:IAirline[]=[];

  constructor(private _flight: FlightService, private route:Router) { }

  ngOnInit(): void {
    this._flight.GetAllAirLine()
    .subscribe((result) => {
      
       this.Airlines=result;
    })
  }


  addScheduleFormSubmit(data:IFlight, form:any){
    console.log(data)
    this._flight.addFlight(data)
    .subscribe((result) => {
      if(result){
        alert("Flight Added")
        form.reset()
      }
       
    })
  }

}
