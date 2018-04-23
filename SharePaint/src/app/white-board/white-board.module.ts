import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { WhiteBoardRoutingModule } from './white-board-routing.module';
import { WhiteBoardComponent } from './white-board/white-board.component';
import { CanvasComponent } from './canvas/canvas.component';
import { AppCommonModule } from '../app-common/app-common.module';
import { UserService } from './white-board/services/user.service';
import { PaintService } from './white-board/services/paint.service';
import { StrokeService } from './white-board/services/stroke.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    WhiteBoardRoutingModule,
    AppCommonModule
  ],
  declarations: [[WhiteBoardComponent,CanvasComponent]],
  providers:[
    UserService,PaintService,StrokeService
  ]
})
export class WhiteBoardModule { }
