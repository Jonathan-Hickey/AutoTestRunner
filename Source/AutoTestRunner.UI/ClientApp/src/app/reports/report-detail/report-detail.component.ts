import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'
import { IReport } from "../reports.component"
import { ChartType } from 'chart.js';
import { MultiDataSet, Label, Color } from 'ng2-charts';

@Component({
  selector: 'app-report-detail',
  templateUrl: './report-detail.component.html'
})

export class ReportDetailComponent {
  public report: IReport;

  public doughnutChartLabels: Label[] = ['Passed', 'Failed', 'Ignored'];
  public doughnutChartData: MultiDataSet = null;

  public doughnutChartType: ChartType = 'doughnut';
  public colors: Color[] = [
    {
      backgroundColor: [
        '#00F815', //green
        '#FF0000', //red
        '#FF9900' //ogrange
      ]
    }
  ];

  public options : Option

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {

    let projectId = this.route.snapshot.params['project_id'];
    let reportId = this.route.snapshot.params['report_id'];

    http.get<IReport>(baseUrl + 'ProjectWatcher/' + projectId + '/TestReports/' + reportId).subscribe(results => {
      this.report = results;

      this.doughnutChartData = [[this.report.number_of_passed_tests, this.report.number_of_failed_tests, this.report.number_of_ignored_tests]];
    }, error => console.error(error));
  }
}

