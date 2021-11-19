import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class ContactlistService {
  apiURL = environment.apiPath;
  



  constructor(private httpClient: HttpClient) {

   }
  public LoadData(): Observable<any> {
    
   
    const url = `${this.apiURL}api/Contact/ListContacts`;
    return this.httpClient.get(url);
  }
  public GetUserDetails(): Observable<any> {
    
   
    const url = `${this.apiURL}api/Authentication/GetUserDetails`;
    return this.httpClient.get(url);
  }
  public GetUsersInterests(): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/getUserInterests`;
    return this.httpClient.get(url);
  }
  public SaveInterests(id:number): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/Save?interestId=${id}`;
    return this.httpClient.get(url);
  }
  public DeleteInterests(id:number): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/DeleteUserInterest?interestId=${id}`;
    return this.httpClient.get(url);
  }
  public DeleteContact(id:number): Observable<any> {
    
   
    const url = `${this.apiURL}api/Contact/Delete?contactId=${id}`;
    return this.httpClient.get(url);
  }
  public GetAllInterests(): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/getAllInterest`;
    return this.httpClient.get(url);
  }
}
