import { useState } from "react";
import Box from "@mui/joy/Box";
import { TogglerProps } from "./toggler-props";

export default function Toggler(props: Readonly<TogglerProps>) {
  const { defaultExpanded = false, renderToggle, children } = props;
  const [open, setOpen] = useState(defaultExpanded);

  return (
    <>
      {renderToggle({ open, setOpen })}
      <Box
        sx={{
          display: "grid",
          gridTemplateRows: open ? "1fr" : "0fr",
          transition: "0.2s ease",
          "& > *": {
            overflow: "hidden",
          },
        }}
      >
        {children}
      </Box>
    </>
  );
}
