import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocketService } from './services/socket.service';
import { RestService } from './services/rest.service';
import { HttpModule } from '@angular/http';
import { 
  MatCardModule,
  MatIconModule,
  MatButtonModule,
  MatNativeDateModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,

  MatToolbarModule,
  MatTooltipModule,} from '@angular/material';
@NgModule({
  imports: [
    CommonModule,
    HttpModule,
    MatCardModule,
  MatIconModule,
  MatButtonModule,
  MatNativeDateModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  ],
  providers:[
    SocketService,
    RestService
  ],
  declarations: [],
  exports:[
    CommonModule,
    HttpModule,
    MatCardModule,
  MatIconModule,
  MatButtonModule,
  MatNativeDateModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  ]
})
export class AppCommonModule { }
