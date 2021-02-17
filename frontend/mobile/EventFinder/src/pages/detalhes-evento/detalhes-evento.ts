import { Component } from '@angular/core';
import { NavController, NavParams, ViewController } from 'ionic-angular';
import { Evento } from "../models/evento";

@Component({
  selector: 'page-detalhes-evento',
  templateUrl: 'detalhes-evento.html',
})
export class DetalhesEventoPage {
  public evento: Evento;
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public viewCtrl: ViewController) {
       this.evento = this.navParams.get('evento');
  }

  ionViewDidLoad() {
  }

  fechar()
  {
      this.viewCtrl.dismiss();
  }
}
