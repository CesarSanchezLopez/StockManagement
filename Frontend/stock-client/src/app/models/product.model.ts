export interface Product {
  id: string;
  name: string;
  productionType: 'Elaborado a mano' | 'Elaborado a mano y máquina'; // importante
  state: ProductState;
  createdAt?: Date;
}

export enum ProductState {
  Disponible = 0,
  Salido = 1,
  Defectuoso = 2
}