import { Component, OnInit, AfterViewInit, ElementRef, ViewChildren, ViewContainerRef } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormArray, FormBuilder, Validators, FormControl, FormControlName } from "@angular/forms";
import { Router } from "@angular/router";

import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';
import { Observable } from "rxjs/Observable";
import { Subscription } from 'rxjs/Subscription';

import { CustomValidators, CustomFormsModule } from 'ng2-validation';
import { GenericValidator } from "../../common/validation/generic-form-validator";

import { Empresa } from "../models/empresa";
import { EmpresaService } from "../../services/empresa.service";

import { conformToMask } from 'angular2-text-mask';
import { ToastrService } from "ngx-toastr";



@Component({
  selector: 'app-inscricao',
  templateUrl: './inscricao.component.html',
  styleUrls: ['./inscricao.component.css']
})
export class InscricaoComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  public cnpjMask: Array<string | RegExp>;
  public errors: any[] = [];
  inscricaoForm: FormGroup;
  empresa: Empresa;
  displayMessage: { [key: string]: string } = {};
  validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  constructor(private fb: FormBuilder,
    private empresaService: EmpresaService,
    private router: Router,
    private toastr: ToastrService,
    vcr: ViewContainerRef) {


    this.validationMessages = {
      razaoSocial: {
        required: 'O razão social é requerida',
        minLength: 'A razão social deve ter no mínimo 2 caracteres',
        maxLength: 'A razão social deve ter no máximo 150 caracteres',
      },
      cnpj: {
        required: 'Informe o CNPJ',
        rangeLength: 'CNPJ deve conter 14 caracteres',
      },
      telefone: {
        required: 'Informe o Telefone',
        rangeLength: 'Telefone deve conter 11 caracteres',
      },
      email: {
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      senha: {
        required: 'Informe a senha',
        minLength: 'A senha deve possuir no mínimo 6 caracteres'
      },
      senhaConfirmacao: {
        required: 'Informe a senha novamente',
        minLength: 'A confirmação da senha deve possuir no mínimo 6 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);

    this.cnpjMask = [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/];
  }

  ngOnInit() {

    let senha = new FormControl('', [Validators.required, Validators.minLength(6)]);
    let senhaConfirmacao = new FormControl('', [Validators.required, Validators.minLength(6), CustomValidators.equalTo(senha)]);

    this.inscricaoForm = this.fb.group({
      razaoSocial: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      cnpj: ['', [Validators.required, CustomValidators.rangeLength([18, 18])]],
      telefone: ['', [Validators.required, CustomValidators.rangeLength([14, 15])]],
      email: ['', [Validators.required, CustomValidators.email]],
      senha: senha,
      senhaConfirmacao: senhaConfirmacao
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements.
      map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.inscricaoForm);
    });
  }

  adicionarEmpresa() {
    if (this.inscricaoForm.dirty && this.inscricaoForm.valid) {
      let e = Object.assign({}, this.empresa, this.inscricaoForm.value);

      this.empresaService.registrarEmpresa(e)
        .subscribe(
        result => { this.onSaveComplete(result) },
        error => { this.onError(error) });
    }
  }

  onSaveComplete(response: any) {
    this.inscricaoForm.reset();
    this.errors = [];

    localStorage.setItem('eio.token', response.result.access_token);
    localStorage.setItem('eio.user', JSON.stringify(response.result.user));

    this.toastr.success('Registro realizado com Sucesso!', 'Bem vindo!!!');
    this.router.navigate(['/gerenciamento']);

  }

  onError(error: any) {
    this.toastr.error('Ocorreu um erro no processamento', 'Ops :(');
    this.errors = JSON.parse(error._body).errors;
  }

  telMask(userInput) {
    let numbers = userInput.match(/\d/g);
    let numberLength = 0;
    if (numbers) {
      numberLength = numbers.join("").length;
    }

    if (numberLength > 10) {
      return ['(', /[1-9]/, /[1-9]/, ')', ' ', /\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/];
    } else {
      return ['(', /[1-9]/, /[1-9]/, ')', ' ', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/];
    }
  }

}
