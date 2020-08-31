import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(this.handleError))
    }

    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            if (error.status === 401) {
                this.authService.logout();
                location.reload(true);
            }

            // The backend returned an unsuccessful response code.
            console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
        }

        const errorMessage = error.error.message || error.statusText;
        // Return an observable with a user-facing error message.
        return throwError(`An error occured: ${errorMessage}`);
    }
}