import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faGauge } from "@fortawesome/free-solid-svg-icons";
import { useTranslation } from "react-i18next";


export default function SidebarStaff() {
  const { t } = useTranslation();
  return (
    <div className="flex flex-col items-center justify-center py-5 px-5">
      <div>
        <img
          src="/assets/images/Logo.png"
          alt="2Sport"
          className="max-w-md max-h-16 pr-3"
        />
      </div>
      <div className="mb-4">
        <h1 className="text-2xl">{t("sidebar_staff.menu")}</h1>
      </div>
      <ul>
        <li className="flex items-center text-xl">
          <FontAwesomeIcon icon={faGauge} className="pr-3" /> {t("sidebar_staff.dashboard")}
        </li>
      </ul>
    </div>
  );
}
