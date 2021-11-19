import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public getToken() {
    
    const userJson = localStorage.getItem('token');
    return userJson;
  }
  private dataObs$ = new Subject();

    getData() {
        return this.dataObs$;
    }

    updateData(data: boolean) {
        this.dataObs$.next(data);
    }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    if(token===undefined || token==null)
    {
      return false;
    }else
    {
      return true;
    }
    // return a boolean reflecting 
    // whether or not the token is expired
  }
  public logout()
  {
    localStorage.removeItem('token');
  }
}
