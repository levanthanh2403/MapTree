import { HTTP_INTERCEPTORS, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';

import { BehaviorSubject, Observable, Subscription, throwError } from 'rxjs';
import { catchError, filter, finalize, switchMap, take, timeout } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthService } from '../services/auth.service';
import { NotificationService } from '../services/notification.service';
import { SpinnerOverlayService } from '../services/spinneroverlay.service';
const TOKEN_HEADER_KEY = 'Authorization';       // for Spring Boot back-end

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  private ACCESS_TOKEN: string = "ACCESS_TOKEN";
  private totalRequests = 0;

  constructor(private spinnerOverlayService: SpinnerOverlayService, private authService: AuthService, private notificationService: NotificationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<Object>> {
    // console.log('handle-request-http: ', req);
    let authReq = req;
    const token = localStorage.getItem(this.ACCESS_TOKEN);
    if (token != null) {
      authReq = this.addTokenHeader(req, token);
    }
    this.totalRequests++;
    this.spinnerOverlayService.setLoading(true);
    console.log("authReq: ", authReq);
    return next.handle(authReq)
      .pipe(catchError(error => {
        try {
          console.log('error: ', error);
          this.spinnerOverlayService.setLoading(false);
          if (error.status === 0)
            return throwError(() => 'Unable to Connect to the Server!');
          else if (error instanceof HttpErrorResponse && !authReq.url.includes('auth/login') && error.status === 401) {
            this.authService.logout();
            return throwError(() => 'Hết phiên làm việc, vui lòng đăng nhập lại!');
          }
        } catch {
          this.spinnerOverlayService.setLoading(false);
          this.authService.logout();
        }
        return throwError(() => error);
      }))
      .pipe(finalize(() => {
        this.totalRequests--;
        if (this.totalRequests == 0) {
          console.log('finished call api!');
          this.spinnerOverlayService.setLoading(false);
        }
      }))
      .pipe(timeout({
        each: 180000,
        with: () => throwError(() => "Request timed out!")
      }));
  }

  private addTokenHeader(request: HttpRequest<any>, token: string) {
    /* for Spring Boot back-end */
    // return request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token) });

    /* for Node.js Express back-end */
    return request.clone({ headers: request.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token) });
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];