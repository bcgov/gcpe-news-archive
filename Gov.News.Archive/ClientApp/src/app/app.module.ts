import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Pipe, PipeTransform } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ArchiveDetailComponent } from 'app/archive-detail/archive-detail.component';
import { CollectionsComponent } from './collections/collections.component';
import { CollectionDetailComponent } from './collection-detail/collection-detail.component';
import { SearchBoxDirective } from './search-box/search-box.directive';
import { GeneralDataService } from 'app/general-data.service';
import { SearchComponent } from './search/search.component';
import { AdminModule } from 'app/admin/admin.module';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { CollectionsService } from "./collections/collections-service";

import { DomSanitizer } from '@angular/platform-browser'

@Pipe({ name: 'safeHtml' })
export class SafeHtmlPipe implements PipeTransform {
  constructor(private sanitized: DomSanitizer) { }
  transform(value) {
    console.log(this.sanitized.bypassSecurityTrustHtml(value))
    return this.sanitized.bypassSecurityTrustHtml(value);
  }
}

@NgModule({
  declarations: [
    SafeHtmlPipe,
    AppComponent,
    ArchiveDetailComponent,
    CollectionsComponent,
    CollectionDetailComponent,
    SearchBoxDirective,
    SearchComponent,
    BreadcrumbComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    NgbModule,
    AdminModule,
  ],
  providers: [GeneralDataService, CollectionsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
