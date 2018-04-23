import { Injectable } from '@angular/core';
import { RestService } from '../../../app-common/services/rest.service';

@Injectable()
export class UserService {

  constructor(private restService:RestService) {  }

  public AddToPaint(paintId:AAGUID)
  {
    return this.restService.put('api/user/paint/'+paintId);
  }

}
