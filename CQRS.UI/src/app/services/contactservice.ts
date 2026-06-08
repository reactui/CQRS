import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Contact } from '../models/Contact';


@Injectable()
export class ContactService {

    private headers: HttpHeaders;
    private accessPointUrl: string = 'http://localhost:5000/api/contacts';

    constructor(private http: HttpClient) {
        this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
    }

    get() {
        return this.http.get<Contact[]>(this.accessPointUrl, {headers: this.headers});       
    }

    update(contact: Contact) {        
        return this.http.put(this.accessPointUrl + '/' + contact.id, contact, {headers: this.headers})        
    }

    add(contact: Contact) {
        return this.http.post(this.accessPointUrl, contact, {headers: this.headers});
    }
    
    delete(contact: Contact) {
        return this.http.delete(this.accessPointUrl + '/' + contact.id, {headers: this.headers});
    }    
}