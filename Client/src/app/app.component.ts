import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  private httpClinet = inject(HttpClient);
   title = 'Client';
   products: any[]  = [];
   
   ngOnInit() {
    this.httpClinet.get<any>(this.baseUrl + 'products').subscribe({
      next: (response) => {
        this.products = response.data;
      },
      error: (error) => {
        console.error('Error fetching products:', error);
      },
      complete: () => {
        console.log('Product fetch complete');
      }
    });
   }
}
