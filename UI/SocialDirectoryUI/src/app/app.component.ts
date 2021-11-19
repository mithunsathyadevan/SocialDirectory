import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Auth/auth.service';
import { CommonService } from './services/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  login=false;
  dropdown :any;
  subDropDown:any;
  title = 'SocialDirectoryUI';
  IsAuthenticated:any=false;
  ngOnInit(): void {
    this.IsAuthenticated=this.authService.isAuthenticated();
    this.authService.getData().subscribe(data => {
      this.IsAuthenticated = data;
    })
  }
  constructor(private commmonService:CommonService,    private router: Router,private authService:AuthService) { }
 
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
  Logout()
  {
    this.authService.logout();
    this.router.navigate(['']);
    this.IsAuthenticated=this.authService.isAuthenticated();
    
  }
 
 }

