import { Component, ViewChild } from '@angular/core';
import { Nav, Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { HomePage } from '../pages/home/home';
import { LoginPage } from '../pages/login/login';
import { LocalizaEventoPage } from '../pages/localiza-evento/localiza-evento';


import { Cliente } from "../pages/models/cliente";
import { ClienteService } from "../pages/services/cliente.service";

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = LoginPage;
  public cliente: Cliente;



  pages: Array<{ icon: string, title: string, component: any }>;

  constructor(
    public platform: Platform,
    public statusBar: StatusBar,
    public splashScreen: SplashScreen,
    public clienteService: ClienteService) {
    this.initializeApp();

    // used for an example of ngFor and navigation
    this.pages = [ 
      { icon: 'pin', title: 'Encontrar evento', component: LocalizaEventoPage },
      { icon: 'calendar', title: 'Eventos', component: HomePage }
    ];

    this.clienteService.userChange.subscribe((data) => {
      this.cliente = this.clienteService.obterUsuario();
    });

  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  openPage(page) {
    // Reset the content nav to have just this page
    // we wouldn't want the back button to show in this scenario
    this.nav.setRoot(page.component);
  }

  sair() {
    localStorage.removeItem('eio.token');
    localStorage.removeItem('eio.user');

    setTimeout(() => {
      this.cliente = new Cliente();
      this.clienteService.userChangeObserver.next(this.cliente);
    }, 1000);

    this.nav.setRoot(LoginPage);
  }
}
