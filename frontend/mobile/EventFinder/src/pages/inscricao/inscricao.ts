import { Component } from '@angular/core';
import { NavController, NavParams, ToastController, LoadingController } from 'ionic-angular';
import { ClienteService } from "../services/cliente.service";
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { CustomValidators } from "ng2-validation/dist";
import { Cliente } from "../models/cliente";
import { HomePage } from "../home/home";
import { Ionic2MaskDirective } from "ionic2-mask-directive";
import { LoginPage } from "../login/login";


@Component({
  selector: 'page-inscricao',
  templateUrl: 'inscricao.html',
})
export class InscricaoPage {

  public inscricaoForm: FormGroup;
  public cliente: Cliente;

  constructor(
    public fb: FormBuilder,
    public navCtrl: NavController,
    public navParams: NavParams,
    private clienteService: ClienteService,
    private toastCtrl: ToastController,
    public loadingCtrl: LoadingController) {
  }

  ionViewDidLoad() {
  }

  ngOnInit() {
    let senha = new FormControl('', [Validators.required, Validators.minLength(6)]);
    let senhaConfirmacao = new FormControl('', [Validators.required, Validators.minLength(6), CustomValidators.equalTo(senha)]);

    this.inscricaoForm = this.fb.group({
      nome: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      sexo: ['', [Validators.required, CustomValidators.rangeLength([1, 1])]],
      estadoCivil: ['', [Validators.required, CustomValidators.rangeLength([1, 1])]],
      telefone: ['', [Validators.required, CustomValidators.rangeLength([13, 16])]],
      atracaoPreferida: '',
      estiloPreferido: '',
      email: ['', [Validators.required, CustomValidators.email]],
      senha: senha,
      senhaConfirmacao: senhaConfirmacao
    });

  }

  cadastrar() {

    if (this.inscricaoForm.dirty && this.inscricaoForm.valid) {
      let e = Object.assign({}, this.cliente, this.inscricaoForm.value);

      this.clienteService.registrarCliente(e)
        .subscribe(
        result => { this.onSaveComplete(result) },
        error => { this.onError(error) });
    }
  }

  onSaveComplete(response: any) {
    this.inscricaoForm.reset();


    localStorage.setItem('eio.token', response.result.access_token);
    localStorage.setItem('eio.user', JSON.stringify(response.result.user));
    this.clienteService.userChangeObserver.next(JSON.stringify(response.result.user));

    let loading = this.loadingCtrl.create({
      content: 'Autenticando'
    });

    loading.present();

    setTimeout(() => {
      loading.dismiss();
      this.navCtrl.setRoot(HomePage);
    }, 1000);

    let toast = this.toastCtrl.create({
      message: 'Cadastro e Login efetuado com sucesso!',
      duration: 1500
    });
    toast.present();
  }

  logar()
  {
    this.navCtrl.setRoot(LoginPage);
  }


  onError(error: any) {
    let toast = this.toastCtrl.create({
      message: 'Ocorreu um erro no processamento!',
      duration: 1500
    });
    toast.present();
  }
}
