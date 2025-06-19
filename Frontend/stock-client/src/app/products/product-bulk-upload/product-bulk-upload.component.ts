import { Component } from '@angular/core';
import { ProductService } from '../product.service';
import { Product, ProductState } from '../../models/product.model';

@Component({
  selector: 'app-product-bulk-upload',
  templateUrl: './product-bulk-upload.component.html'
})
export class ProductBulkUploadComponent {
  bulkText = '';
  successMsg = '';
  errorMsg = '';

  constructor(private productService: ProductService) {}

  submit(): void {
    this.successMsg = '';
    this.errorMsg = '';

    try {
      const products: Product[] = JSON.parse(this.bulkText);
      this.productService.addBulk(products).subscribe({
        next: () => {
          this.successMsg = '✅ Productos cargados exitosamente';
          this.bulkText = '';
        },
        error: () => {
          this.errorMsg = '❌ Error al cargar los productos';
        }
      });
    } catch {
      this.errorMsg = '❌ Formato inválido. Asegúrate de usar un JSON válido.';
    }
  }
}