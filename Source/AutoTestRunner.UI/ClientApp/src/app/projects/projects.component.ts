import { Component, Inject, OnInit  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { faTrash } from "@fortawesome/free-solid-svg-icons"; 

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styles: [
    ' table {border-collapse:collapse; table-layout:fixed; width:100%;}',
    'table td {word-wrap:break-word;}',
    ]
})
export class ProjectsComponent implements OnInit {

  public projects: IProject[];
  public faTrash = faTrash;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  ngOnInit(): void {
    this.http.get<IProject[]>(this.baseUrl + 'ProjectWatcher').subscribe(result => {
      this.projects = result;
    }, error => console.error(error));
  }

  public deleteProject(projectId:string ) {
    this.http.delete(this.baseUrl + 'ProjectWatcher/' + projectId).subscribe(result => {
      this.ngOnInit();
    }, error => console.error(error));
  }
}

export interface IProject {
  project_watcher_id: string;
  rile_to_watch: string,
  project_watch_path: string,
}
