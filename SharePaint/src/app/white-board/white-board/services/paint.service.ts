import { Injectable } from '@angular/core';
import { RestService } from '../../../app-common/services/rest.service';
import { Paint } from '../Models/stroke';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class PaintService {

  constructor(private restService:RestService) {  }

  public GetPaint():Observable<Paint>
  {
    return this.restService.get('api/paint')
  }

}
