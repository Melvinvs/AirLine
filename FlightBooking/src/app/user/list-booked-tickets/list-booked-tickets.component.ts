import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { GlobalComponent } from 'src/app/GlobalComponent';
import { Ticket } from 'src/app/Models/Ticket';
import { BookingService } from 'src/app/Services/Booking/booking.service';

declare var require:any
const FileSaver = require('file-saver')

@Component({
  selector: 'app-list-booked-tickets',
  templateUrl: './list-booked-tickets.component.html',
  styleUrls: ['./list-booked-tickets.component.css']
})
export class ListBookedTicketsComponent implements OnInit {
  tickets:Ticket[]=[]
  constructor(private bokking:BookingService, private route:Router) { }

  ngOnInit(): void {
    if(!AppComponent.isLoggedIn){
      this.route.navigateByUrl("")
    }
    this.bokking.GetAllTickets(GlobalComponent.loggedUserId)
    .subscribe((result) => {
       this.tickets=result;
    })
  }

  

  CancelTicket(pnr:string){
    this.bokking.CancelTicket(pnr)
    .subscribe((result) => {
      this.ngOnInit()
    })
  }

  DownloadPdf(name:string){
    FileSaver.saveAs('./assets/Tickets/' + name + '.pdf', name + '.pdf')
  }

}
