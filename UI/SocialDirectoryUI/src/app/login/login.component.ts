import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject,takeUntil } from 'rxjs';

import { AuthService } from '../Auth/auth.service';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  bntStyle = 'dd';
  heightStyle = 'dd4'
  private destroy$ = new Subject<void>();
  constructor(private loginsService: LoginService,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {
  }
  style(styleVar: string) {
    this.bntStyle = styleVar;
    if (styleVar == 'dd2') {
      this.heightStyle = 'dd3'
    } else {
      this.heightStyle = 'dd4'
    }
  }
  loginClicked(loginForm: any) {

    if (loginForm.valid) {
      this.loginsService.login(loginForm.value.username, loginForm.value.password).pipe(takeUntil(this.destroy$)).subscribe((data) => {
        if (data.isSuccess) {
          localStorage.setItem('token', data.token);
          this.router.navigate(['/contact']);
          this.authService.updateData(true);
        }
      }
      );
    }

  }
  signUpClicked(signUp: any) {
    if (signUp.valid) {
      if (signUp.value.password1 === signUp.value.password2) {
        this.loginsService.register(signUp.value).pipe(takeUntil(this.destroy$)).subscribe((data) => {
          if (data.isSuccess) {
            sessionStorage.setItem('token', data.token);
            // this.router.navigate(['/matchfinder']);

          }
        }
        );
      }
    }
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
