import List from "@mui/joy/List";

import SettingsRoundedIcon from "@mui/icons-material/SettingsRounded";

import SidebarNavLink from "./sidebar-navlink";

function SidebarBottomMenu() {
  return (
    <List
      size="sm"
      sx={{
        mt: "auto",
        flexGrow: 0,
        "--ListItem-radius": (theme) => theme.vars.radius.sm,
        "--List-gap": "8px",
        mb: 2,
      }}
    >
      <SidebarNavLink
        title="Setting"
        icon={<SettingsRoundedIcon />}
        to="/setting"
      />
    </List>
  );
}

export default SidebarBottomMenu;
