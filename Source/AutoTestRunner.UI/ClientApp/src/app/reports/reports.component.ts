import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
})

export class ReportsComponent {
  public reports: IReport[];
  public project_name: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {

    let projectId = this.route.snapshot.params['project_id'];

    http.get<IReport[]>(baseUrl + 'ProjectWatcher/' + projectId +'/TestReports').subscribe(results => {
      this.reports = results;
      this.project_name = this.reports[0].test_summary.project_name;
    }, error => console.error(error));
  }
}

export interface IReport {
  report_id : string;
  project_watcher_id : string;
  run_date_time : string;
  test_summary: IReportSummary;
}

export interface IReportSummary {
  project_name: string;
  number_of_passed_tests: number;
  number_of_failed_tests: number;
  number_of_ignored_tests: number;
  total_number_of_tests: number;
  time_taken_in_second: number;
}
