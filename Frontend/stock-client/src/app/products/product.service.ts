import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = `${environment.apiUrl}/products`;

  constructor(private http: HttpClient) { }

  // Crear producto individual
  addProduct(product: Partial<Product>): Observable<any> {
    return this.http.post(this.apiUrl, product);
  }

  // Registro masivo
  addBulk(products: Product[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/bulk`, products);
  }

  // Obtener todos los productos (Nota: este método no está definido en el backend aún)
  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);//return this.http.get<Product[]>(`${this.apiUrl}/state/0`); // Por defecto, estado 'Disponible'
  }

  // Obtener productos por estado
  getByState(state: number): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/state/${state}`);
  }

  // Marcar como defectuoso (usa PUT según el backend)
  markAsDefective(id: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/defect`, {});
  }

  // Marcar como salido (usa PUT según el backend)
  markAsShipped(id: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/ship`, {});
  }
}