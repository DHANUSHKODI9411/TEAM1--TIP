import { Component, inject } from '@angular/core';
import { AuthService } from '../auth-service';
import { NavbarComponent } from "../navbar-component/navbar-component";

@Component({
  selector: 'app-home-component',
  imports: [NavbarComponent],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {
  authSvc: AuthService = inject(AuthService);

  constructor() {
    if (sessionStorage.getItem('empName')) {
      this.authSvc.getToken().subscribe({
        next: (response: any) => {
          sessionStorage.setItem("token", response);
        },
        error: (err) => { console.error(err); }
      });
    }
  }
}
