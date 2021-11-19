import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";


@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiURL = environment.apiPath;
  



  constructor(private httpClient: HttpClient) {

   }

  
  public GetMasterInterest(search:string): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/getinterests?search=${search}`;
    return this.httpClient.get(url);
  }
  public GetSubInterest(search:any): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/getSubInterests?id=${search}`;
    return this.httpClient.get(url);
  }

}



