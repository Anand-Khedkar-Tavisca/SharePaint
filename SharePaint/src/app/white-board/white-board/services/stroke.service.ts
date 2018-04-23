import { Injectable } from '@angular/core';
import { RestService } from '../../../app-common/services/rest.service';
import { Stroke } from '../Models/stroke';

@Injectable()
export class StrokeService {

  constructor(private restService:RestService) {  }

  public Create(stroke:Stroke)
  {
    return this.restService.post('api/Stroke',stroke);
  }

}
