import { Component } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { User } from '../shared/models/user';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  user: User;

  constructor(private authService: AuthService) {
    this.authService.currentUser.subscribe(x => this.user = x);    
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authService.logout();
  }
}
