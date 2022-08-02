import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserServiceService } from '../UserService/user-service.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errorMessage:string = '';
  constructor(private _router: Router, private _user: UserServiceService) { }

  ngOnInit(): void {
  }

  loginformSubmit(data:any){
    this._user.login(data.userName, data.password)
    .subscribe((result) => {
      if(result.refreshToken != undefined && result.refreshToken != "" && result.refreshToken != null){
        console.log(result)
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
