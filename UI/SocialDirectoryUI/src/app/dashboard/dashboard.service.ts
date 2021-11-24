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


  public addContactList(contactId: Number): Observable<any> {
    const url = `${this.apiURL}api/Contact/Save?contactId=${contactId}`;
    return this.httpClient.get(url);
  }
  public loadData(): Observable<any> {
    const url = `${this.apiURL}api/Matching/getMatches`;
    return this.httpClient.get(url);
  }

}



