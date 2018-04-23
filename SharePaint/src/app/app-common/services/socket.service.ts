import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Observer, } from 'rxjs/Observer';
import 'rxjs/add/operator/share'
import 'rxjs/add/operator/map'
const SERVER_URL = 'ws://localhost:61140';

@Injectable()
export class SocketService {
    socket: WebSocket;
    constructor() {
        this.responce = new Observable((observer => {
            this.socket.onmessage = (message: MessageEvent) => {
              console.log('received message:',  message.data);
              let data = JSON.parse(message.data);
              observer.next(data);
            }
          }))
     }

    public responce: Observable<any>;

    public connect() {
        this.socket = new WebSocket(SERVER_URL);
    }
    public send(payload: any): void {
        let data =  JSON.stringify(payload);
        this.socket.send(data);
    }

    public closeConnection() {
    }


}

