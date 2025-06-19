import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './auth/login/login.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductFormComponent } from './products/product-form/product-form.component';
import { ProductBulkUploadComponent } from './products/product-bulk-upload/product-bulk-upload.component';
import { authGuard } from './auth/auth.guard'; // este es el guard como función

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'products',
    canActivate: [authGuard], // aquí va como función
    children: [
      { path: '', component: ProductListComponent },
      { path: 'new', component: ProductFormComponent },
      { path: 'bulk', component: ProductBulkUploadComponent }
    ]
  },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}