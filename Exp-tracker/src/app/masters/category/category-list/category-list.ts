import { formatDate } from '@angular/common';
import { Component, inject, LOCALE_ID, OnInit, signal } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import {
  ClientSideRowModelModule,
  ColDef,
  DateFilterModule,
  ModuleRegistry,
  PaginationModule,
  TextFilterModule,
  themeQuartz,
  ValidationModule,
} from 'ag-grid-community';
import { CategoryService } from '../../../services/master-services/category';
import { Category } from '../../../models/master-models/category';

ModuleRegistry.registerModules([
  ClientSideRowModelModule,
  TextFilterModule,
  DateFilterModule,
  PaginationModule,
  ...(ngDevMode ? [ValidationModule] : []),
]);

@Component({
  imports: [AgGridAngular],
  selector: 'app-category-list',
  styleUrl: './category-list.scss',
  templateUrl: './category-list.html',
})
export class CategoryList implements OnInit {
  private readonly categoryService = inject(CategoryService);
  private readonly locale = inject(LOCALE_ID);

  categories = signal<Category[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);

  protected readonly theme = themeQuartz;

  protected readonly columnDefs: ColDef<Category>[] = [
    { field: 'name', headerName: 'Name', filter: 'agTextColumnFilter', flex: 1, minWidth: 150 },
    { field: 'description', headerName: 'Description', filter: 'agTextColumnFilter', flex: 2, minWidth: 200 },
    {
      field: 'isActive',
      headerName: 'Status',
      width: 120,
      valueFormatter: p => (p.value ? 'Active' : 'Inactive'),
    },
    {
      field: 'createdAt',
      headerName: 'Created',
      width: 180,
      filter: 'agDateColumnFilter',
      valueFormatter: p => (p.value ? formatDate(p.value, 'mediumDate', this.locale) : ''),
    },
  ];

  protected readonly defaultColDef: ColDef<Category> = {
    sortable: true,
    resizable: true,
  };

  ngOnInit() {
    this.getAllCategories();
  }

  getAllCategories() {
    this.categoryService.getAll().subscribe({
      next: data => { this.categories.set(data); this.loading.set(false); },
      error: err => { this.error.set(err.message); this.loading.set(false); },
    })
  }
}
