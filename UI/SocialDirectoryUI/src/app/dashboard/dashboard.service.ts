import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  apiURL = environment.apiPath;
  



  constructor(private httpClient: HttpClient) {

   }

   public Login(username:string,pass:string): Observable<any> {
    let objet={userName:username,password:pass};
    const url = `${this.apiURL}api/Authentication/Login`;
    return this.httpClient.post(url, objet);
  }
  public AddContactList(contactId:Number): Observable<any> {
    

    const url = `${this.apiURL}api/Contact/Save?contactId=${contactId}`;
    return this.httpClient.get(url);
  }
  public LoadData(): Observable<any> {
    
   
    const url = `${this.apiURL}api/Matching/getMatches`;
    return this.httpClient.get(url);
  }

}



