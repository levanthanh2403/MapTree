import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { CONST } from "../shared/CONST";

@Injectable({
    providedIn: 'root'
})
export class ProjectService {
    constructor(
        private router: Router,
        private http: HttpClient
    ) {
    }
    getListProjects(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Project/get-list-projects' + params, {
            responseType: 'json'
        });
    }
    
    getDetailProject(params: string): Observable<any> {
        return this.http.get(CONST.API_URL + 'api/Project/get-detail-project?projectId=' + params, {
            responseType: 'json'
        });
    }

    createProject(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/project/create-project", req, CONST.httpOptions)
        return ret;
    }

    updateProject(req: any): Observable<any> {
        let ret = this.http.post(CONST.API_URL + "api/project/update-project", req, CONST.httpOptions)
        return ret;
    }
}