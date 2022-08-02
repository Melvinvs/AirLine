import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Flight} from '../../Models/Flight';

@Injectable({
  providedIn: 'root'
})
export class FlightService {
  baseUrl:string;
  url:string;

  constructor(private http: HttpClient) {
    this.baseUrl = 'https://localhost:7024/Flight/';
    this.url = '';
   }

   addFlight(model:Flight): Observable<any> {
   
      this.url = this.baseUrl + 'addflight'  ;    
    console.log(this.url);
    console.log(model);
    return this.http.post(this.url,JSON.stringify(model))
  }

  search(from:string, to:string, starttime:Date): Observable<any> {
   
  this.url = this.baseUrl + 'flights?from=del&to=mum'  ;    
  console.log(this.url);
  return this.http.post(this.url,'')
}
}
