import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { GoogleMaps } from '@ionic-native/google-maps';



import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
 import { LoginPage } from '../pages/login/login';
 import { DetalhesEventoPage } from '../pages/detalhes-evento/detalhes-evento';
 import { LocalizaEventoPage } from '../pages/localiza-evento/localiza-evento';

 

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';


import { ClienteService } from '../pages/services/cliente.service';
import { InscricaoPage } from "../pages/inscricao/inscricao";
import {Ionic2MaskDirective} from "ionic2-mask-directive";
import { EventoService } from "../pages/services/evento.service";

import { Facebook } from '@ionic-native/facebook';

import { Geolocation } from '@ionic-native/geolocation';
import { NativeGeocoder  } from '@ionic-native/native-geocoder';
 



@NgModule({
  declarations: [
    MyApp,
    HomePage, 
    LoginPage ,
    DetalhesEventoPage,
    InscricaoPage,
    Ionic2MaskDirective, 
    LocalizaEventoPage
  ],
  imports: [ 
    ReactiveFormsModule,
    HttpModule,
    FormsModule,
    BrowserModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage, 
    LoginPage,
    DetalhesEventoPage,
    InscricaoPage, 
    LocalizaEventoPage
  ],
  providers: [ 
    NativeGeocoder,
    Geolocation,
    GoogleMaps,
    Facebook,
    EventoService,
    ClienteService, 
    StatusBar,
    SplashScreen,
    { provide: ErrorHandler, useClass: IonicErrorHandler }
  ]
})
export class AppModule { }
