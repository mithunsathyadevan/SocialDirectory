import { Component, OnInit } from '@angular/core';
import { Subject,takeUntil } from 'rxjs';
import { CommonService } from '@commonservice/common.service';
import { DashboardService } from './dashboard.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  users: any;
  Success: boolean = false;
  private destroy$ = new Subject<void>();
  Failed: boolean = false;
  Message: string = '';
  constructor(private dashBoardService: DashboardService, private commonService: CommonService) { }
  ngOnInit(): void {
    this.loadData();
    this.commonService.getData().pipe(takeUntil(this.destroy$)).subscribe(data => {
      this.users = data;
    })
  }
  loadData() {
    this.dashBoardService.loadData().pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.users = data;
    }
    );
  }
  clickMethod(name: string, contactId: Number) {
    if (confirm("Are you sure you want to add " + name + " to your contact")) {
      this.dashBoardService.addContactList(contactId).pipe(takeUntil(this.destroy$)).subscribe((data) => {
        if (data.isSuccess) {
          this.Success = data.isSuccess;
          this.Failed = false;
          this.loadData();
        }
        else {
          this.Failed = true;
          this.Success = false;
          this.Message = data.message;
        }
        setInterval(() => {
          this.Failed = false;
          this.Success = false;
        }, 4000)
      }
      );
    }
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
