import { Component, OnInit } from '@angular/core';
import { Subject,takeUntil } from 'rxjs';
import { ContactlistService } from './contactlist.service';

@Component({
  selector: 'app-contactlist',
  templateUrl: './contactlist.component.html',
  styleUrls: ['./contactlist.component.css']
})
export class ContactlistComponent implements OnInit {
  users: any;
  selectedInterest = null;
  userDetails: any;
  private destroy$ = new Subject<void>();
  interst: any;
  popoverMessage = "Are you sure you want to delete this item?";
  popoverTitle = "Confirmation"
  interestList: any;
  constructor(private contactService: ContactlistService) { }

  ngOnInit(): void {
    this.loadData();
    this.loadDataUserDetails();
    this.GetUsersInterests();
    this.GetAllInterest();
  }
  GetAllInterest() {
    this.contactService.getAllInterests().pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.interestList = data;
    });
  }
  DeleteInterest(Id: number) {
    this.contactService.deleteInterests(Id).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.GetAllInterest();
      this.GetUsersInterests();
    });
  }
  DeleteContact(Id: number) {
    this.contactService.DeleteContact(Id).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.loadData();
    });
  }
  AddInterest() {
    if (this.selectedInterest != null) {
      let interest = this.interestList.find((user: any) => user.name === this.selectedInterest);
      this.contactService.saveInterests(interest.id).pipe(takeUntil(this.destroy$)).subscribe((data) => {
        if (!data.isSuccess) {
          this.GetAllInterest();
          this.GetUsersInterests();
        }
        });
    }
  }
  GetUsersInterests() {
    this.contactService.getUsersInterests().pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.interst = data;
    });
  }
  loadDataUserDetails() {
    this.contactService.getUserDetails().pipe(takeUntil(this.destroy$)
  ).subscribe((data) => {
      this.userDetails = data;
    });
  }
  loadData() {
    this.contactService.loadData().pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.users = data;
    }
    );
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
