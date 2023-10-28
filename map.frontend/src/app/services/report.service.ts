import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { CONST } from "../shared/CONST";

@Injectable({
    providedIn: 'root'
})
export class ReportService {
    constructor(
        private http: HttpClient
    ) {
    }
    getParams(): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Report/get-params', {
            responseType: 'json'
        });
    }
    exportReportLocation(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Report/export-report-location' + params, {
            responseType: 'json'
        });
    }
}