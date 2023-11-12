import Chip from "@mui/joy/Chip";
import ListItem from "@mui/joy/ListItem";
import ListItemContent from "@mui/joy/ListItemContent";
import ListItemButton from "@mui/joy/ListItemButton";

import { useMatch, useResolvedPath, useNavigate } from "react-router-dom";

interface SidebarNavLinkProps {
  icon: React.ReactNode;
  title: string;
  chip?: number;
  to: string;
}

function SidebarNavLink(props: SidebarNavLinkProps) {
  const resolved = useResolvedPath(props.to);
  const match = useMatch({ path: resolved.pathname, end: true });
  const navigate = useNavigate();

  function handleNavigate() {
    navigate(props.to);
  }

  return (
    <ListItem>
      <ListItemButton selected={match != null} onClick={handleNavigate}>
        {props.icon}
        <ListItemContent>{props.title}</ListItemContent>
        {props.chip != null ? (
          <Chip size="sm" color="primary" variant="solid">
            {props.chip}
          </Chip>
        ) : null}
      </ListItemButton>
    </ListItem>
  );
}

export default SidebarNavLink;
