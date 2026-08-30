import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-calculator',
  imports: [FormsModule],
  templateUrl: './calculator.html',
  styleUrl: './calculator.css',
})
export class Calculator {
  a: number;
  b: number;
  op: string;
  result: number;

  constructor() {
    this.a = 0;
    this.b = 0;
    this.op = '+';
    this.result = 0;
  }

  handleSubmit() {
    this.result = eval(this.a + this.op + this.b);
  }
}
