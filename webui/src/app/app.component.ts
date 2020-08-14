import { Component } from '@angular/core';
import { Repository } from "./models/repository";
import { Party } from "./models/party.model";
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'webui';
  constructor(private repo:Repository){}

  get party$(): Observable<Party>{
    return this.repo.party$
  }
}
