import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonService } from './services/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  login=false;
  dropdown :any;
  subDropDown:any;
  title = 'SocialDirectoryUI';
  constructor(private commmonService:CommonService,    private router: Router) { }
  onKeyDownEvent(event: any){

    console.log(event.target.value);
    this.commmonService.GetMasterInterest(event.target.value).subscribe((data) => {
  
       this. dropdown=data;
        
    
    }
    );
  }
  public saveCode(e:any): void {
   debugger
  }
  
  SearchButtonClicked(seachForm:any)
  {
    debugger
    let user = this.dropdown.find((user: any) => user.name === seachForm.value.search);
    if(user===undefined)
    {
      this. subDropDown=[];
    }
    else
    {
      this.commmonService.GetSubInterest(user.id).subscribe((data) => {
  
        this. subDropDown=data;
        this.router.navigate(['/matchfinder']);
         
     
     }
     );
    }
    
  }
 
 }

