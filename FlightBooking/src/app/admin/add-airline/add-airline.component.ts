import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { Flight } from 'src/app/Models/Flight';
import { FlightService } from 'src/app/Services/Flight/flight.service';

@Component({
  selector: 'app-add-airline',
  templateUrl: './add-airline.component.html',
  styleUrls: ['./add-airline.component.css']
})
export class AddAirlineComponent implements OnInit {
  scheduleType:number=1;
  mealType:number=1;
  Messsage:string=""
  constructor(private _flight: FlightService) { }

  ngOnInit(): void {
  }

  ScheduleTypeSelect(event:any){
    this.scheduleType = event.target.value;
  }

  MealTypeSelect(event:any){
    this.mealType = event.target.value;
  }

  addAirlineormSubmit(data:Flight){
    console.log(data)
    this._flight.addFlight(data)
    .subscribe((result) => {
      if(result){
        this.Messsage = "Flight Added"
      }
       
    })
  }

}
