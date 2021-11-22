import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
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
  public GetSubInterest(search:any,type:string): Observable<any> {
    
   
    const url = `${this.apiURL}api/Interest/getSubInterests?id=${search}&&type=${type}`;
    return this.httpClient.get(url);
  }
  public GetMatches(obj:any): Observable<any> {
    
   
    const url = `${this.apiURL}api/Matching/ListInterest`;
    return this.httpClient.post(url,obj);
  }
  private dataObs$ = new Subject();

  getData() {
      return this.dataObs$;
  }

  updateData(data: boolean) {
      this.dataObs$.next(data);
  }

}



