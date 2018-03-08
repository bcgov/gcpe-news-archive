import { Component, OnInit } from '@angular/core';
import { Collection } from "./collection.model";
import { CollectionsService } from "./collections-service";

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrls: ['./collections.component.scss']
})
export class CollectionsComponent implements OnInit {
  collections: Collection[];
  constructor(private collectionsService: CollectionsService) { }
   
  ngOnInit(): void {
    this.collectionsService.getCollections()
      .then((collections) => {
        this.collections = collections;
      });
  }

}
