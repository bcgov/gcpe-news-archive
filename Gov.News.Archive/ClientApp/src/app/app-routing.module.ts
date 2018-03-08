import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArchiveDetailComponent } from 'app/archive-detail/archive-detail.component';
import { CollectionsComponent } from 'app/collections/collections.component';
import { CollectionDetailComponent } from 'app/collection-detail/collection-detail.component';
import { SearchComponent } from 'app/search/search.component';

const routes: Routes = [
  {
    path: '',
    children: []
  },
  {
    path: 'collections',
    component: CollectionsComponent,
    data: {
      breadcrumb: 'Archived News Releases'
    }

  },
  { path: "collections/:id", component: CollectionDetailComponent },
  { path: "archives/:id", component: ArchiveDetailComponent },
  {
    path: 'search',
    component: SearchComponent,
    data: {
      breadcrumb: 'Search'
    }
  }  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
