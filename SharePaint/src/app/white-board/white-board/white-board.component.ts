import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PaintService } from './services/paint.service';

@Component({
  selector: 'sp-white-board',
  templateUrl: './white-board.component.html',
  styleUrls: ['./white-board.component.scss']
})
export class WhiteBoardComponent implements OnInit {

  constructor(private routee:Router,private route: ActivatedRoute, private paintService:PaintService) { }
  paintId:string;
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.paintId= params['paintId'];
      if(this.paintId)
      {

      }
      else
      {
        //create new paint
       /* this.paintService.GetPaint().subscribe(data=>{
          this.paintId = data.Id;
        },
        error=>{

        }
        
        
      )*/
      }
    });
  }

}
