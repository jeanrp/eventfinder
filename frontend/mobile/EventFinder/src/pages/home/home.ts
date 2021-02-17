import { Component } from '@angular/core';
import { NavController, NavParams, ModalController } from 'ionic-angular';
import { DetalhesEventoPage } from '../detalhes-evento/detalhes-evento';
import { Cliente } from "../models/cliente";
import { ClienteService } from "../services/cliente.service";
import { EventoService } from "../services/evento.service";
import { Evento } from "../models/evento";


@Component({
  selector: 'page-home',
  templateUrl: 'home.html',
})
export class HomePage {

  public cliente: Cliente;
  public eventos: Evento[];
    public errors: any[] = [];


  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public modalCtrl: ModalController,
    public clienteService: ClienteService,
    public eventoService: EventoService) {


    this.eventoService.obterEventos()
      .subscribe(eventos => this.eventos = eventos,
      error => this.errors);

  }

    formatarData(mes: string) : string
  {
      if (mes == '01') return 'JAN';
      if (mes == '02') return 'FEV';
      if (mes == '03') return 'MAR';
      if (mes == '04') return 'ABR';
      if (mes == '05') return 'MAI';
      if (mes == '06') return 'JUN';
      if (mes == '07') return 'JUL';
      if (mes == '08') return 'AGO';
      if (mes == '09') return 'SET';
      if (mes == '10') return 'OUT';
      if (mes == '11') return 'NOV';
      if (mes == '12') return 'DEZ';


    return mes;
  }



  ionViewDidLoad() { }

  mostrarDetalheEvento(evento:Evento) { 
    let modal = this.modalCtrl.create(DetalhesEventoPage, {evento : evento});
    modal.present();
  }
}
