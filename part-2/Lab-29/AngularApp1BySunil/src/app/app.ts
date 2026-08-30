import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Signupform } from './components/signupform/signupform';

@Component({
  selector: 'app-root',
  imports: [Signupform],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('AngularApp1BySunil');
}
