import GlobalStyles from "@mui/joy/GlobalStyles";

function HeaderGlobalStyles() {
  return (
    <GlobalStyles
      styles={(theme) => ({
        ":root": {
          "--Header-height": "52px",
          [theme.breakpoints.up("md")]: {
            "--Header-height": "0px",
          },
        },
      })}
    />
  );
}

export default HeaderGlobalStyles;
