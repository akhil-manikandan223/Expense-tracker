import { Component, signal } from '@angular/core';
import { CategoryList } from './masters/category/category-list/category-list';

@Component({
  imports: [CategoryList],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('Exp-tracker');
}
