import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { TextMaskModule } from 'angular2-text-mask';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { rootRouterConfig } from './app.routes';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

//Modules
import { AlertModule } from 'ngx-bootstrap';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FacebookModule } from 'ngx-facebook';


//Components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AutenticacaoComponent } from './usuario/autenticacao/autenticacao.component';
import { InscricaoComponent } from './usuario/inscricao/inscricao.component';


//Services
import { SeoService } from "./services/seo.service";
import { EmpresaService } from "./services/empresa.service";


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AutenticacaoComponent,
    InscricaoComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    TextMaskModule,
    ReactiveFormsModule,
    HttpModule,
    CollapseModule.forRoot(),
    CarouselModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule.forRoot(rootRouterConfig, { useHash: false }),
    BrowserModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      preventDuplicates: true
    }),
    AlertModule.forRoot(),
    BsDropdownModule.forRoot(),
    FacebookModule.forRoot()
  ],
  providers: [
    Title,
    SeoService,
    EmpresaService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
