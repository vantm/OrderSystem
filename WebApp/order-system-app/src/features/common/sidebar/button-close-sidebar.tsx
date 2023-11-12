import Box from "@mui/joy/Box";
import { closeSidebar } from "../sidebar-utils";

function CloseSidebarButton() {
  return (
    <Box
      className="Sidebar-overlay"
      sx={{
        position: "fixed",
        zIndex: 9998,
        top: 0,
        left: 0,
        width: "100vw",
        height: "100vh",
        opacity: "var(--SideNavigation-slideIn)",
        backgroundColor: "var(--joy-palette-background-backdrop)",
        transition: "opacity 0.4s",
        transform: {
          xs: "translateX(calc(100% * (var(--SideNavigation-slideIn, 0) - 1) + var(--SideNavigation-slideIn, 0) * var(--Sidebar-width, 0px)))",
          lg: "translateX(-100%)",
        },
      }}
      onClick={() => closeSidebar()}
    />
  );
}

export default CloseSidebarButton;
