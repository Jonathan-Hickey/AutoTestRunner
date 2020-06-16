import { appRoutes} from "./routes";
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, ExtraOptions } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ProjectsComponent } from './projects/projects.component';
import { ReportsComponent } from './reports/reports.component';
import { ReportDetailComponent } from "./reports/report-detail/report-detail.component";
import { AddProjectComponent } from "./projects/add-project/add-project.component";
import { BreadcrumbComponent } from "./breadcrumb/breadcrumb.component"
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'

import { ChartsModule } from 'ng2-charts';

export const routingConfiguration: ExtraOptions = {
  paramsInheritanceStrategy: 'always'
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProjectsComponent,
    ReportsComponent,
    ReportDetailComponent,
    AddProjectComponent,
    BreadcrumbComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes, routingConfiguration),
    ChartsModule,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
