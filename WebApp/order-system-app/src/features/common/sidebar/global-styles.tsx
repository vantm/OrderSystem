import GlobalStyles from "@mui/joy/GlobalStyles";

function SidebarGlobalStyles() {
  return (
    <GlobalStyles
      styles={(theme) => ({
        ":root": {
          "--Sidebar-width": "260px",
          [theme.breakpoints.up("lg")]: {
            "--Sidebar-width": "280px",
          },
        },
      })}
    />
  );
}

export default SidebarGlobalStyles;
