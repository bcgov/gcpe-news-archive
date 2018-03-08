import { Component, Pipe, PipeTransform } from '@angular/core';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  logoPath: string;

  title = 'News Archive';

  constructor() {
    this.logoPath = 'assets/images/Gov/gov3_bc_logo.png';
  }

}
