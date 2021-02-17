import { Component, OnInit } from '@angular/core';
import { EventoService } from "../services/evento.service";
import { Evento } from "../models/evento";

@Component({
  selector: 'app-listar-eventos',
  templateUrl: './listar-eventos.component.html',
  styleUrls: ['./listar-eventos.component.css']
})
export class ListarEventosComponent implements OnInit {

public eventos: Evento[];
public errorMessage: string = "";

  constructor(public eventoService: EventoService) { }

  ngOnInit() {
        this.eventoService.obterMeusEventos()
      .subscribe(eventos => this.eventos = eventos,
      error => this.errorMessage);
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
}
