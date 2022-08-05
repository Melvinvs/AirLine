import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { Component } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { GlobalComponent } from './GlobalComponent';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public static isLoggedIn:boolean = false
  isAdmin:boolean = false
  title = 'FlightBooking';

  constructor(private _cookie:CookieService){
  }

  onLogout(){
    this._cookie.delete('token')
  }

  get isLoggedIn() {
    return AppComponent.isLoggedIn;
  }
}
