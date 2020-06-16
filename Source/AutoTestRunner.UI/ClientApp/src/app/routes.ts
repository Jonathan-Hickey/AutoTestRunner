import { Routes } from '@angular/router'

import { HomeComponent } from "./home/home.component";
import { ProjectsComponent } from "./projects/projects.component";
import { ReportsComponent } from "./reports/reports.component";
import { ReportDetailComponent } from "./reports/report-detail/report-detail.component";

export const appRoutes: Routes = [
  {
    path: 'projects', component: ProjectsComponent,
    children: [
      {
        path: ':project_id/reports',
        component: ReportsComponent,
        children: [
          {
            path: ':report_id',
            component: ReportDetailComponent,
          }
        ]
      }
    ]
  },
  { path: '', component: HomeComponent, pathMatch: 'full' }
]
