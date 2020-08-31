import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services/api.service';
import { User } from 'src/app/shared/models/user';
import { LoginCredentials } from '../models/login-credentials';
import { AuthService } from 'src/app/auth/services/auth.service';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private authService: AuthService) {
   }

  login(loginCreds: LoginCredentials): Observable<User> {
    return this.authService.loginUser(loginCreds);
  }
}
