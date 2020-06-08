import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'
import { IReport } from "../reports.component"

@Component({
  selector: 'app-report-detail',
  templateUrl: './report-detail.component.html'
})

export class ReportDetailComponent {
  public report: IReport;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {

    let projectId = this.route.snapshot.params['project_id'];
    let reportId = this.route.snapshot.params['report_id'];

    http.get<IReport>(baseUrl + 'ProjectWatcher/' + projectId + '/TestReports/' + reportId).subscribe(results => {
      this.report = results;
    }, error => console.error(error));
  }
}

