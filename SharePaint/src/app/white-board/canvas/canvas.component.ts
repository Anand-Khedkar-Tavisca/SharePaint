import { Component, OnInit, AfterViewInit, ElementRef, Input, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/takeUntil';
import 'rxjs/add/operator/pairwise';
import 'rxjs/add/operator/switchMap';
import { Stroke, Point, Line, Paint } from '../white-board/Models/stroke';
import { SocketService } from '../../app-common/services/socket.service';
import { RestService } from '../../app-common/services/rest.service';
import { UserService } from '../white-board/services/user.service';
import { PaintService } from '../white-board/services/paint.service';
import { StrokeService } from '../white-board/services/stroke.service';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'sp-canvas',
  templateUrl: './canvas.component.html',
  styleUrls: ['./canvas.component.scss']
})
export class CanvasComponent implements AfterViewInit, OnInit {

  user: any;
  messages: any[] = [];
  messageContent: string;
  ioConnection: any;
  shareURL:string;
  constructor(private socketService: SocketService, private userService: UserService, private paintService: PaintService, private strokeService: StrokeService) {


  }

  res: Observable<any>;
  ngOnInit(): void {
    //this.drowFromStorage();
    this.socketService.connect();
    this.socketService.responce.subscribe(data => {
      this.onReviced(data);
    })
  }
  public sendMessage(message: any): void {
    if (!message) {
      return;
    }

    this.socketService.send(message);
    this.messageContent = null;
  }


  @ViewChild('canvas') public canvas: ElementRef;

  @Input() public width = 600;
  @Input() public height = 400;
  @Input() public paintId = '';


  private cx: CanvasRenderingContext2D;

  public ngAfterViewInit() {
    const canvasEl: HTMLCanvasElement = this.canvas.nativeElement;
    this.cx = canvasEl.getContext('2d');

    canvasEl.width = this.width;
    canvasEl.height = this.height;

    this.cx.lineWidth = 3;
    this.cx.lineCap = 'round';
    this.cx.strokeStyle = '#000';

    this.captureEvents(canvasEl);

  }
  paint: Paint;
  currentScroc: Stroke;

  private captureEvents(canvasEl: HTMLCanvasElement) {
    Observable
      .fromEvent(canvasEl, 'mousedown')
      .switchMap((e) => {
        return Observable
          .fromEvent(canvasEl, 'mousemove')
          .takeUntil(Observable.fromEvent(canvasEl, 'mouseup'))
          .pairwise()
      })
      .subscribe((res: [MouseEvent, MouseEvent]) => {
        const rect = canvasEl.getBoundingClientRect();
        let subStrock = new Line();
        subStrock.StartPoint = {
          X: res[0].clientX - rect.left,
          Y: res[0].clientY - rect.top
        };
        subStrock.EndPoint = {
          X: res[1].clientX - rect.left,
          Y: res[1].clientY - rect.top
        };
        if (this.currentScroc.Id) {
          subStrock.StrokeId = this.currentScroc.Id;
          this.sendMessage(subStrock);
        }
        else {
          this.currentScroc.LinesQueue.push(subStrock);
          
        }
        this.currentScroc.Lines.push(subStrock);
        
        this.drawOnCanvas(subStrock.StartPoint, subStrock.EndPoint);

      });

    Observable
      .fromEvent(canvasEl, 'mousedown')
      .subscribe((res: MouseEvent) => {
        this.currentScroc = new Stroke();
        this.currentScroc.StartPoint = new Point();
        this.currentScroc.StartPoint.X = res.clientX;
        this.currentScroc.StartPoint.Y = res.clientY;

        this.strokeService.Create(this.currentScroc).subscribe(data => {
          this.currentScroc.Id = data.Id;
          if(!this.paint.Strokes){
            this.paint.Strokes = [];
                    }
          this.paint.Strokes.push(this.currentScroc);
        });
      });

    Observable
      .fromEvent(canvasEl, 'mouseup')
      .subscribe((res: MouseEvent) => {
        this.currentScroc.EndPoint = new Point();
        this.currentScroc.EndPoint.X = res.clientX;
        this.currentScroc.EndPoint.Y = res.clientY;
        if (this.currentScroc.Lines && this.currentScroc.Lines.length > 0) {
          if (this.currentScroc.Id) {
            while (this.currentScroc.LinesQueue && this.currentScroc.LinesQueue.length > 0) {

              let line = this.currentScroc.LinesQueue.pop();
              line.StrokeId = this.currentScroc.Id;
              this.sendMessage(line);
            }
          }
        }

      });
  }

  onReviced(data: any) {
    if (data.user) {
      localStorage.setItem("key", data.user.Secret);
      //since new user
      if (this.paintId) {
        //get paint data

        this.userService.AddToPaint(this.paintId).subscribe(data => {
          this.GetPaint();
        },
          error => {
            //handle error
            this.GetPaint();

          })
      }
      else {
        this.GetPaint();

      }
    }
    else if(data.IsUndone){
      this.GetPaint();
    }
    else{
      this.drawOnCanvas(data.StartPoint, data.EndPoint);
    }
  }

  GetPaint() {
    this.paintService.GetPaint().subscribe(data => {
      this.paintId = data.Id;
      if(this.paintId){
      this.shareURL = environment.selfURI+"/board/"+this.paintId;
      }
      this.paint = data;
      this.loadPaint();
    })
  }

  private drawOnCanvas(prevPos: Point, currentPos: Point) {
    if (!this.cx) { return; }

    this.cx.beginPath();

    if (prevPos) {
      this.cx.moveTo(prevPos.X, prevPos.Y); // from
      this.cx.lineTo(currentPos.X, currentPos.Y);
      this.cx.stroke();


    }
  }
  public undo() {
    this.cx.restore();
   var stroke = this.paint.Strokes.pop();
   stroke.IsUndone = true;
   this.sendMessage(stroke);
    this.GetPaint();
  }


  loadPaint() {
    this.cx.clearRect(0,0,this.width,this.height);    
    if (this.paint.Strokes) {
      this.paint.Strokes.forEach(strock => {
        if (strock.Lines && strock.IsUndone ==false) {
          strock.Lines.forEach(subStrock => {
            this.drawOnCanvas(subStrock.StartPoint, subStrock.EndPoint);
          });
        }
      });
    }

  }
}
