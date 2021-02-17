import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { LocalizaEventoPage } from './localiza-evento';

@NgModule({
  declarations: [
    LocalizaEventoPage,
  ],
  imports: [
    IonicPageModule.forChild(LocalizaEventoPage),
  ],
})
export class LocalizaEventoPageModule {}
