import Box from "@mui/joy/Box";
import PageHelmet from "../common/page-helmet";

const pageTitle = "Login into your account";

export default function LoginPage() {
  return (
    <Box>
      <PageHelmet title={pageTitle} />
      Login
    </Box>
  );
}
