import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Auth/auth.guard';
import { ContactlistComponent } from './components/contactlist/contactlist.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [  { path: '', component: LoginComponent },
{ path: 'matchfinder', component: DashboardComponent,canActivate:[AuthGuard] },
{ path: 'contact', component: ContactlistComponent,canActivate:[AuthGuard] }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
