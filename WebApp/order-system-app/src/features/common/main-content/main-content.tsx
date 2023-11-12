import { PropsWithChildren } from "react";
import Typography from "@mui/joy/Typography";
import Box from "@mui/joy/Box";
import Stack from "@mui/joy/Stack";
import { MainContentProps } from "./main-content-props";

function MainContent(props: Readonly<PropsWithChildren<MainContentProps>>) {
  return (
    <Box
      component="main"
      className="MainContent"
      sx={{
        px: {
          xs: 2,
          md: 6,
        },
        pt: {
          xs: "calc(12px + var(--Header-height))",
          sm: "calc(12px + var(--Header-height))",
          md: 3,
        },
        pb: {
          xs: 2,
          sm: 2,
          md: 3,
        },
        flex: 1,
        display: "flex",
        flexDirection: "column",
        minWidth: 0,
        height: "100dvh",
        overflow: "auto",
        gap: 1,
      }}
    >
      {props.breadcrumbComponent ? (
        <Box sx={{ display: "flex", alignItems: "center" }}>
          {props.breadcrumbComponent}
        </Box>
      ) : null}
      <Box
        sx={{
          display: "flex",
          my: 1,
          gap: 1,
          flexDirection: { xs: "column", sm: "row" },
          alignItems: { xs: "start", sm: "center" },
          flexWrap: "wrap",
          justifyContent: "space-between",
        }}
      >
        <Typography level="h2">{props.title}</Typography>
        <Stack direction="row" gap={1}>
          {props.actionsComponent}
        </Stack>
      </Box>
      <Box>{props.children}</Box>
    </Box>
  );
}

export default MainContent;
