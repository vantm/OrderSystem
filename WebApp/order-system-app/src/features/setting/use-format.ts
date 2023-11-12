import { useMemo } from "react";
import { useAppSelector } from "../../app/store";
import { selectLocale } from "./setting-slice";

function useFormat() {
  const locale = useAppSelector(selectLocale);

  return useMemo(() => {
    const currencyFormat = new Intl.NumberFormat(locale, {
      style: "currency",
      currency: "VND",
    });

    const numberFormat = new Intl.NumberFormat(locale, {
      maximumSignificantDigits: 4,
    });

    return {
      currency: currencyFormat.format,
      number: numberFormat.format,
    };
  }, [locale]);
}

export default useFormat;
