import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { CONST } from "../shared/CONST";

@Injectable({
    providedIn: 'root'
})

export class LocationService {
    constructor(
        private router: Router,
        private http: HttpClient
    ) {
    }
    getListLocation(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Location/get-list-location' + params, {
            responseType: 'json'
        });
    }
    getListLocationHist(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Location/get-list-location-hist' + params, {
            responseType: 'json'
        });
    }
    
    getDetailLocation(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Location/get-detail-location?locationid=' + params, {
            responseType: 'json'
        });
    }
    
    getDetailLocationHist(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Location/get-detail-location-hist?locationid=' + params, {
            responseType: 'json'
        });
    }

    createLocation(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/Location/create-location", req, CONST.httpOptions)
        return ret;
    }

    updateLocation(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/Location/update-location", req, CONST.httpOptions)
        return ret;
    }
}