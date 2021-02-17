import { Routes } from "@angular/router";

import { EventoComponent } from "./evento.component";

import { GerenciamentoComponent } from "./gerenciamento/gerenciamento.component";
import { AuthService } from "./services/auth.service";
import { ListarEventosComponent } from "./listar-eventos/listar-eventos.component";
import { AdicionarEventoComponent } from "./adicionar-evento/adicionar-evento.component";

export const eventosRouterConfig: Routes =
    [
        {
            path: '', component: EventoComponent,
            children:[
                {path:'', canActivate:[AuthService], component: GerenciamentoComponent},
                {path:'listar-eventos', canActivate:[AuthService], component: ListarEventosComponent},
                {path:'adicionar-evento', canActivate:[AuthService], component: AdicionarEventoComponent}
            ]
        }
    ];