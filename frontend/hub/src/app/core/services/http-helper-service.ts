import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root' 
})
export class HttpHelperService{

    constructor(private http: HttpClient) {}

    getHttpHeaderOptions() {
       
        return {
            headers: new HttpHeaders({
                'Content-Type':  'application/json',
                Authorization: 'my-auth-token'
            })
        };
    }
}