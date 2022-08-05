import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { ITicket } from 'src/app/Interface/Ticket';
import { Ticket } from 'src/app/Models/Ticket';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  baseUrl:string;
  url:string;
  token:string = '';

  constructor(private http: HttpClient, private _cookie:CookieService) {
    this.baseUrl = 'https://localhost:7024/Booking/';
    this.url = '';
   }

   AddTicket(model:Ticket): Observable<any> {
   
    this.url = this.baseUrl + 'AddTicket'  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.post(this.url,model, {headers:header});
   }

   GetAllTickets(userid:number):Observable<any>{
     console.log("stservice:" ,userid)
    this.url = this.baseUrl + 'ticketsbyuserid?UserID=' + userid  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.get(this.url, {headers:header})
  }

  CancelTicket(PNR:string):Observable<any>{
    this.url = this.baseUrl + 'CancelTicket?PNR=' + PNR  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.get(this.url, {headers:header})
  }

  SearchByPNR(PNR:string):Observable<any>{
    this.url = this.baseUrl + 'SearchByPNR?PNR=' + PNR  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.get(this.url, {headers:header})
  }

  SearchByemail(email:string):Observable<any>{
    this.url = this.baseUrl + 'SearchByemail?email=' + email  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.get(this.url, {headers:header})
  }
}
