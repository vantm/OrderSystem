import { Provider } from "react-redux";
import { RouterProvider } from "react-router-dom";

import { CssVarsProvider } from "@mui/joy/styles";
import CssBaseline from "@mui/joy/CssBaseline";
import Typography from "@mui/joy/Typography";

import "@fontsource/inter";

import routes from "./routes";
import store from "./store";

function App() {
  return (
    <Provider store={store}>
      <CssBaseline />
      <CssVarsProvider>
        <RouterProvider
          router={routes}
          fallbackElement={<Typography level="body-lg">Loading...</Typography>}
        />
      </CssVarsProvider>
    </Provider>
  );
}

export default App;
