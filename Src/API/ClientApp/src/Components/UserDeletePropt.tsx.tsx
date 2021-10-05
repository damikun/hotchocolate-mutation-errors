import clsx from "clsx";
import { ReactComponent as DeleteImage } from "../Images/remove.svg";
import CapitalizedText from "../UIComponents/CapitalizedText/CapitalizedText";

type UserDeleteModalContentProps = {
  userName?: string | null | undefined;
};

export default function UserDeleteModalContent({ userName }: UserDeleteModalContentProps) {
  return (
    <div className="flex flex-col w-full max-w-full space-y-5 mt-5 items-center justify-center">
      <div className="flex justify-center">
        <DeleteImage className="h-32" />
      </div>

      {userName && (
        <div className="truncate-1-lines break-all italic text-base">
          <CapitalizedText
            className={clsx(
            "flex items-center tracking-wide",
            "truncate-1-lines select-none"
        )}>
            {userName}
          </CapitalizedText>
        </div>
      )}

      <div className="flex justify-center space-x-2 text-gray-500">
        <p className="max-w-md text-center text-base font-normal">
          You are atempting to delete User. Are you shure about
          this action?
        </p>
      </div>
    </div>
  );
}
