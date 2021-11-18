import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public getToken() {
    
    const userJson = localStorage.getItem('token');
    return userJson;
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    if(token===undefined)
    {
      return false;
    }else
    {
      return true;
    }
    // return a boolean reflecting 
    // whether or not the token is expired
  }
}
