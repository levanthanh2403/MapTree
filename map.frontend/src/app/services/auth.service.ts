import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CONST } from '../shared/CONST';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private ACCESS_TOKEN: string = "ACCESS_TOKEN";
    constructor(
        private router: Router,
        private http: HttpClient
    ) {
    }

    login(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/auth/login", req, CONST.httpOptions)
        return ret;
    }

    logout() {
        console.log("reomve token");
        localStorage.removeItem(this.ACCESS_TOKEN);
        this.router.navigate(['/auth/signin'], {
            queryParams: {},
        });
    }

    registerUser(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/auth/register-user", req, CONST.httpOptions)
        return ret;
    }

    updateUser(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/auth/update-user", req, CONST.httpOptions)
        return ret;
    }

    getListUser(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/auth/get-list-user' + params, {
            responseType: 'json'
        });
    }
    
    getDetailUser(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/auth/get-detail-user?userId=' + params, {
            responseType: 'json'
        });
    }

    public setAuthFromLocalStorage(token: any) {
        localStorage.setItem(this.ACCESS_TOKEN, token);
    }
    public getAuthFromLocalStorage(): any | undefined {
        try {
            const lsValue = localStorage.getItem(this.ACCESS_TOKEN);
            if (!lsValue) {
                return undefined;
            }
            const authData = JSON.parse(lsValue);
            return authData;
        } catch (error) {
            console.error(error);
            return undefined;
        }
    }
    checkLogin(): boolean {
        var token = localStorage.getItem(this.ACCESS_TOKEN)
        if (token == null || token == '')
            return false;
        return true;
    }
}