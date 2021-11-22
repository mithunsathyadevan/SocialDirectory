import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { CommonService } from '../services/common.service';
import { DashboardService } from './dashboard.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  users: any;
  Success: boolean = false;
  Failed: boolean = false;
  Message:string='';
  constructor(private dashBoardService: DashboardService,private commonService:CommonService) { }

  ngOnInit(): void {
    this.loadData();
    this.commonService.getData().subscribe(data => {
      
      this.users = data;
    })
  }
  loadData() {
    this.dashBoardService.LoadData().subscribe((data) => {

      this.users = data;


    }
    );
  }
  clickMethod(name: string, contactId: Number) {
    
    if (confirm("Are you sure you want to add " + name + " to your contact")) {

      this.dashBoardService.AddContactList(contactId).subscribe((data) => {
        if (data.isSuccess) {
          this.Success = data.isSuccess;
          this.Failed=false;
          this.loadData();

        }
        else 
        {
          this.Failed=true;
          this.Success=false;
          this.Message=data.message;
        }
        setInterval(() => {
          this.Failed=false;
          this.Success=false;
        },4000)


      }
      );
    }
  }
 

}
