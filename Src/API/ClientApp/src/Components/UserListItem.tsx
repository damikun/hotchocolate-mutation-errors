import clsx from "clsx";
import { useCallback } from "react";
import {
  useFragment,
  useMutation,
} from "react-relay/hooks";
import { graphql } from "babel-plugin-relay/macro";
import { UserListItemFragment$key } from "./__generated__/UserListItemFragment.graphql";
import StayledPromtButton from "../UIComponents/Buttons/StayledPromtButton";
import { useToast } from "../UIComponents/Toast/ToastProvider";
import { HandleErrors } from "../Utils/ErrorHelper";
import UserDeleteModalContent from "./UserDeletePropt.tsx";
import { useUserContext } from "./Home";
import CapitalizedText from "../UIComponents/CapitalizedText/CapitalizedText";
import { UserListItemRemoveUserMutation } from "./__generated__/UserListItemRemoveUserMutation.graphql";

export const UserListItemFragment = graphql`
  fragment UserListItemFragment on GQL_User {
    id
    nickName
    age
  }
`;

type UserListItemProps = {
  dataRef: UserListItemFragment$key | null;
};

export default function UserListItem({
  dataRef,
}: UserListItemProps) {
  const entity = useFragment(UserListItemFragment, dataRef);

  const toast = useToast();

  const userContext = useUserContext();
  
  const [
    commitRemove,
    removeInFlight,
  ] = useMutation<UserListItemRemoveUserMutation>(graphql`
    mutation UserListItemRemoveUserMutation(
      $userId: ID!
      $connections: [ID!]!
    ) {
      removeUser(userId: $userId) {
        ... on RemoveUserPayload {
          errors {
            ... on IBaseError {
              message
            }
          }

          removedId @deleteEdge(connections: $connections)

        }
      }
    }
  `);

  
  const handleRemove = useCallback(() => {
    entity &&
      !removeInFlight &&
      entity.id &&
      commitRemove({
        variables: {
          userId: entity.id,
          connections: userContext?.connection_id ? [userContext.connection_id] : [],
        },

        onError(error) {
          toast?.pushError("Failed to process mutation");
          console.log(error);
        },

        onCompleted(data) {},
        updater(store, data) {
          HandleErrors(toast, data.removeUser?.errors);
        },
      });
  }, [toast, commitRemove, entity,removeInFlight,userContext?.connection_id ]);

  

  return (
    <div
      className={clsx(
        "flex p-3 md:px-5 first:rounded-t-md last:rounded-b-md",
        "items-center space-x-10 justify-between bg-white",
        "cursor-pointer hover:bg-gray-50"
      )}
    >
  
      <CapitalizedText className="font-semibold truncate break-all">
        {entity?.nickName}
      </CapitalizedText>
    
      <div className="flex space-x-2">
        <div className="flex w-20 justify-center">
          <StayledPromtButton
            promt_className="md:max-w-lg"
            promt_buttonTitle={"Delete"}
            promt_isProcessing={removeInFlight}
            promt_variant="error"
            promt_minWidth="min-w-24"
            promt_position="justify-start"
            promt_callback={handleRemove}
            promt_title="Delete User"
            button_size="normal"
            className="hover:text-red-500 text-gray-400 duration-300"
            promt_content={
              <UserDeleteModalContent userName={entity?.nickName} />
            }
            variant="primaryred"
          >
            Delete
          </StayledPromtButton>
        </div>
      </div>
    </div>
  );
}

