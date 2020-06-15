import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router'
import { IReport, ITestDetail } from "../reports.component"
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
  public chartType: ChartType = 'doughnut';
  public chartLegend = true;

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

  public testDetails: ITestDetail[];
  public testDetailsTableHeader: string;
  private clickedIndex : number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private route: ActivatedRoute) {

    let projectId = this.route.snapshot.params['project_id'];
    let reportId = this.route.snapshot.params['report_id'];

    http.get<IReport>(baseUrl + 'ProjectWatcher/' + projectId + '/TestReports/' + reportId).subscribe(result => {
      this.report = result;

      this.doughnutChartData = [[this.report.test_summary.number_of_passed_tests, this.report.test_summary.number_of_ignored_tests, this.report.test_summary.number_of_failed_tests]];
    }, error => console.error(error));
  }


  public chartClicked({ event, active }: { event: MouseEvent, active: any }): void {
    console.log(event, active);

    if (active !== null && active[0] !== null && active !== undefined && active[0] !== undefined ) {
      this.clickedIndex = active[0]._index;
      if (this.clickedIndex === 0) {
        this.testDetails = this.report.passed_tests;
        this.testDetailsTableHeader = "Passed Tests";
      }
      else if (this.clickedIndex === 1) {
        this.testDetails = this.report.ignored_tests;
        this.testDetailsTableHeader = "Ignore Tests";

      }
      else if (this.clickedIndex === 2) {
        this.testDetails = this.report.failed_tests;
        this.testDetailsTableHeader = "Failed Tests";
      }
    } else {
      this.testDetails = null;
    }
  }
}
