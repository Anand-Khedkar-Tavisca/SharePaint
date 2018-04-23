import { Injectable} from '@angular/core';
import { Http, Response, RequestOptions, Headers, Request, RequestMethod,URLSearchParams} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { environment } from '../../../environments/environment';

@Injectable()
export class RestService {
    _baseUrl: string = '';
    public file_name = '';
    self = this;
  	constructor(private http: Http) {
      this._baseUrl = environment.apiURI;

    }

    public get(urlPart: string, parms:URLSearchParams = null){
        let token = localStorage.getItem("key");
        let headers = new Headers({ContextType: "application/json; charset=utf-8",Accept: "application/json","Authorization": "Secret " + token });
        let options = new RequestOptions({ headers: headers});
        options.search = parms;
        
        let url = this._baseUrl + "/"+urlPart;

        return this.http.get(url,options).map(this.extractData);
        //.catch((error)=>this.handleServerError(error));   
    }

    public delete(urlPart: string, parms:URLSearchParams = null){
        let token = localStorage.getItem("key");
        let headers = new Headers({ContextType: "application/json; charset=utf-8",Accept: "application/json","Authorization": "Secret " + token});
        let options = new RequestOptions({ headers: headers});
        options.search = parms;
        
        let url = this._baseUrl + "/"+urlPart;

        return this.http.delete(url,options).map(this.extractData);
       // .catch((error)=>this.handleServerError(error));   
    }

    public post(urlPart: string, body:any, parms:URLSearchParams = null){
        let url_saveNewOrg = this._baseUrl+ "/" + urlPart ;
        let token = localStorage.getItem("key");
        let headers = new Headers({ContextType: "application/json; charset=utf-8",Accept: "application/json","Authorization": "Secret " + token });
        let options = new RequestOptions({ headers: headers});
        options.search = parms;
        return this.http.post(url_saveNewOrg,body,options).map(this.extractData);
       // .catch((error)=>this.handleServerError(error));   
    }

    public put(urlPart: string, body:any = null, parms:URLSearchParams = null){
        let url_saveNewOrg = this._baseUrl + "/"+ urlPart ;
        let token = localStorage.getItem("key");
        let headers = new Headers({ContextType: "application/json; charset=utf-8",Accept: "application/json","Authorization": "Secret " + token });
        let options = new RequestOptions({ headers: headers});
        options.search = parms;
        return this.http.put(url_saveNewOrg,body,options).map(this.extractData);
       // .catch((error)=>this.handleServerError(error));   
    }

    
    private handleServerError(error: Response) {
        let er = error.json().error;
        if(error.status === 401)
        {
        }
        if(error.status === 503 || error.status === 504){
            er = "Unable to connect. Please attempt your action again."
        }
    return Observable.throw(er|| 'Server error'); // Observable.throw() is undefined at runtime using Webpack
    }

    private extractData(response: Response) {
        let body;
	      if (response.status === 401) {
            // show message
    	  }
        return response.text() ? response.json() : {};
      }
}