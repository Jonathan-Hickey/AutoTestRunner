import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
})

export class ReportsComponent implements OnInit {
  
  public reports: IReport[];
  public project_name: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.activatedRoute.url.subscribe(url => {
      let projectId = this.activatedRoute.snapshot.params['project_id'];
      this.http.get<IReport[]>(this.baseUrl + 'ProjectWatcher/' + projectId + '/TestReports').subscribe(results => {
        this.reports = results;
        if (this.reports.length > 0) {
          this.project_name = this.reports[0].test_summary.project_name;
        }
      }, error => console.error(error));
    });
  }
}

export interface IReport {
  report_id : string;
  project_watcher_id : string;
  run_date_time : string;
  test_summary: IReportSummary;
  passed_tests: ITestDetail[];
  failed_tests: ITestDetail[];
  ignored_tests : ITestDetail[];
}

export interface IReportSummary {
  project_name: string;
  number_of_passed_tests: number;
  number_of_failed_tests: number;
  number_of_ignored_tests: number;
  total_number_of_tests: number;
  time_taken_in_second: number;
}

export enum TestStatus {
  Passed = 1,
  Failed = 2,
  Ignored = 3
}

export interface ITestDetail {
  test_name: string;
  time_taken_in_milliseconds: number;
  test_status: TestStatus;
}
