import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { CollectionsService } from "../collections/collections-service";
import { Collection } from "../collections/collection.model";
import { Archive } from "../collections/archive.model";

@Component({
  selector: 'archive-detail',
  moduleId: module.id,
  templateUrl: './archive-detail.component.html',
  styleUrls: ['./archive-detail.component.scss']
})
export class ArchiveDetailComponent implements OnInit {
  archive: Archive;
  constructor(private collectionsService: CollectionsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params["id"];
    this.collectionsService.getArchive(id)
      .then((archive) => {
        this.archive = archive;
      });
  }

}
