import { Component, TemplateRef, OnInit, ElementRef, ViewChildren, AfterViewInit } from '@angular/core';
import { IMyDpOptions, IMyInputFocusBlur } from 'mydatepicker';
import { FormGroup, FormControlName, FormBuilder, Validators } from "@angular/forms";
import { GenericValidator } from "../../common/validation/generic-form-validator";
import { DateUtils } from "../../common/data-type-utils/date-utils";
import { conformToMask } from 'angular2-text-mask';
import { EventoService } from "../services/evento.service";
import { Estado, Cidade, Evento, Endereco, Atracao, Ingresso, Categoria, Foto } from "../models/evento";
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Observable } from "rxjs/Observable";
import { Subscription } from 'rxjs/Subscription';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from "@angular/router";
import { CustomValidators, CustomFormsModule } from 'ng2-validation';
import { CurrencyUtils } from "../../common/data-type-utils/currency-utils";
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';
import { UploadMetadata, FileHolder } from "angular2-image-upload";

@Component({
  selector: 'app-adicionar-evento',
  templateUrl: './adicionar-evento.component.html',
  styleUrls: ['./adicionar-evento.component.css']
})
export class AdicionarEventoComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  modalRef: BsModalRef;
  modalRefIngresso: BsModalRef;
  public cepMask: Array<string | RegExp>;
  public loteMask: Array<string | RegExp>;
  public horaMask: Array<string | RegExp>;
  public eventoForm: FormGroup;
  public atracaoForm: FormGroup;
  public ingressoForm: FormGroup;
  public evento: Evento;
  public atracao: Atracao;
  public ingresso: Ingresso;
  public categorias: Categoria[];
  public estados: Estado[];
  public errors: any[] = [];
  public errorsAtracao: any[] = [];
  public errorsIngresso: any[] = [];
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  public modalVisible: boolean;
  public myDatePickerOptions = DateUtils.getMyDatePickerOptions();




  constructor(private fb: FormBuilder,
    private eventoService: EventoService,
    private router: Router,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private toastrService: ToastrService) {
    this.horaMask = [/\d/, /\d/, ':', /\d/, /\d/];
    this.cepMask = [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/];
    this.loteMask = [/\d/];

    this.eventoService.obterEstados()
      .subscribe(estados => this.estados = estados,
      error => this.errors);


    this.eventoService.obterCategorias()
      .subscribe(categorias => this.categorias = categorias,
      error => this.errors);

    this.modalVisible = false;

    this.validationMessages = {
      nome: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 2 caracteres',
        maxlength: 'O Nome precisa ter no máximo 100 caracteres'
      },
      descricao: {
        required: 'A descrição é requerida.'
      },
      subDescricao: {
        minlength: 'A descrição curta precisa ter no mínimo 2 caracteres caso seja  declarada',
        maxlength: 'A descrição curta precisa ter no máximo 100 caracteres caso seja declarada'
      },
      descPatrocinadores: {
        minlength: 'Os patrocinadores precisam ter no mínimo 2 caracteres caso sejam declarados',
        maxlength: 'Os patrocinadores precisam ter no máximo 50 caracteres caso sejam declarados'
      },
      dataInicio: {
        required: 'Informe a data de início'
      },
      dataFim: {
        required: 'Informe a data de encerramento'
      },
      horaInicio: {
        required: 'Informe o horário de início',
        rangeLength: 'A hora de ínicio deve conter 5 caracteres',
      },
      horaFim: {
        required: 'Informe o horário de encerramento',
        rangeLength: 'A hora de encerramento deve conter 5 caracteres',
      },
      logradouro: {
        required: 'Informe o logradouro do endereço',
        minlength: 'O logradouro precisa ter no mínimo 2 caracteres',
        maxlength: 'O logradouro precisa ter no máximo 150 caracteres'
      },
      numero: {
        required: 'Informe o numero do endereço',
        minlength: 'O numero precisa ter no mínimo 2 caracteres',
        maxlength: 'O numero precisa ter no máximo 10 caracteres'
      },
      bairro: {
        required: 'Informe o bairro do endereço',
        minlength: 'O bairro precisa ter no mínimo 2 caracteres',
        maxlength: 'O bairro precisa ter no máximo 150 caracteres'
      },
      complemento: {
        minlength: 'O complemento precisa ter no mínimo 2 caracteres caso seja declarado',
        maxlength: 'O complemento precisa ter no máximo 500 caracteres caso seja declarado'
      },
      cidade: {
        required: 'Informe a cidade do endereço',
        minlength: 'O cidade precisa ter no mínimo 2 caracteres',
        maxlength: 'O cidade precisa ter no máximo 100 caracteres'
      },
      categoriaId: {
        required: 'Informe a categoria do evento'
      },
      estadoId: {
        required: 'Informe o estado do endereço'
      }, cep: {
        required: 'Informe o cep do endereço',
        rangeLength: 'O cep deve conter 8 caracteres',
      }
    };


    this.genericValidator = new GenericValidator(this.validationMessages);

    this.evento = new Evento();
    this.evento.endereco = new Endereco();
    this.evento.endereco.cidade = new Cidade();
    this.evento.atracoes = [];
    this.evento.ingressos = [];
    this.evento.fotos = [];

  }



  ngOnInit() {
    this.eventoForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      descricao: ['', Validators.required],
      subDescricao: ['', [Validators.minLength(2), Validators.maxLength(100)]],
      descPatrocinadores: ['', [Validators.minLength(2), Validators.maxLength(50)]],
      dataInicio: ['', Validators.required],
      horaInicio: ['', [Validators.required, CustomValidators.rangeLength([5, 5])]],
      dataFim: ['', Validators.required],
      horaFim: ['', [Validators.required, CustomValidators.rangeLength([5, 5])]],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      bairro: ['', Validators.required],
      complemento: ['', [Validators.minLength(2), Validators.maxLength(500)]],
      cidade: ['', Validators.required],
      estadoId: ['', Validators.required],
      categoriaId: ['', Validators.required],
      cep: ['', [Validators.required, CustomValidators.rangeLength([9, 9])]],
    });

    this.atracaoForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(150)]],
      estilo: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]]
    });


    this.ingressoForm = this.fb.group({
      tipo: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      preco: ['', Validators.required],
      lote: ['', Validators.required]
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.eventoForm);
    });

  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);

  }

  hideModal(): void {
    this.modalRef.hide();
  }


  openModalIngresso(template: TemplateRef<any>) {
    this.modalRefIngresso = this.modalService.show(template);

  }

  hideModalIngresso(): void {
    this.modalRefIngresso.hide();
  }

  onUploadFinished(file: FileHolder) {
    let element = document.getElementsByClassName("img-ul")[0];
    element.setAttribute('style', 'height:300px !important;width: 97.5% !important;margin-bottom: 30px !important;');
    let elementImage = <HTMLElement>document.getElementsByClassName("img-ul-image")[0];
    var imagem = elementImage.attributes[3].nodeValue;
    elementImage.setAttribute('style', 'height:300px !important; width:100% !important;background-size: auto !important;margin-bottom: 30px !important;' + imagem + '');
    var foto = JSON.parse(JSON.stringify(file.serverResponse));
    foto = JSON.parse(foto._body);
    var fotoObj = new Foto();
    fotoObj.id = foto.data.id;
    fotoObj.imagem = foto.data.imagem;
    this.evento.fotos.push(fotoObj);
  }


  onInputFocusBlurDataInicio(event: IMyInputFocusBlur): void {
    if (event.reason == 1 && event.value == "") {
      var element= document.getElementById('dataInicio') ;
        var elementChild : HTMLElement = element.getElementsByClassName("icon-mydpcalendar")[0] as HTMLElement;
        elementChild.click();
      }
  }

  onInputFocusBlurDataFim(event: IMyInputFocusBlur): void {
    if (event.reason == 1 && event.value == "") {
        var element = document.getElementById('dataFim');
        var elementChild : HTMLElement = element.getElementsByClassName("icon-mydpcalendar")[0] as HTMLElement;
        elementChild.click();
      }
  }





  onRemoved(file: FileHolder) {
    let element = document.getElementsByClassName("img-ul")[0];
    element.setAttribute('style', 'height:86px !important;width: 97.5% !important;margin-bottom: 30px !important;');
    let elementImage = <HTMLElement>document.getElementsByClassName("img-ul-image")[0];
    elementImage.setAttribute('style', 'height:86px !important; width:100% !important;background-size: auto !important;margin-bottom: 30px !important;');

    var foto = JSON.parse(JSON.stringify(file.serverResponse));
    foto = JSON.parse(foto._body);
    this.eventoService.removerFoto(foto.data.id)
      .subscribe(
      result => { this.toastrService.success('Imagem removida com sucesso!', 'Sucesso'); },
      error => {
        this.onError(error);
      });

  }

  adicionarEvento() {
    if (this.eventoForm.dirty && this.eventoForm.valid) {
      let user = this.eventoService.obterUsuario();

      let e = Object.assign({}, this.evento, this.eventoForm.value);
      e.empresaId = user.id;


      e.dataHoraInicio = DateUtils.getMyDatePickerDate(e.dataInicio);
      e.dataHoraFim = DateUtils.getMyDatePickerDate(e.dataFim);
      var arrHoraInicio = e.horaInicio.split(':');
      var arrHoraFim = e.horaFim.split(':');
      e.dataHoraInicio = new Date(e.dataHoraInicio.getFullYear(), e.dataHoraInicio.getMonth(), e.dataHoraInicio.getDate(), arrHoraInicio[0], arrHoraInicio[1], 0);
      e.dataHoraFim = new Date(e.dataHoraFim.getFullYear(), e.dataHoraFim.getMonth(), e.dataHoraFim.getDate(), arrHoraFim[0], arrHoraFim[1], 0);
      e.endereco.logradouro = e.logradouro;
      e.endereco.numero = e.numero;
      e.endereco.complemento = e.complemento;
      e.endereco.bairro = e.bairro;
      e.endereco.cep = e.cep;
      e.endereco.cidade.nome = e.cidade;
      e.endereco.cidade.classificacao = 18;
      e.endereco.cidade.estadoId = e.estadoId;
      e.atracoes = this.evento.atracoes;
      e.ingressos = this.evento.ingressos;

      this.eventoService.registrarEvento(e)
        .subscribe(
        result => { this.onSaveComplete() },
        error => {
          this.onError(error);
        });
    }

  }

  onError(error) {
    this.toastrService.error('Ocorreu um erro no processamento', 'Erro! :(');
    this.errors = JSON.parse(error._body).errors;
  }

  onSaveComplete(): void {
    this.eventoForm.reset();
    this.errors = [];
    this.toastrService.success('Evento Registrado com Sucesso!', 'Sucesso!');
    this.router.navigate(['/gerenciamento/listar-eventos']);
  }

  adicionarAtracao() {
    if (this.atracaoForm.dirty && this.atracaoForm.valid) {
      let a = Object.assign({}, this.atracao, this.atracaoForm.value);
      this.evento.atracoes.push(a);
      this.atracaoForm.reset();
      this.hideModal();
      this.toastrService.success('Atração adicionada com sucesso', "Sucesso!");
    }

  }
  adicionarIngresso() {
    if (this.ingressoForm.dirty && this.ingressoForm.valid) {
      let i = Object.assign({}, this.ingresso, this.ingressoForm.value);
      this.evento.ingressos.push(i);
      this.ingressoForm.reset();
      this.hideModalIngresso();
      this.toastrService.success('Atração adicionada com sucesso', "Sucesso!");
    }

  }


  removerAtracao(nome) {
    this.evento.atracoes = this.evento.atracoes.filter(function (obj) {
      return obj.nome !== nome;
    });
    this.toastrService.success('Ingresso removido com sucesso!', 'Sucesso');
  }

  removerIngresso(tipo) {
    this.evento.ingressos = this.evento.ingressos.filter(function (obj) {
      return obj.tipo !== tipo;
    });
    this.toastrService.success('Ingresso removido com sucesso!', 'Sucesso');
  }
}
