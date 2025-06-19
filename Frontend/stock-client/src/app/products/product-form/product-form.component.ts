import { Component } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from  'src/app/models/product.model';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent {
  product: Partial<Product> = {
    name: '',
    productionType: undefined,
    state: 0
  };

  successMsg: string = '';
  errorMsg: string = '';

  constructor(private productService: ProductService) {}

  submit(): void {
    this.successMsg = '';
    this.errorMsg = '';

    this.productService.addProduct(this.product).subscribe({
      next: () => {
        this.successMsg = '✅ Producto registrado con éxito';
        this.product = { name: '', productionType: undefined, state: 0 };
      },
      error: (err) => {
        console.error(err);
        this.errorMsg = '❌ Error al registrar el producto';
      }
    });
  }
}