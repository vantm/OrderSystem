import Avatar from "@mui/joy/Avatar";
import Box from "@mui/joy/Box";
import IconButton from "@mui/joy/IconButton";
import Typography from "@mui/joy/Typography";
import LogoutRoundedIcon from "@mui/icons-material/LogoutRounded";

function SidebarUserSection() {
  return (
    <Box sx={{ display: "flex", gap: 1, alignItems: "center" }}>
      <Avatar
        variant="outlined"
        size="sm"
        src="https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?auto=format&fit=crop&w=286"
      />
      <Box sx={{ minWidth: 0, flex: 1 }}>
        <Typography level="title-sm">Siriwat K.</Typography>
        <Typography level="body-xs">siriwatk@test.com</Typography>
      </Box>
      <IconButton size="sm" variant="plain" color="neutral">
        <LogoutRoundedIcon />
      </IconButton>
    </Box>
  );
}

export default SidebarUserSection;
