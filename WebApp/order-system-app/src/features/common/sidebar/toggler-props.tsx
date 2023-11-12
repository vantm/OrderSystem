import { Dispatch, SetStateAction } from "react";

interface TogglerRenderFunctionParams {
  open: boolean;
  setOpen: Dispatch<SetStateAction<boolean>>;
}

type TogglerRenderFunction = (
  params: TogglerRenderFunctionParams
) => React.ReactNode;

export interface TogglerProps {
  defaultExpanded?: boolean;
  children: React.ReactNode;
  renderToggle: TogglerRenderFunction;
}
