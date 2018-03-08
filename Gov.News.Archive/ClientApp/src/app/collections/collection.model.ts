import { DatePipe } from '@angular/common';

export class Collection {

  startDate: any;
  endDate: any;
  id: any;
  name: string;

  constructor() { }
  
  getLinkTitle() {
    var result = '';
    result = "startDate | date" + " to " + "endDate | date";
  }
}
