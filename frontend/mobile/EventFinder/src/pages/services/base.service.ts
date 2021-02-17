import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/throw';
import { Observer } from "rxjs/Observer"; 
import 'rxjs/add/operator/map';
 
export abstract class BaseService {
    constructor()  { }
 
    public Token: string = "";

    protected UrlServiceV1: string = "http://10.0.2.2:59084/api/v1/";

    protected extractData(response: Response) {
        let body = response.json();
        return body.data || {};
    }
    protected ObterJsonHeader(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return options;
    }
    protected obterAuthHeader(): RequestOptions {
        this.Token = localStorage.getItem('eio.token');
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Authorization', `Bearer ${this.Token}`);
        let options = new RequestOptions({ headers: headers });
        return options;
    }
    public obterUsuario() {
        return JSON.parse(localStorage.getItem('eio.user'));
    }

    protected serviceError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        return Observable.throw(error);
    }
}