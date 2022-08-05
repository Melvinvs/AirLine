import { Component, OnInit } from '@angular/core';
import { tick } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { GlobalComponent } from 'src/app/GlobalComponent';
import { IFlight } from 'src/app/Interface/FlightModel';
import { ITicket } from 'src/app/Interface/Ticket';
import { Flight } from 'src/app/Models/Flight';
import { Ticket } from 'src/app/Models/Ticket';
import { BookingService } from 'src/app/Services/Booking/booking.service';

@Component({
  selector: 'app-add-passenger',
  templateUrl: './add-passenger.component.html',
  styleUrls: ['./add-passenger.component.css']
})
export class AddPassengerComponent implements OnInit {
  flight:Flight = new Flight();
  tickets:Ticket[]=[]
  tempTickets:Ticket[]=[]
  currentTicket:ITicket = new Ticket();
  totalPersons:number = 0;
  totalAmount:number = 0;
  ticketPrize:number = 0;

  constructor(private route:Router, private _booking:BookingService) { 
    const data: any = route.getCurrentNavigation()?.extras.state!;
    this.flight = data.customdData
    this.ticketPrize = this.flight.discount > 0 ? (this.flight.ticketPrice - (this.flight.ticketPrice*this.flight.discount/100)) : this.flight.ticketPrice
  }

  ngOnInit(): void {
    if(!AppComponent.isLoggedIn){
      this.route.navigateByUrl("")
    }
  }

  addPassengerFormSubmit(data:Ticket, form:any){
    this.totalAmount += this.ticketPrize
    this.totalPersons +=1
    data.id=0
    data.flightNo = this.flight.flightNo
    data.airLineName = this.flight.airLineName
    data.fromPlace = this.flight.fromPlace
    data.toPlace = this.flight.toPlace
    data.startTime = this.flight.startTime
    data.endTime = this.flight.endTime
    data.ticketPrice = this.ticketPrize
    data.discount = this.flight.discount
    data.seatNo = ""
    data.pnr =''
    data.totalBSeats = this.flight.bussinesSeatNo
    data.totalESeats = this.flight.economySeatNo
    data.discount = this.flight.discount
    data.isCancelled = 0
    data.createdBy = GlobalComponent.loggedUserId
    this.tickets.push(data)  
    console.log(GlobalComponent.loggedUserId)
    console.log(this.tickets)
    form.reset()
  }

  removePassenger(email:string){
    this.tickets.forEach(x =>{
      if(x.email != email){
        this.tempTickets.push(x)
      }
    })

    this.totalAmount -= this.ticketPrize
    this.totalPersons -=1

    this.tickets = this.tempTickets
    this.tempTickets = []
  }

  confirmTicketBooking(){
    var status = false
    this.tickets.forEach(e => {
      console.log(e)
      this._booking.AddTicket(e)
      .subscribe((result) => {
        if(result != null){
          if(result.isBooked == 1){
            alert("Bokked successfully")
            this.route.navigateByUrl('/user')
          }
          else{
            alert("All seats are bokked")
          }
          
        }
         
      })
    });

    
  }

}
