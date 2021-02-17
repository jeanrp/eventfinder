import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from "./base.service";
import { Cliente, Token } from "../models/cliente";
import { Observable } from "rxjs/Observable";
import { Observer } from "rxjs/Observer";
import { Facebook, FacebookLoginResponse } from '@ionic-native/facebook';

@Injectable()
export class ClienteService extends BaseService {

    userChange: Observable<any>;
    userChangeObserver: Observer<any>;

    constructor(private http: Http) {
        super();

        this.userChange = new Observable((observer: Observer<any>) => {
            this.userChangeObserver = observer;
        });
    }


    registrarCliente(cliente: Cliente): Observable<Cliente> {
        let response = this.http
            .post(this.UrlServiceV1 + "nova-conta-cliente", cliente, super.ObterJsonHeader())
            .map(super.extractData)
            .catch(super.serviceError);

        return response;
    }


    login(cliente: Cliente): Observable<Cliente> {
        let response = this.http
            .post(this.UrlServiceV1 + "conta", cliente, super.ObterJsonHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };


    loginFacebook(loginResponse: FacebookLoginResponse): Observable<Cliente> {

        let tokenVm = new Token();
        tokenVm.token = loginResponse.authResponse.accessToken;

        let response = this.http
            .post(this.UrlServiceV1 + "conta-facebook-cliente", tokenVm, super.ObterJsonHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };
}                                                                         