import clsx from "clsx";
import React, { SuspenseList, useMemo } from "react";
import { Suspense, useContext, useState } from "react";
import ContainerSpinner from "../UIComponents/Spinner/ContainerSpinner";
import CreateNewUser from "./CreateNewUser"
import UserList from "./UserList";

export type UserContextType = {
  connection_id: string;
  setConnectionId: (connection: string) => void;
};

export const UserContext = React.createContext<
UserContextType | undefined
>(undefined);

export const useUserContext = () => useContext(UserContext);

export default function Home() {

  const [state, setState] = useState("");
  
  const ctx = useMemo(() => {
    return {
      connection_id: state,
      setConnectionId: (connection: string) => setState(connection),
    };
  }, [state, setState]);


  return <div
    className={clsx(
      "flex h-full w-full",
      "text-xs md:text-sm max-h-full space-y-1",
      "p-5"
    )}
  >
    <div className="flex w-full max-w-5xl mx-auto mt-14">
      <div
        className={clsx(
          "flex flex-col md:flex-row space-x-0 md:space-x-5",
          "space-y-2 md:space-y-0 w-full h-full xl:space-x-10"
        )}
      >
        <div className="flex-1">
            <UserContext.Provider value={ctx}>
              <div className="flex h-full flex-col space-y-2 justify-center content-center ">
                <SuspenseList revealOrder="together">
                  <Suspense fallback={<></>}>
                    <CreateNewUser />
                  </Suspense>
                  <Suspense fallback={<ContainerSpinner />}>
                    <UserList />
                  </Suspense>
                </SuspenseList>
              </div>
            </UserContext.Provider>
        </div>
      </div>
    </div>
  </div>
  }
  