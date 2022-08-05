import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Flight} from '../../Models/Flight';
import { CookieService } from 'ngx-cookie-service';
import {IAirline} from '../../Interface/AirlineModel';
import { IFlight } from 'src/app/Interface/FlightModel';

@Injectable({
  providedIn: 'root'
})

export class FlightService {
  baseUrl:string;
  url:string;
  token:string = '';

  constructor(private http: HttpClient, private _cookie:CookieService) {
    this.baseUrl = 'https://localhost:7024/Flight/';
    this.url = '';
   }

   AddAirLine(model:IAirline): Observable<any> {
   
    this.url = this.baseUrl + 'addairline'  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.post(this.url,model, {headers:header});
  }

  GetAllAirLine():Observable<any>{
    this.url = this.baseUrl + 'getallairline'  ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.get(this.url, {headers:header})
  }

  addFlight(model:IFlight): Observable<any> {
   
    this.url = this.baseUrl + 'addflight'  ;    
    this.token= this._cookie.get('token')
    console.log(model, this.token)
    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.post(this.url,model, {headers:header});
  }

  search(from:string, to:string, starttime:Date): Observable<any> {
   console.log(starttime)
    this.url = this.baseUrl + 'flights?from='+ from +'&to=' + to + '&date=' + starttime ;    
    console.log(this.url);
    return this.http.post(this.url,'')
  }

  BlockAirline(id:number): Observable<any> {
   
    this.url = this.baseUrl + 'blockAirline?id=' + id ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.post(this.url,'', {headers:header});
  }

  AddDiscount(airlinename:string, value:number): Observable<any> {
   
    this.url = this.baseUrl + 'adddiscount?name=' + airlinename+ '&value=' + value ;    
    this.token= this._cookie.get('token')

    let header = new HttpHeaders().set("Authorization", 'Bearer ' + this.token)
                                  .set('Content-Type', 'application/json');

    return this.http.post(this.url,'', {headers:header});
  }
}
