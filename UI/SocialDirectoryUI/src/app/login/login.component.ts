import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginsService:LoginService) { }

  ngOnInit(): void {
    // this.LoginClicked();
  }
  LoginClicked(loginForm:any) {
    debugger
    this.loginsService.Login(loginForm.value.username,loginForm.value.password).subscribe((data) => {
      if (!!data) {
        sessionStorage.setItem('token', data.token);
      }
    }
    );
  }

}
