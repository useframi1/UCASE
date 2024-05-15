import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterFormComponent } from './register/register-form/register-form.component';
import { DetailsComponent } from './register/details/details.component';
import { AddressComponent } from './register/address/address.component';
import { InterestsComponent } from './register/interests/interests.component';
import { HubComponent } from './hub/hub.component';
import { ApplicationComponent } from './application/application.component';
import { ProfileComponent } from './profile/profile.component';
import { UniversityDetailComponent } from './universities/university-detail/university-detail.component';
import { UniversityListComponent } from './universities/university-list/university-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './_modules/shared.module';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { DatePickerComponent } from './_forms/date-picker/date-picker.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    RegisterFormComponent,
    DetailsComponent,
    AddressComponent,
    InterestsComponent,
    HubComponent,
    ApplicationComponent,
    ProfileComponent,
    UniversityDetailComponent,
    UniversityListComponent,
    NotFoundComponent,
    ServerErrorComponent,
    TextInputComponent,
    DatePickerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    SharedModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
