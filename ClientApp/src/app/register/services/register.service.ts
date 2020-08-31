import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/auth/services/auth.service';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/models/user';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  constructor(private authService: AuthService) {
  }

 register(user: User): Observable<User> {
   return this.authService.registerUser(user);
 }
}
