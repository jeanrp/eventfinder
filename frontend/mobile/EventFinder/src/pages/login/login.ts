import { Component, OnInit, AfterViewInit, ElementRef, ViewChildren, ViewContainerRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from "@angular/forms";
import { NavController, ToastController, NavParams, LoadingController } from 'ionic-angular';
import { Cliente } from "../models/cliente";
import { GenericValidator } from "../common/validation/generic-form-validator";
import { CustomValidators } from 'ng2-validation';
import { Observable } from "rxjs/Observable";
import { Subscription } from 'rxjs/Subscription';
import { HomePage } from '../home/home'
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';
import { ClienteService } from '../services/cliente.service'
import { InscricaoPage } from "../inscricao/inscricao";
import { Facebook, FacebookLoginResponse } from '@ionic-native/facebook';
import { LocalizaEventoPage } from "../localiza-evento/localiza-evento";


@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage implements OnInit, AfterViewInit {


  public loginForm: FormGroup;
  cliente: Cliente;



  constructor(
    private toastCtrl: ToastController,
    private clienteService: ClienteService,
    private fb: FormBuilder,
    private navCtrl: NavController,
    private navParams: NavParams,
    private loadingCtrl: LoadingController,
    private facebook: Facebook) {


  }

  ionViewDidLoad() {

  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, CustomValidators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]]
    });
  }


  ionViewDidEnter() { }
  ngAfterViewInit(): void { }

  logar() {

    if (this.loginForm.dirty && this.loginForm.valid) {
      let e = Object.assign({}, this.cliente, this.loginForm.value);

      this.clienteService.login(e)
        .subscribe(
        result => { this.onSaveComplete(result) },
        error => { this.onError(error) });
    }
  }


  logarComFacebook(): void {
    this.facebook.login(['public_profile', 'user_friends', 'email'])
      .then((res: FacebookLoginResponse) => { 
        this.onSaveCompleteFacebookComplete(res);
      })
      .catch(e => {
        let toast = this.toastCtrl.create({
          message: 'Não foi possível efetuar o login com o facebook!',
          duration: 1500
        });
        toast.present();
      }); 
  }


  onSaveCompleteFacebookComplete(response: FacebookLoginResponse) {
    this.clienteService.loginFacebook(response)
      .subscribe(
      result => { this.onSaveComplete(result) },
      error => { this.onError(error) });
  }

  onSaveComplete(response: any) {
    this.loginForm.reset();

    localStorage.setItem('eio.token', response.result.access_token);
    localStorage.setItem('eio.user', JSON.stringify(response.result.user));
    this.clienteService.userChangeObserver.next(JSON.stringify(response.result.user));


    let loading = this.loadingCtrl.create({
      content: 'Autenticando'
    });

    loading.present();

    setTimeout(() => {
      loading.dismiss();
      this.navCtrl.setRoot(LocalizaEventoPage);
    }, 1000);

    let toast = this.toastCtrl.create({
      message: 'Login efetuado com sucesso!',
      duration: 1500
    });
    toast.present();
  }

  cadastrar() {
    this.navCtrl.setRoot(InscricaoPage);
  }

  

  onError(error: any) {
    let toast = this.toastCtrl.create({
      message: 'Ocorreu um erro no processamento!',
      duration: 1500
    });
    toast.present();

  }

}
