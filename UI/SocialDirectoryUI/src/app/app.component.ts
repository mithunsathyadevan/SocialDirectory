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
 selectionList : number[] = [];
 
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
  Apply()
  {
    
    this.selectionList=[];
    this.subDropDown.forEach((element:any) => {
      if( element.selected==true)
      {
          this.selectionList.push(element.id);

      }
    });
   
     this.loadList();
    
 
    this.subDropDown;
  }
  loadList()
  {
    let objet={InterestIds:this.selectionList}
    this.commmonService.GetMatches(objet).subscribe((data) => {
      if(data)
      {
        this.commmonService.updateData(data); 
      }
   
   
   }
   );
  }
 
  public saveCode(e:any): void {
   
  }
  
  SearchButtonClicked(seachForm:any)
  {
    
    let drop = this.dropdown.find((user: any) => user.name === seachForm.value.search);
    if(drop===undefined)
    {
      this. subDropDown=[];
    }
    else
    {
      this.router.navigate(['/matchfinder']);
      this.selectionList=[];
      this.selectionList.push(drop.id);
      this.commmonService.GetSubInterest(drop.id).subscribe((data) => {
  
        this. subDropDown=data;
        
        this.Apply();
     
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

