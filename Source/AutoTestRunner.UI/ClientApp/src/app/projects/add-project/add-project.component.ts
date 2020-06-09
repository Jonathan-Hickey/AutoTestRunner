import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProject } from "../projects.component";

@Component({
  selector: 'add-project',
  templateUrl: './add-project.component.html'
})
export class AddProjectComponent {
  public projects: IProject[];

  public fullPath: string;
  public isValidFilePath: boolean;
  public projectId: string;

  public isProjectWatcherCreated: boolean;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.isValidFilePath = null;
    this.isProjectWatcherCreated = null;
    this.projectId = null;
  }

  public validateFilePath(object) {

    if (this.fullPath == null || this.fullPath === "")
    {
      this.isValidFilePath = null;
    }
    else
    {
      this.http.get(this.baseUrl + 'validate/filepath?filePath=' + this.fullPath).subscribe(result => {
        this.isValidFilePath = true;
      }, error => this.isValidFilePath = false);
    }
  }

  public addProjectToBeWatched() {

    this.http.post<IProject>(this.baseUrl + 'ProjectWatcher', new CreateProjectWatcherDto(this.fullPath)).subscribe(result => {
      this.isProjectWatcherCreated = true;
      this.fullPath = null;
      this.projectId = result.project_watcher_id;
    }, error => this.isProjectWatcherCreated = false);
  }
}


class CreateProjectWatcherDto {
  full_project_path: string;

  constructor(path: string) {
    this.full_project_path = path;
  }
}
