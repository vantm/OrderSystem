import { PropsWithChildren, cloneElement } from "react";

import Box from "@mui/joy/Box";

import ChevronRightRoundedIcon from "@mui/icons-material/ChevronRightRounded";

import { useMatches } from "react-router-dom";
import { chain } from "lodash";

import Header from "../header";
import Sidebar from "../sidebar";
import MainContent from "../main-content";
import { MainContentProps } from "../main-content/main-content-props";
import { Breadcrumbs } from "@mui/joy";
import PageHelmet from "../page-helmet";

type MainLayoutProps = PropsWithChildren<
  Omit<MainContentProps, "title" | "breadcrumbComponent">
>;

function MainLayout(props: MainLayoutProps) {
  const matches = useMatches();
  const match = matches[matches.length - 1];

  let title = "";
  if (
    match?.handle != null &&
    typeof match.handle === "object" &&
    "title" in match.handle
  ) {
    title = match.handle.title as string;
  }

  let breadcrumb: React.ReactNode | null = null;

  const isIndex = match.pathname !== "/";

  if (isIndex) {
    const crumbs = chain(matches)
      .filter("handle")
      .map("handle.crumb")
      .map((el: React.ReactElement, index: number) =>
        cloneElement(el, { key: /** NOSONAR */ index })
      )
      .value();

    breadcrumb = (
      <Breadcrumbs
        size="sm"
        aria-label="breadcrumbs"
        separator={<ChevronRightRoundedIcon fontSize="small" />}
        sx={{ pl: 0 }}
      >
        {crumbs}
      </Breadcrumbs>
    );
  }

  return (
    <Box sx={{ display: "flex", minHeight: "100dvh" }}>
      <PageHelmet title={title} />
      <Header />
      <Sidebar />
      <MainContent
        title={title}
        breadcrumbComponent={breadcrumb}
        actionsComponent={props.actionsComponent}
      >
        {props.children}
      </MainContent>
    </Box>
  );
}

export default MainLayout;
