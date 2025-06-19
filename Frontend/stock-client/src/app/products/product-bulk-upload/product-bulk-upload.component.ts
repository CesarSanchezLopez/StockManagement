import { Component } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-bulk-upload',
  templateUrl: './product-bulk-upload.component.html'
})
export class ProductBulkUploadComponent {
  bulkText = '';
  successMsg = '';
  errorMsg = '';
  previewProducts: Product[] = [];

  constructor(private productService: ProductService) {}

  submit(): void {
  this.successMsg = '';
  this.errorMsg = '';

  if (!this.previewProducts || this.previewProducts.length === 0) {
    this.errorMsg = 'No hay productos válidos para cargar.';
    return;
  }

  this.productService.addBulk(this.previewProducts).subscribe({
    next: () => {
      this.successMsg = 'Productos cargados correctamente';
      this.previewProducts = [];
      this.bulkText = '';
    },
    error: () => {
      this.errorMsg = 'Error al cargar los productos';
    }
  });
}

  insertExample(event: Event): void {
    event.preventDefault();
    this.bulkText = JSON.stringify([
      { name: "Silla pequeña", productionType: "Elaborado a mano", state: 0 },
      { name: "Mesa mediana", productionType: "Elaborado a mano y máquina", state: 0 },
      { name: "Banco alto", productionType: "Elaborado a mano", state: 2 },
      { name: "Cama doble", productionType: "Elaborado a mano y máquina", state: 0 },
      { name: "Estantería", productionType: "Elaborado a mano", state: 1 }
    ], null, 2);
    this.previewProducts = JSON.parse(this.bulkText);
  }

  handleFileUpload(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    if (!file) return;

    const reader = new FileReader();
    reader.onload = () => {
      try {
        this.bulkText = reader.result as string;
        this.previewProducts = JSON.parse(this.bulkText);
      } catch (err) {
        this.errorMsg = 'Error leyendo el archivo JSON';
      }
    };
    reader.readAsText(file);
  }

  getStateName(state: number): string {
    return ['Disponible', 'Despachado', 'Defectuoso'][state] || 'Desconocido';
  }
  removeRow(index: number): void {
  this.previewProducts.splice(index, 1);
  this.bulkText = JSON.stringify(this.previewProducts, null, 2);
}
}