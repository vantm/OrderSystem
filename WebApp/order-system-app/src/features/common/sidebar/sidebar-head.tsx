import Box from "@mui/joy/Box";
import IconButton from "@mui/joy/IconButton";
import Typography from "@mui/joy/Typography";
import BrightnessAutoRoundedIcon from "@mui/icons-material/BrightnessAutoRounded";
import ColorSchemeToggle from "./color-scheme-toggle";

function SidebarHead() {
  return (
    <Box sx={{ display: "flex", gap: 1, alignItems: "center" }}>
      <IconButton variant="soft" color="primary" size="sm">
        <BrightnessAutoRoundedIcon />
      </IconButton>
      <Typography level="title-lg">Acme Co.</Typography>
      <ColorSchemeToggle sx={{ ml: "auto" }} />
    </Box>
  );
}

export default SidebarHead;
