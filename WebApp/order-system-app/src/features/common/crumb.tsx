import { useMemo } from "react";

import Link from "@mui/joy/Link";
import Typography from "@mui/joy/Typography";

import { Link as RouterLink, generatePath, useMatch } from "react-router-dom";

export type RouteData = {
  // TODO: fix remove `any`
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  [k: string]: any;
};

export type CrumbProps = {
  to: string;
  label: string | React.ReactNode;
  data?: RouteData;
};

export function Crumb(props: CrumbProps) {
  const toPath = useMemo<string>(() => {
    if (!props.data) {
      return props.to;
    }
    return generatePath(props.to, props.data);
  }, [props.to, props.data]);

  const match = useMatch({
    path: props.to,
    end: true,
  });

  if (match) {
    return (
      <Typography textColor="text.primary" level="body-sm">
        {props.label}
      </Typography>
    );
  }

  return (
    <Link underline="none" color="neutral" component={RouterLink} to={toPath}>
      {props.label}
    </Link>
  );
}
