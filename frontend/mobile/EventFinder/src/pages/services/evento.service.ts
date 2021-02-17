import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';


import { Evento, Endereco, Estado, Categoria, Foto } from "../models/evento";
import { BaseService } from "./base.service";

@Injectable()
export class EventoService extends BaseService {

    constructor(private http: Http) { super(); }

    public obterUsuario() {
        return JSON.parse(localStorage.getItem('eio.user'));
    }

    obterEventos(): Observable<Evento[]> {
        return this.http.get(this.UrlServiceV1 + "eventos")
            .map((res: Response) => <Evento[]>res.json())
            .catch(super.serviceError);
    } 
 
}


