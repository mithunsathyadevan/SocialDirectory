import { Component, OnInit } from '@angular/core';
import { ContactlistService } from './contactlist.service';

@Component({
  selector: 'app-contactlist',
  templateUrl: './contactlist.component.html',
  styleUrls: ['./contactlist.component.css']
})
export class ContactlistComponent implements OnInit {
  users:any;
  selectedInterest=null;
  userDetails:any;
  interst:any;
  popoverMessage="Are you sure you want to delete this item?";
  popoverTitle="Confirmation"
  interestList:any;
  constructor(private contactService:ContactlistService) { }

  ngOnInit(): void {
    
    this.loadData();
    this.loadDataUserDetails();
    this.GetUsersInterests();
    this.GetAllInterest();
  }
  GetAllInterest()
  {
    this.contactService.GetAllInterests().subscribe((data) => {
  
      this.interestList=data;
      
   
   });
  }
  DeleteInterest(Id:number)
  {
    
    this.contactService.DeleteInterests(Id).subscribe((data) => {
  
      this.GetAllInterest();
      this.GetUsersInterests();
   
   });
  }
  DeleteContact(Id:number)
  {
    this.contactService.DeleteContact(Id).subscribe((data) => {
  
     
      this.loadData();
  });
}
  AddInterest()
  {
    
  if(this.selectedInterest!=null)
  {
    let interest = this.interestList.find((user: any) => user.name === this.selectedInterest);
    this.contactService.SaveInterests(interest.id).subscribe((data) => {
  
     if(!data.isSuccess)
     {
       
       this.GetAllInterest();
      this.GetUsersInterests();
     }
     else{
       
     }
      
   
   });
  }
  }
  SaveInterest()
  {

  }
  GetUsersInterests()
  {
    this.contactService.GetUsersInterests().subscribe((data) => {
  
      this. interst=data;
      
   
   });
  }
  loadDataUserDetails()
  {
    this.contactService.GetUserDetails().subscribe((data) => {
  
      this. userDetails=data;
       
   
   });
  }
  loadData()
  {
    this.contactService.LoadData().subscribe((data) => {
  
      this. users=data;
       
   
   }
   );
  }

}
