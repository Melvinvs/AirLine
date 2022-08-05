import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserServiceService } from '../Services/UserService/user-service.service';
import { CookieService } from 'ngx-cookie-service';
import { GlobalComponent } from '../GlobalComponent';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errorMessage:string = '';
  constructor(private _router: Router, private _user: UserServiceService, private _cookie:CookieService) { }

  ngOnInit(): void {
  }

  loginformSubmit(data:any){
    this._user.login(data.userName, data.password)
    .subscribe((result) => {
      if(result.refreshToken != undefined && result.refreshToken != "" && result.refreshToken != null){
        this._cookie.set('token', result.refreshToken)
        
        GlobalComponent.loggedUserId = result.id
        
        AppComponent.isLoggedIn = true
        if(result.roleType == 1){

          this._router.navigateByUrl('/admin')
        }
        else{

          this._router.navigateByUrl('/user')
        }
      }
      else{
        this.errorMessage = 'Invalid username or password';
      };
    })
    
  }
}
