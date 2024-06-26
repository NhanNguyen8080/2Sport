import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faMagnifyingGlass,
  faBell,
  faCaretDown,
} from "@fortawesome/free-solid-svg-icons";
import { Menu, MenuHandler, MenuList, MenuItem, Button } from "@material-tailwind/react";
import { useTranslation } from "react-i18next";


function HeaderStaff() {
  const { t } = useTranslation();
  const [open, setOpen] = useState(false);

  const handleMenuToggle = () => {
    setOpen(!open);
  };

  return (
    <div className="flex justify-end items-center pt-10 pb-10 space-x-4">
      <div className="flex items-center pr-5">
        <FontAwesomeIcon icon={faMagnifyingGlass} className="text-xl" />
      </div>
      <div className="flex items-center pr-5">
        <FontAwesomeIcon icon={faBell} className="text-xl" />
      </div>
      <div className="w-40 rounded font-bold pr-5 bg-transparent">
        <Menu open={open} handler={setOpen}>
          <MenuHandler>
            <Button
              className="w-24 h-10 text-black bg-transparent flex items-center justify-center"
              onClick={handleMenuToggle}
            >
              {t("header_staff.staff")}
              <FontAwesomeIcon icon={faCaretDown} className="ml-2" />
            </Button>
          </MenuHandler>
          <MenuList>
            <MenuItem onClick={() => setOpen(false)}>{t("header_staff.log_out")}</MenuItem>
          </MenuList>
        </Menu>
      </div>
    </div>
  );
}

export default HeaderStaff;

