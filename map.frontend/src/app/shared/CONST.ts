import { HttpHeaders } from "@angular/common/http";

export class CONST {
    static httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    static API_URL: string = 'https://localhost:5002/';
}