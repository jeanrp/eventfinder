import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavController, NavParams, ToastController, Platform } from 'ionic-angular';
import { GoogleMaps, GoogleMap, HtmlInfoWindow, GoogleMapsEvent, GoogleMapOptions, CameraPosition, MarkerOptions, Marker } from '@ionic-native/google-maps';
import { Geolocation } from '@ionic-native/geolocation';
import { EventoService } from "../services/evento.service";
import { Evento } from '../models/evento'
import { NativeGeocoder, NativeGeocoderReverseResult, NativeGeocoderForwardResult } from '@ionic-native/native-geocoder';


@Component({
  selector: 'page-localiza-evento',
  templateUrl: 'localiza-evento.html',
})
export class LocalizaEventoPage {
  map: GoogleMap;
  latitudeUsuario: any;
  longitudeUsuario: any;

  constructor(public platform: Platform,
    private nativeGeocoder: NativeGeocoder,
    private eventoService: EventoService,
    private geolocation: Geolocation,
    private googleMaps: GoogleMaps,
    public navCtrl: NavController,
    private toastCtrl: ToastController,
    public navParams: NavParams) {
  }

  ionViewDidLoad() {
    this.initMap();
  }
  initMap() {
    this.platform.ready().then(() => {
      this.latitudeUsuario = -20.236115;
      this.longitudeUsuario = -40.276119;

      this.map = GoogleMaps.create('map', {
        'controls': {
          'compass': true,
          'myLocationButton': true,
          'indoorPicker': true,
          'zoom': true
        },
        'gestures': {
          'scroll': true,
          'tilt': true,
          'rotate': true,
          'zoom': true
        },
        'camera': {
          'target':
          {
            lat: this.latitudeUsuario,
            lng: this.longitudeUsuario
          },
          'tilt': 30,
          'zoom': 6,
          'bearing': 50
        }
      });
      this.map.on(GoogleMapsEvent.MAP_READY).subscribe(() => {
        this.marcarMapa(this.map);
      });
    });
  }

  localizacaoUsuario() {
    this.geolocation.getCurrentPosition().then((resp) => {
      this.latitudeUsuario = resp.coords.latitude;
      this.longitudeUsuario = resp.coords.longitude;
      this.map.setCameraTarget({
        lat: resp.coords.latitude,
        lng: resp.coords.longitude
      });
    }).catch((error) => {
      let toast = this.toastCtrl.create({
        message: 'Favor habilitar o GPS antes de buscar a sua localização!',
        duration: 1500
      });
      toast.present();
    });
  }


  marcarMapa(map: any) {
    this.eventoService.obterEventos()
      .subscribe(eventos => {
        eventos.forEach((evento: Evento) => {
          this.nativeGeocoder.forwardGeocode(evento.endereco.enderecoFormatado)
            .then((coordinates: NativeGeocoderForwardResult) => {
              map.addMarker({
                title: evento.nome,
                icon: 'blue',
                animation: 'DROP',
                position: {
                  lat: parseFloat(coordinates.latitude),
                  lng: parseFloat(coordinates.longitude)
                }
              }).then((marker: Marker) => {
                marker.addEventListener(GoogleMapsEvent.MARKER_CLICK).subscribe(() => { console.log('Marker clicked...'); });
              });
            });
        },
          error => {
            let toast = this.toastCtrl.create({
              message: 'Ocorreu um erro no processamento!',
              duration: 1500
            });
            toast.present();
          });
      });
  }
}
