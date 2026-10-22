import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Category } from '../../models/master-models/category';

@Service()
export class CategoryService {
    private http = inject(HttpClient);
    private baseUrl = `${environment.apiBaseUrl}/Category`;

    getAll(){
        return this.http.get<Category[]>(this.baseUrl);
    }

    getById(id: string) {
        return this.http.get<Category>(`${this.baseUrl}/${id}`);
    }
}
