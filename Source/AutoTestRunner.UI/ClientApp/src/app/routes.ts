import { Routes } from '@angular/router'

import { HomeComponent } from "./home/home.component";
import { ProjectsComponent } from "./projects/projects.component";
import { ReportsComponent } from "./reports/reports.component";
import { ReportDetailComponent } from "./reports/report-detail/report-detail.component";

export const appRoutes = [
  { path: 'projects', component: ProjectsComponent },
  { path: 'projects/:project_id/reports', component: ReportsComponent },
  { path: 'projects/:project_id/reports/:report_id', component: ReportDetailComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' }
]
