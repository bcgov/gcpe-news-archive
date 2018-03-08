import { Injectable } from "@angular/core";
import { Http, Headers, Response } from "@angular/http";
import "rxjs/add/operator/toPromise";

import { Collection } from "./collection.model";

import { Archive } from "./archive.model";

@Injectable()
export class CollectionsService {
  constructor(private http: Http) { }

  getArchive(id: any) {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    return this.http.get("http://localhost:9010/api/archives/" + id, {
      headers: headers
    })
      .toPromise()
      .then((res: Response) => {
        let data = res.json();
        let archive = new Archive();
        archive.id = data.id;
        archive.dateReleased = data.dateReleased;
        archive.title = data.title;
        archive.ministryText = data.ministryText;
        archive.htmlContent = data.htmlContent;
        archive.textContent = data.textContent;
        archive.preview = data.preview;
        archive.body = data.body;
        return archive;
      })
      .catch(this.handleError);
  }

  getArchives(id: any) {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    return this.http.get("http://localhost:9010/api/collections/" + id + "/archives", {
      headers: headers
    })
      .toPromise()
      .then((res: Response) => {
        let data = res.json();
        let allArchives = [];

        data.forEach((entry) => {
          let archive = new Archive();
          archive.id = entry.id;
          archive.dateReleased = entry.dateReleased;
          archive.title = entry.title;
          archive.ministryText = entry.ministryText;
          archive.htmlContent = entry.htmlContent;
          archive.textContent = entry.textContent;
          archive.preview = entry.preview;
          allArchives.push(archive);
        });

        return allArchives;

      })
      .catch(this.handleError);
  }


  getCollections() {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    return this.http.get("http://localhost:9010/api/collections", {
      headers: headers
    })
      .toPromise()
      .then((res: Response) => {
        
        
        let data = res.json();
        let allCollections = [];
        
        data.forEach((entry) => {
          let collection = new Collection();
          collection.startDate = entry.startDate;
          collection.id = entry.id;
          collection.name = entry.name;
          allCollections.push(collection);
        });

        return allCollections;

        
      })
      .catch(this.handleError);    
  }

  getCollection(id: any) {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");

    return this.http.get("http://localhost:9010/api/collections/" + id , {
      headers: headers
    })
      .toPromise()
      .then((res: Response) => {
        let data = res.json();

        let collection = new Collection();
        collection.id = data.id;
        collection.startDate = data.startDate;
        collection.endDate = data.endDate;
        collection.name = data.name;
        return collection;
      })
      .catch(this.handleError);
  }

  private handleError(error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || "";
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ""} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Promise.reject(errMsg);
  }
}
