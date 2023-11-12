import { Helmet } from "react-helmet";

export interface PageHelmetProps {
  title: string;
  description?: string;
}

export default function PageHelmet(props: PageHelmetProps) {
  return (
    <Helmet>
      <title>{props.title} - Order System LLC</title>
      {props.description != null ? (
        <meta name="description" content={props.description} />
      ) : null}
    </Helmet>
  );
}
