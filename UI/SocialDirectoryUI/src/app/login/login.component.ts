import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginsService:LoginService,    private router: Router) { }

  ngOnInit(): void {
    // this.LoginClicked();
  }
  LoginClicked(loginForm:any) {
    
    if(loginForm.valid)
    {
      this.loginsService.Login(loginForm.value.username,loginForm.value.password).subscribe((data) => {
        if (data.isSuccess) {
          sessionStorage.setItem('token', data.token);
          this.router.navigate(['/matchfinder']);
          
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
