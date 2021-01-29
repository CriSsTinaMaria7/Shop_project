import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Shop';

  constructor(private http: HttpClient) {}

  //lifecycle hook
ngOnInit(): void {
  this.http.get('https://localhost:5001/api/products').subscribe((response: any) =>{
    console.log(response);
  }, error => {
    console.log(error);
  });
  } 
}
