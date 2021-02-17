import { Component, OnInit, AfterViewInit, ElementRef, ViewChildren, ViewContainerRef } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators, FormArray, FormControl, FormControlName } from "@angular/forms";

import { Router } from "@angular/router";

import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';
import { Observable } from "rxjs/Observable";
import { Subscription } from 'rxjs/Subscription';

import { CustomValidators, CustomFormsModule } from 'ng2-validation';
import { GenericValidator } from "../../common/validation/generic-form-validator";
import { ToastrService } from 'ngx-toastr';

import { Empresa } from "../models/empresa";
import { EmpresaService } from "../../services/empresa.service";

import { conformToMask } from 'angular2-text-mask';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';

import { FacebookService, InitParams, LoginResponse } from 'ngx-facebook';



@Component({
  selector: 'app-autenticacao',
  templateUrl: './autenticacao.component.html',
  styleUrls: ['./autenticacao.component.css']
})
export class AutenticacaoComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  public errors: any[] = [];
  loginForm: FormGroup;
  empresa: Empresa;
  displayMessage: { [key: string]: string } = {};
  validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  constructor(private fb: FormBuilder,
    private empresaService: EmpresaService,
    private router: Router,
    private toastr: ToastrService,
    private facebook: FacebookService) {
    let initParams: InitParams = {
      appId: '146726135960966',
      xfbml: true,
      version: 'v2.8'
    };

    facebook.init(initParams);

    this.validationMessages = {
      email: {
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      senha: {
        required: 'Informe a senha',
        minLength: 'A senha deve possuir no mínimo 6 caracteres'
      }
    };
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, CustomValidators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  ngAfterViewInit(): void {


  }

  logar() {
    if (this.loginForm.dirty && this.loginForm.valid) {
      let e = Object.assign({}, this.empresa, this.loginForm.value);

      this.empresaService.login(e)
        .subscribe(
        result => { this.onSaveComplete(result) },
        error => { this.onError(error) });
    }

  }

  logarComFacebook(): void {

    this.facebook.login()
      .then((response: LoginResponse) => { 
        this.onSaveCompleteFacebookComplete(response);
      })
      .catch((error: any) => {
        this.toastr.error('Não foi possível efetuar o login com o facebook', 'Ops :(');
      });
  }

  onSaveComplete(response: any) {
    this.loginForm.reset();
    this.errors = [];

    localStorage.setItem('eio.token', response.result.access_token);
    localStorage.setItem('eio.user', JSON.stringify(response.result.user));
    this.toastr.success('Login efetuado com Sucesso!', 'Bem vindo!!!');
    this.router.navigate(['/gerenciamento']);
  }


  onSaveCompleteFacebookComplete(response: LoginResponse) {
    console.log(response);
    this.empresaService.loginFacebook(response)
      .subscribe(
      result => { this.onSaveComplete(result) },
      error => { this.onError(error) });
  }

  onError(error: any) {
    this.toastr.error('Ocorreu um erro no processamento', 'Ops :(');
    this.errors = JSON.parse(error._body).errors;
  }

}
