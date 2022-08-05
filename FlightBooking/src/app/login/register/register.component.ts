import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../../Services/UserService/user-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerStatus:boolean = false;
  constructor(private _user: UserServiceService, private _router: Router) { }

  ngOnInit(): void {
  }

  registerformSubmit(data:any){
    this._user.register(data.userName, data.password)
    .subscribe((result) => {
      if(result){
        this._router.navigateByUrl('')
      }
      else{
        this._router.navigateByUrl('/error')
      };
    })
    
  }

}
