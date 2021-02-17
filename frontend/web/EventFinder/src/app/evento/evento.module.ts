import { NgModule } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';



//components
import { GerenciamentoComponent } from './gerenciamento/gerenciamento.component';
import { MenuSuperiorComponent } from '../shared/menu-superior/menu-superior.component';
import { MenuLateralComponent } from '../shared/menu-lateral/menu-lateral.component';
import { AdicionarEventoComponent } from './adicionar-evento/adicionar-evento.component';
import { ListarEventosComponent } from './listar-eventos/listar-eventos.component';
import { EventoComponent } from './evento.component';


//Service
import { SeoService } from "../services/seo.service";
import { EmpresaService } from "../services/empresa.service";
import { AuthService } from "./services/auth.service";

// Modules bibliotecas
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TextMaskModule } from "angular2-text-mask/dist/angular2TextMask";
import { ModalModule } from 'ngx-bootstrap';
import { CurrencyMaskModule } from "ng2-currency-mask";
import { ImageUploadModule } from "angular2-image-upload";



//Routes
import { eventosRouterConfig } from "./evento.routes";

import { MyDatePickerModule } from 'mydatepicker';
import { EventoService } from "./services/evento.service";


@NgModule({
    declarations: [
        EventoComponent,
        GerenciamentoComponent,
        AdicionarEventoComponent,
        ListarEventosComponent,
        MenuSuperiorComponent,
        MenuLateralComponent],
    imports: [
        ImageUploadModule.forRoot(),
        CurrencyMaskModule,
        ModalModule.forRoot(),
        MyDatePickerModule,
        CollapseModule,
        TextMaskModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        CommonModule,
        RouterModule.forChild(eventosRouterConfig),
    ],
    providers: [
        Title,
        SeoService,
        EmpresaService,
        AuthService,
        EventoService
    ],
    exports: [RouterModule]
})

export class EventosModule {


}