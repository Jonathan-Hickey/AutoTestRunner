import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, NavigationEnd, Event } from "@angular/router";
import { filter, distinctUntilChanged } from "rxjs/operators";

@Component({
  selector: "app-breadcrumb",
  templateUrl: "./breadcrumb.component.html",
  styleUrls: ["./breadcrumb.component.css"]
})
export class BreadcrumbComponent implements OnInit {

  breadcrumbs: IBreadCrumb[];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
  }

  ngOnInit() {
    this.router.events.pipe(
      filter((event: Event) => event instanceof NavigationEnd),
      distinctUntilChanged()
    ).subscribe(() => {
      this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
    });
  }

  buildBreadCrumb(route: ActivatedRoute, url: string = "", breadcrumbs: IBreadCrumb[] = []): IBreadCrumb[] {

    //If no routeConfig is avalailable we are on the root path
    let label = route.routeConfig &&
                route.routeConfig.data &&
                route.routeConfig.data instanceof BreadCrumbRouteData
                ? route.routeConfig.data.breadcrumb
                : "";


    let path = route.routeConfig && route.routeConfig.data ? route.routeConfig.path : "";

    // If the route is dynamic route such as ':id', remove it
    const lastRoutePart = path.split("/").pop();
    const isDynamicRoute = lastRoutePart.startsWith(":");
    if (isDynamicRoute && !!route.snapshot) {
      const paramName = lastRoutePart.split(":")[1];
      path = path.replace(lastRoutePart, route.snapshot.params[paramName]);
      label = route.snapshot.params[paramName];
    }

    //In the routeConfig the complete path is not available,
    //so we rebuild it each time
    const nextUrl = path ? `${url}/${path}` : url;

    // Only adding route with non-empty label
    const newBreadcrumbs = label ? [...breadcrumbs, new BreadCrumb(label, url)] : [...breadcrumbs];
    if (route.firstChild) {
      //If we are not on our current path yet,
      //there will be more children to look after, to build our breadcumb
      return this.buildBreadCrumb(route.firstChild, nextUrl, newBreadcrumbs);
    }
    return newBreadcrumbs;
  }
}

export interface IBreadCrumb {
  label: string;
  url: string;
}

export class BreadCrumb implements IBreadCrumb {
  label: string;
  url: string;

  constructor(label: string, url: string) {
    this.label = label;
    this.url = url;
  }
}


export class BreadCrumbRouteData {
  breadcrumb: string;
}
