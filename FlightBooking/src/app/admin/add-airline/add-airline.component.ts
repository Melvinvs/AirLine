import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { IAirline } from 'src/app/Interface/AirlineModel';
import { FlightService } from 'src/app/Services/Flight/flight.service';
import { ManageAirlineComponent } from '../manage-airline/manage-airline.component';

@Component({
  selector: 'app-add-airline',
  templateUrl: './add-airline.component.html',
  styleUrls: ['./add-airline.component.css']
})
export class AddAirlineComponent implements OnInit {
  airlineID:number=0;
  mealType:number=1;
  Messsage:string=""

  constructor(private _flight: FlightService, private _manageAirline: ManageAirlineComponent) { }

  ngOnInit(): void {
  }

  addAirlineormSubmit(data:IAirline, form:any){
    this._flight.AddAirLine(data)
    .subscribe((result) => {
      if(result){
        this._manageAirline.loadAirLineList()
        form.reset()
      }
       
    })
  }

}
