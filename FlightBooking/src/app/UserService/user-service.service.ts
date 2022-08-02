import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {
  baseUrl:string;
  url:string;

  constructor(private http: HttpClient) {
    this.baseUrl = 'https://localhost:7024/';
    this.url = '';
   }

   register(username:string,password:string): Observable<any> {
   
    this.url = this.baseUrl + 'Login/register?name='+ username +'&password='+ password  ;    
  console.log(this.url);
    return this.http.post(this.url,'')
  }

  login(username:string,password:string): Observable<any> {
   
    this.url = this.baseUrl + 'Login/login?name='+ username +'&password='+ password  ;    
  console.log(this.url);
    return this.http.post(this.url,'')
  }
}
