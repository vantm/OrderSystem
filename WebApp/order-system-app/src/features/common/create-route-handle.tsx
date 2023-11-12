import { createElement } from "react";
import { Crumb } from "./crumb";

export type RouteHandle = {
  title: string;
  icon?: React.ReactNode;
  path: string;
};

export function isRouteHandle(obj?: object) {
  if (obj == null) {
    return false;
  }
  if ("pageTitle" in obj && "crumbPath" in obj) {
    return true;
  }
}

export default function createRouteHandle(handle: RouteHandle) {
  return {
    title: handle.title,
    crumb: createElement(Crumb, {
      to: handle.path,
      label: handle.icon ?? handle.title,
    }),
  };
}
