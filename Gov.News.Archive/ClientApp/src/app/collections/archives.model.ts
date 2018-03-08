export class Archive {
  title: string
  summary: string;
  releaseDate: any;
  id: number;

  constructor() { }
  
  pdf() {
    return "/api/Collections/download/" + this.id + ".pdf"
  }
}
