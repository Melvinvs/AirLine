import { Component, OnInit } from '@angular/core';
import { FlightService } from 'src/app/Services/Flight/flight.service';
import { IAirline } from 'src/app/Interface/AirlineModel';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';

@Component({
  selector: 'app-manage-discount',
  templateUrl: './manage-discount.component.html',
  styleUrls: ['./manage-discount.component.css']
})
export class ManageDiscountComponent implements OnInit {
  Airlines:IAirline[]=[];
  Message:string = ''

  constructor(private _flight: FlightService) { }

  ngOnInit(): void {
    this._flight.GetAllAirLine()
    .subscribe((result) => {
      
       this.Airlines=result;
    })
  }

  addDiscontFormSubmit(form:any){
    console.log(form.value.airLineName, form.value.Discount)
  
  this._flight.AddDiscount(form.value.airLineName, form.value.Discount)
    .subscribe((result) => {
      
      if(result){
        this.Message = "Discount added"
        form.reset()
      }
    })
  }

}
