import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from "./base.service";
import { Empresa, Token } from "../usuario/models/empresa";
import { Observable } from "rxjs/Observable";
import { LoginResponse } from "ngx-facebook/dist/esm"; 

@Injectable()
export class EmpresaService extends BaseService {

    /**
     *
     */
    constructor(private http: Http) {
        super();
    }


    registrarEmpresa(empresa: Empresa): Observable<Empresa> {
        let response = this.http
            .post(this.UrlServiceV1 + "nova-conta", empresa, super.ObterJsonHeader())
            .map(super.extractData)
            .catch(super.serviceError);

        return response;
    }


    login(empresa: Empresa): Observable<Empresa> {
        let response = this.http
            .post(this.UrlServiceV1 + "conta", empresa, super.ObterJsonHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };


    loginFacebook(loginResponse: LoginResponse): Observable<Empresa> {               
        let tokenVm = new Token();
        tokenVm.token = loginResponse.authResponse.accessToken;
        let response = this.http
            .post(this.UrlServiceV1 + "conta-facebook", tokenVm, super.ObterJsonHeader())
            .map(super.extractData)
            .catch((super.serviceError));
        return response;
    };


}                                                                         
