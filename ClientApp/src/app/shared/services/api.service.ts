import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService<T> {
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.baseUrl = `${baseUrl}api`;
  }

  getAll(url: string): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${url}`);
  }
  
  getById(url: string, id: number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${url}/${id}`);
  }

  getAllById(url: string, id: number): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${url}/${id}`);
  }

  getWithQueryParams(url: string, params: HttpParams): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${url}`, { params } );
  }

  post(url: string, item: any): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${url}`, item);
  }

  put(url: string, id: number, item: any): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${url}/${id}`, item);
  }

  delete(url: string, id: number): Observable<T> {
    return this.http.delete<T>(`${this.baseUrl}/${url}/${id}`);
  }
}
