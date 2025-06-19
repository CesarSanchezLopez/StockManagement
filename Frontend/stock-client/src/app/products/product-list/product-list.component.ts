import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  successMsg = '';
  errorMsg = '';
  showActions = true;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getAll().subscribe({
      next: (data) => this.products = data,
      error: () => this.errorMsg = 'Error al cargar productos'
    });
  }

  filterByState(state: number): void {
    this.productService.getByState(state).subscribe({
      next: (data) => this.products = data,
      error: () => this.errorMsg = 'Error al filtrar productos'
    });
  }

  markShipped(id: string): void {
    this.productService.markAsShipped(id).subscribe({
      next: () => {
        this.successMsg = 'Producto marcado como Salido';
        this.loadProducts();
      },
      error: () => this.errorMsg = 'No se pudo marcar como Salido'
    });
  }

  markDefective(id: string): void {
    this.productService.markAsDefective(id).subscribe({
      next: () => {
        this.successMsg = 'Producto marcado como Defectuoso';
        this.loadProducts();
      },
      error: () => this.errorMsg = 'No se pudo marcar como Defectuoso'
    });
  }

  getStateName(state: number): string {
    return ['Disponible', 'Salido', 'Defectuoso'][state] || 'Desconocido';
  }
}
