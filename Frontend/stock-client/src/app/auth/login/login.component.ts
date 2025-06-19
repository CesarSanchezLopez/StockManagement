import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/models/login-request.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginData: LoginRequest = {
    email: '',
    password: ''
  };

  errorMessage: string = '';

  constructor(private auth: AuthService, private router: Router) {}

  login() {
    this.auth.login(this.loginData).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token);
        this.router.navigate(['/']); // redirige donde quieras
      },
      error: () => {
        this.errorMessage = 'Credenciales inválidas';
      }
    });
  }
}