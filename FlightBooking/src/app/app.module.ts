import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './login/register/register.component';
import { ErrorComponent } from './error/error.component';
import { AddAirlineComponent } from './admin/add-airline/add-airline.component';
import { ManageAirlineComponent } from './admin/manage-airline/manage-airline.component';
import { ScheduleAirlineComponent } from './admin/schedule-airline/schedule-airline.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ManageDiscountComponent } from './admin/manage-discount/manage-discount.component';
import { AddPassengerComponent } from './user/add-passenger/add-passenger.component';
import { environment } from 'src/environments/environment';
import { ListBookedTicketsComponent } from './user/list-booked-tickets/list-booked-tickets.component';
import { SerarchByPnrEmailComponent } from './user/serarch-by-pnr-email/serarch-by-pnr-email.component';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    AdminComponent,
    LoginComponent,
    RegisterComponent,
    ErrorComponent,
    AddAirlineComponent,
    ManageAirlineComponent,
    ScheduleAirlineComponent,
    ManageDiscountComponent,
    AddPassengerComponent,
    ListBookedTicketsComponent,
    SerarchByPnrEmailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NoopAnimationsModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
