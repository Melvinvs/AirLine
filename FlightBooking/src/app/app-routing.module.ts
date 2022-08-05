import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAirlineComponent } from './admin/add-airline/add-airline.component';
import { AdminComponent } from './admin/admin.component';
import { ErrorComponent } from './error/error.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './login/register/register.component';
import { AddPassengerComponent } from './user/add-passenger/add-passenger.component';
import { ListBookedTicketsComponent } from './user/list-booked-tickets/list-booked-tickets.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {
    path:'',
    component:UserComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'register',
    component:RegisterComponent
  },
  {
    path:'user',
    component:UserComponent
  },
  {
    path:'admin',
    component:AdminComponent 
  },
  {
    path:'addPassenger',
    component:AddPassengerComponent 
  },
  {
    path:'listTickets',
    component:ListBookedTicketsComponent 
  },
  {
    path:'**',
    component:ErrorComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
