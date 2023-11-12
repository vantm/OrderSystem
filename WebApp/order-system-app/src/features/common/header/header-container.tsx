import Sheet from "@mui/joy/Sheet";
import { PropsWithChildren } from "react";

function HeaderContainer(props: Readonly<PropsWithChildren>) {
  return (
    <Sheet
      sx={{
        display: { xs: "flex", md: "none" },
        alignItems: "center",
        justifyContent: "space-between",
        position: "fixed",
        top: 0,
        width: "100vw",
        height: "var(--Header-height)",
        zIndex: 9995,
        p: 2,
        gap: 1,
        borderBottom: "1px solid",
        borderColor: "background.level1",
        boxShadow: "sm",
      }}
    >
      {props.children}
    </Sheet>
  );
}

export default HeaderContainer;
