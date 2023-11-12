import IconButton from "@mui/joy/IconButton";
import MenuIcon from "@mui/icons-material/Menu";
import { toggleSidebar } from "../sidebar-utils";
import HeaderGlobalStyles from "./header-global-styles";
import HeaderContainer from "./header-container";

export default function Header() {
  return (
    <HeaderContainer>
      <HeaderGlobalStyles />
      <IconButton
        onClick={() => toggleSidebar()}
        variant="outlined"
        color="neutral"
        size="sm"
      >
        <MenuIcon />
      </IconButton>
    </HeaderContainer>
  );
}
