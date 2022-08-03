import { Component, OnInit } from '@angular/core';
import { FlightService } from 'src/app/Services/Flight/flight.service';
import { IFlight } from 'src/app/Interface/FlightModel';
import { IAirline } from 'src/app/Interface/AirlineModel';

@Component({
  selector: 'app-schedule-airline',
  templateUrl: './schedule-airline.component.html',
  styleUrls: ['./schedule-airline.component.css']
})
export class ScheduleAirlineComponent implements OnInit {
  scheduleType:number=1;
  mealType:number=1;
  Messsage:string=""
  airlineName:string = '';
  Airlines:IAirline[]=[];

  constructor(private _flight: FlightService) { }

  ngOnInit(): void {
    this._flight.GetAllAirLine()
    .subscribe((result) => {
      
       this.Airlines=result;
    })
  }

  ScheduleTypeSelect(event:any){
    this.scheduleType = event.target.value;
  }

  MealTypeSelect(event:any){
    this.mealType = event.target.value;
  }

  airlineNameSelect(event:any){
    this.airlineName = event.target.value
  }

  addAirlineormSubmit(data:IFlight){
    console.log(data)
    this._flight.addFlight(data)
    .subscribe((result) => {
      if(result){
        this.Messsage = "Flight Added"
      }
       
    })
  }

}
