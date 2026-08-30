import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-signupform',
  imports: [FormsModule],
  templateUrl: './signupform.html',
  styleUrl: './signupform.css',
})
export class Signupform {
  username: string;
  password: string;
  cpassword: string;
  error: string;

  constructor() {
    this.username = '';
    this.password = '';
    this.cpassword = '';
    this.error = '';
  }

  reset() {}

  handleSubmit(e: Event) {
    let pwregex = /^(?=.*[A-Za-z])(?=.*\d).{8,}$/;

    if (this.username.length < 4) {
      this.error = 'Username must be greater than 4 characters';
    } else if (!pwregex.test(this.password)) {
      this.error =
        'Password must be 8 characters long and should contain at least one alphabet and one number';
    } else if (this.password !== this.cpassword) {
      this.error = "The passwords don't match";
    } else {
      window.alert('Form validated successfully');
    }
  }
}
