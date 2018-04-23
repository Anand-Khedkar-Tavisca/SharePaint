import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WhiteBoardComponent } from './white-board/white-board.component';

const routes: Routes = [

{
    path:':paintId',
    component:WhiteBoardComponent
},
{
  path:'',
  component:WhiteBoardComponent
},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WhiteBoardRoutingModule { }
