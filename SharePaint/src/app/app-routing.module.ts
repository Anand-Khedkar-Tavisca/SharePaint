import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [

  { 
    path: 'board', loadChildren: 'app/white-board/white-board.module#WhiteBoardModule' 
  
  },
  {
    path: '', loadChildren: 'app/white-board/white-board.module#WhiteBoardModule'     
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
