import { Component } from '@angular/core';
import { AuthService } from './auth/services/auth.service';
import { User } from './shared/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  user: User;

  constructor(private authService: AuthService) {
    this.authService.currentUser.subscribe(x => this.user = x);
  }
}
