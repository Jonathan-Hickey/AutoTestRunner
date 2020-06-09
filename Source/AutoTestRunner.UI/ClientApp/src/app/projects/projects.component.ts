import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html'
})
export class ProjectsComponent {
  public projects: IProject[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IProject[]>(baseUrl + 'ProjectWatcher').subscribe(result => {
      this.projects = result;
    }, error => console.error(error));
  }
}

export interface IProject {
  project_watcher_id: string;
  full_project_path: string;
  rile_to_watch: string,
  project_watch_path: string
}
