import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'
import { IReport } from "../reports.component"
import { ChartType, ChartOptions } from 'chart.js';
import { MultiDataSet, Label, Color } from 'ng2-charts';

@Component({
  selector: 'app-report-detail',
  templateUrl: './report-detail.component.html'
})

export class ReportDetailComponent {
  public report: IReport;

  public doughnutChartLabels: Label[] = ['Passed', 'Ignored', 'Failed',];
  public doughnutChartData: MultiDataSet = null;

  public colors: Color[] = [
    {
      backgroundColor: [
        '#568435', //green
        '#FFA719', //ogrange
        '#FF4E3A' //red
      ]
    }
  ];

  //https://stackblitz.com/edit/ng2-charts-fontfamily?file=src%2Fapp%2Fapp.component.html
  //https://stackblitz.com/edit/ng2-charts-doughnut-template
  //https://stackblitz.com/edit/ng2-charts-bar-labels
  public chartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    legend: {
      labels: {
        fontFamily: '"Arvo", serif',
        fontSize: 18,
        fontColor: `white`
      }
    },
  };

  public chartType: ChartType = 'doughnut';
  public chartLegend = true;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {

    let projectId = this.route.snapshot.params['project_id'];
    let reportId = this.route.snapshot.params['report_id'];

    http.get<IReport>(baseUrl + 'ProjectWatcher/' + projectId + '/TestReports/' + reportId).subscribe(results => {
      this.report = results;

      this.doughnutChartData = [[this.report.number_of_passed_tests, this.report.number_of_ignored_tests, this.report.number_of_failed_tests]];
    }, error => console.error(error));
  }
}

