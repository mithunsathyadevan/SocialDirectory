import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import { AuthService } from '../Auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  bntStyle='dd';
  heightStyle='dd4'
  constructor(private loginsService:LoginService,
        private router: Router,
        private authService:AuthService) { }

  ngOnInit(): void {
    // this.LoginClicked();
  }
  Style(styleVar:string)
  {
    this.bntStyle=styleVar;
    if(styleVar=='dd2')
    {
      this.heightStyle='dd3'
    }
    else
    {
      this.heightStyle='dd4'
    }
  }
  LoginClicked(loginForm:any) {
    
    if(loginForm.valid)
    {
      this.loginsService.Login(loginForm.value.username,loginForm.value.password).subscribe((data) => {
        if (data.isSuccess) {
          localStorage.setItem('token', data.token);
          this.router.navigate(['/contact']);
          this.authService.updateData(true);
        }
      }
      );
    }
    
  }
  SignUpClicked(signUp:any) {
    
    if(signUp.valid)
    {
      if(signUp.value.password1==signUp.value.password2)
      {
        this.loginsService.Register(signUp.value).subscribe((data) => {
          if (data.isSuccess) {
            sessionStorage.setItem('token', data.token);
            // this.router.navigate(['/matchfinder']);
            
          }
        }
        );
      }
     
    }
    
  }

}
