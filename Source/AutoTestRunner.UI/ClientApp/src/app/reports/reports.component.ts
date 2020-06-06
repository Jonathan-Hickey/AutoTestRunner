import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html'
})
export class ReportsComponent {
  public forecasts: IReport[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IReport[]>(baseUrl + 'ProjectWatcher').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface IReport {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
