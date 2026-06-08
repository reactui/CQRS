import { Component, OnInit, Input } from '@angular/core';
import { Contact } from './models/contact';
import { ContactService } from './services/contactservice';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
    styles: [`
        :host ::ng-deep .p-dialog .product-image {
            width: 150px;
            margin: 0 auto 2rem auto;
            display: block;
        }
    `],
    styleUrls: ['./app.component.scss']
})
export class AppComponent { 
    contact: Contact;
    contacts: Contact[];
    contactDialog: boolean;
    selectedContacts: Contact[];
    submitted: boolean;    

    constructor(        
        private contactService: ContactService, 
        private messageService: MessageService, 
        private confirmationService: ConfirmationService) { }

    ngOnInit() {                
        this.contactService.get().subscribe(contacts => this.contacts = contacts );
    }

    openNew() {        
        this.contact = {};
        this.contactDialog = true;
        this.submitted = false;
    }

    deleteSelectedContacts() {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete the selected contacts?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.contacts = this.contacts.filter(val => !this.selectedContacts.includes(val));
                this.selectedContacts = null;
                this.messageService.add({severity:'success', summary: 'Successful', detail: 'Contacts Deleted', life: 3000});
            }
        });
    }

    editContact(contact: Contact) {        
        this.contact = {...contact};                
        this.contactDialog = true;
    }

    deleteContact(contact: Contact) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + contact.firstname + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                
                this.contactService.delete(contact).subscribe(
                    next  => {
                        this.messageService.add({severity:'success', summary: 'Successful', detail: 'Contact Deleted', life: 3000});
                        
                        this.contacts = this.contacts.filter(val => val.id !== contact.id);
                        this.contact = {};
                    },
                    errorResponse => { 
                        this.messageService.add({severity:'error', summary: 'Error deleting contact', detail: errorResponse.error, life: 3000});                
                    }
                );
            }    
        });
    }

    hideDialog() {   
        this.contactDialog = false;        
        this.submitted = false;
    }
    
    saveContact() {        
        this.submitted = true;

        this.contact.id ? this.updateContact() : this.addContact();
    }

    private updateContact() {
        console.log(this.contact);
        this.contactService.update(this.contact).subscribe(
            data  => {
                this.messageService.add({severity:'success', summary: 'Successful', detail: 'Contact Updated', life: 3000});
                this.contacts[this.findIndexById(this.contact.id)] = this.contact;                
                
            },
            errorResponse => { 
                this.messageService.add({severity:'error', summary: 'Error', detail: errorResponse.error, life: 3000});                
            }
        );
    }

    private addContact() {
        
        this.contactService.add(this.contact).subscribe(
            data  => {
                this.messageService.add({severity:'success', summary: 'Successful', detail: 'Contact Added', life: 3000});                
                console.log(data.toString());             
                this.contact.id = data.toString();              
                this.contacts.push(this.contact);
                this.contactService.get().subscribe(contacts => this.contacts = contacts );
            },
            errorResponse => { 
                this.messageService.add({severity:'error', summary: 'Error', detail: errorResponse.error, life: 3000});                
            }
        );
    }

    findIndexById(id: string): number {
        let index = -1;
        for (let i = 0; i < this.contacts.length; i++) {
            if (this.contacts[i].id === id) {
                index = i;
                break;
            }
        }

        return index;
    }
}
