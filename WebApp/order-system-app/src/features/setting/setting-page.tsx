import { useState } from "react";

import Box from "@mui/joy/Box";
import Input from "@mui/joy/Input";

import { useAppDispatch, useAppSelector } from "../../app/store";
import MainLayout from "../common/main-layout";
import { selectLocale, setLocale } from "./setting-slice";
import FormControl from "@mui/joy/FormControl";
import FormLabel from "@mui/joy/FormLabel";

export default function SettingPage() {
  const locale = useAppSelector(selectLocale);
  const dispatch = useAppDispatch();
  const [value, setValue] = useState(locale);

  const newLocale = value?.trim();

  function handleLocaleAccept(): void {
    if (newLocale != null && newLocale != "" && newLocale !== locale) {
      dispatch(
        setLocale({
          locale: value,
        })
      );
    }
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement>): void {
    setValue(e.target.value);
  }

  return (
    <MainLayout>
      <Box>
        <FormControl>
          <FormLabel>Locale</FormLabel>
          <Input
            aria-label="locale"
            placeholder="Locale"
            required
            value={value}
            onChange={handleChange}
            onBlur={handleLocaleAccept}
            error={newLocale == null || newLocale == ""}
          />
        </FormControl>
      </Box>
    </MainLayout>
  );
}
