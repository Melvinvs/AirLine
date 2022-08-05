import { Component, OnInit } from '@angular/core';
import { Ticket } from 'src/app/Models/Ticket';
import { BookingService } from 'src/app/Services/Booking/booking.service';

@Component({
  selector: 'app-serarch-by-pnr-email',
  templateUrl: './serarch-by-pnr-email.component.html',
  styleUrls: ['./serarch-by-pnr-email.component.css']
})
export class SerarchByPnrEmailComponent implements OnInit {
  searchType:number=0
  SearchValue:string = ''
  tickets:Ticket[]=[]
  notickets:number=0
  constructor(private booking:BookingService) { }

  ngOnInit(): void {
  }

  onClickSearch(){
    if(this.searchType == 0){
      this.booking.SearchByemail(this.SearchValue)
      .subscribe((result) => {
      this.tickets = result
      this.notickets = this.tickets.length ==0?1:0
    })
    }
    else{
      this.booking.SearchByPNR(this.SearchValue)
      .subscribe((result) => {
      this.tickets = result
      this.notickets = this.tickets.length ==0?1:0
    })
    }
  }

}
