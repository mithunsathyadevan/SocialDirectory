import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject,takeUntil } from 'rxjs';
import { AuthService } from './Auth/auth.service';
import { CommonService } from '@commonservice/common.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  login = false;
  dropdown: any;
  subDropDown: any;
  locationID = null;
  selectionList: number[] = [];
  title = 'SocialDirectoryUI';
  private destroy$ = new Subject<void>();
  IsAuthenticated: any = false;
  ngOnInit(): void {
    this.IsAuthenticated = this.authService.isAuthenticated();
    this.authService.getData().subscribe(data => {
      this.IsAuthenticated = data;
    })
  }
  constructor(private commmonService: CommonService, private router: Router, private authService: AuthService) { }
  onKeyDownEvent(event: any) {

    console.log(event.target.value);
    this.commmonService.getMasterInterest(event.target.value).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.dropdown = data;
    }
    );
  }
  apply() {
    this.selectionList = [];
    this.subDropDown.forEach((element: any) => {
      if (element.selected == true) {
        this.selectionList.push(element.id);
      }
    });
    this.loadList();
    this.subDropDown;
  }
  loadList() {
    let objet = { InterestIds: this.selectionList, locationId: this.locationID }
    this.commmonService.getMatches(objet).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      if (data) {
        this.commmonService.updateData(data);
      }
    }
    );
  }
  searchButtonClicked(seachForm: any) {
    let drop = this.dropdown.find((user: any) => user.name === seachForm.value.search);
    if (drop === undefined) {
      this.subDropDown = [];
    }
    else {
      this.router.navigate(['/matchfinder']);
      this.selectionList = [];
      this.selectionList.push(drop.id);
      if (drop.type == "Location")
        this.locationID = drop.id;
      else
        this.locationID = null;
      this.commmonService.getSubInterest(drop.id, drop.type).pipe(takeUntil(this.destroy$)).subscribe((data) => {
        this.subDropDown = data;
        this.apply();      }
      );

    }

  }
  logout() {
    this.authService.logout();
    this.router.navigate(['']);
    this.IsAuthenticated = this.authService.isAuthenticated();
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}

