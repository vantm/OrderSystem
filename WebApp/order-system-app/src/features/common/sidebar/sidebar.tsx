import Box from "@mui/joy/Box";
import Divider from "@mui/joy/Divider";
import { listItemButtonClasses } from "@mui/joy/ListItemButton";
import SidebarHead from "./sidebar-head";
import SidebarContainer from "./sidebar-container";
import SidebarGlobalStyles from "./global-styles";
import CloseSidebarButton from "./button-close-sidebar";
import SidebarNav from "./sidebar-nav";
import SidebarBottomMenu from "./sidebar-bottom-menu";
import SidebarUserSection from "./sidebar-user-section";

function Sidebar() {
  return (
    <SidebarContainer>
      <SidebarGlobalStyles />
      <SidebarHead />
      <CloseSidebarButton />
      <Box
        sx={{
          minHeight: 0,
          overflow: "hidden auto",
          flexGrow: 1,
          display: "flex",
          flexDirection: "column",
          [`& .${listItemButtonClasses.root}`]: {
            gap: 1.5,
          },
        }}
      >
        <SidebarNav />
        <SidebarBottomMenu />
      </Box>
      <Divider />
      <SidebarUserSection />
    </SidebarContainer>
  );
}

export default Sidebar;
