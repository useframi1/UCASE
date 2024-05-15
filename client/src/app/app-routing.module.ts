import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterFormComponent } from './register/register-form/register-form.component';
import { UniversityListComponent } from './universities/university-list/university-list.component';
import { UniversityDetailComponent } from './universities/university-detail/university-detail.component';
import { HubComponent } from './hub/hub.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from './_guards/auth.guard';
import { ApplicationComponent } from './application/application.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { DetailsComponent } from './register/details/details.component';
import { AddressComponent } from './register/address/address.component';
import { InterestsComponent } from './register/interests/interests.component';
import { RegisterGuard } from './_guards/register.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'hub', component: HubComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'application', component: ApplicationComponent },
    ],
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [RegisterGuard],
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterFormComponent },
      { path: 'details', component: DetailsComponent },
      { path: 'address', component: AddressComponent },
      { path: 'interests', component: InterestsComponent },
    ],
  },
  { path: 'universities', component: UniversityListComponent },
  { path: 'university-details', component: UniversityDetailComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
