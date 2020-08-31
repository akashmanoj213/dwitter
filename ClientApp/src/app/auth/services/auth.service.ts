import { Injectable } from '@angular/core';
import { ApiService } from 'src/app/shared/services/api.service';
import { User } from 'src/app/shared/models/user';
import { LoginCredentials } from 'src/app/login/models/login-credentials';
import { Observable, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private router: Router, private api: ApiService<User>) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  loginUser(loginCreds: LoginCredentials) {
    return this.api.post(`users/authenticate`, loginCreds)
      .pipe(map(user => {
        this.storeUserToken(user);
        return user;
      }));
  }

  // store user details and jwt token in local storage to keep user logged in between page refreshes
  storeUserToken(user: User) {
    localStorage.setItem('currentUser', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  logout() {
    this.deleteUserToken();
    this.router.navigate(['/login']);
  }

  // remove user from local storage and set current user to null
  deleteUserToken() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  registerUser(user: User) {
    return this.api.post(`users/register`, user);
  }
}
