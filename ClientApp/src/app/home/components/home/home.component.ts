import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUserId: number;

  constructor(private authService: AuthService) { 
    this.currentUserId = this.authService.currentUserValue.id;
  }

  ngOnInit() {
  }

}
