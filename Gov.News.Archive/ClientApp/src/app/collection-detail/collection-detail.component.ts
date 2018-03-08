import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { CollectionsService } from "../collections/collections-service";
import { Collection } from "../collections/collection.model";
import { Archive } from "../collections/archive.model";

@Component({
  selector: 'collection-detail',
  moduleId: module.id,
  templateUrl: './collection-detail.component.html',
  styleUrls: ['./collection-detail.component.scss']
})
export class CollectionDetailComponent implements OnInit {
  archives: Archive[];
  collection: Collection;
  constructor(private collectionsService: CollectionsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params["id"];
    this.collectionsService.getCollection(id)
      .then((collection) => {
        this.collection = collection;
      });
    this.collectionsService.getArchives(id)
      .then((archives) => {
        this.archives = archives;
      });
  }

}
