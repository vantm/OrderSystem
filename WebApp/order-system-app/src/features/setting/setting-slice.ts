import { PayloadAction, createSelector, createSlice } from "@reduxjs/toolkit";
import { AppState } from "../../app/store";

type SettingState = {
  locale: string;
};

type SetLocalePayload = Pick<SettingState, "locale">;

const initialState: SettingState = {
  locale: "vi-VN",
};

const settingSlice = createSlice({
  name: "setting",
  initialState,
  reducers: {
    setLocale(state, { payload }: PayloadAction<SetLocalePayload>) {
      state.locale = payload.locale;
    },
  },
});

export const { setLocale } = settingSlice.actions;

const selectSetting = (state: AppState) => state.setting;

export const selectLocale = createSelector(
  selectSetting,
  (setting) => setting.locale
);

export default settingSlice.reducer;
