import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiURL = environment.apiPath;
  



  constructor(private httpClient: HttpClient) {

   }

   public Login(username:string,pass:string): Observable<any> {
    let objet={userName:username,password:pass};
    const url = `${this.apiURL}api/Authentication/Login`;
    return this.httpClient.post(url, objet);
  }
  public Register(register:any): Observable<any> {
    
    let objet={password:register.password1,
      firstName:register.FirstName,
      lastName:register.LastName,
      middleName:register.MiddleName,
    email:register.email,
  location:register.Location};
    const url = `${this.apiURL}api/Authentication/RegisterUser`;
    return this.httpClient.post(url, objet);
  }

}


