import clsx from "clsx";
import React, { useEffect } from "react";
import {
  useLazyLoadQuery,
  usePaginationFragment,
} from "react-relay/hooks";
import { graphql } from "babel-plugin-relay/macro";
import { UserListRefetchQuery } from "./__generated__/UserListRefetchQuery.graphql";
import { UserListFragment_users$key } from "./__generated__/UserListFragment_users.graphql";
import { UserListQuery } from "./__generated__/UserListQuery.graphql";
import StayledInfinityScrollContainer from "../UIComponents/ScrollContainter/StayledInfinityScrollContainer";
import UserListItem from "./UserListItem";
import { useUserContext } from "./Home";

const UserListQueryTag = graphql`
  query UserListQuery {
    ...UserListFragment_users
      @arguments(first: 20, after: null)

    serverDateTime
  }
`;

export const UserListFragment = graphql`
  fragment UserListFragment_users on Query
  @argumentDefinitions(
    first: { type: Int }
    after: { type: String }
  )
  @refetchable(queryName: "UserListRefetchQuery") {
    users(first: $first, after: $after)
      @connection(key: "UserListConnection_users") {
      __id
      pageInfo {
        hasPreviousPage
        hasNextPage
        startCursor
        endCursor
      }
      edges {
        cursor
        node {
          id
          ...UserListItemFragment
        }
      }
    }
  }
`;

export default React.memo(UserList);

function UserList() {

  const data = useLazyLoadQuery<UserListQuery>(
    UserListQueryTag,
    {},
    {
      fetchPolicy: "store-and-network",
    }
  );

  const userContext = useUserContext();

  const pagination = usePaginationFragment<
    UserListRefetchQuery,
    UserListFragment_users$key
  >(UserListFragment, data);

  useEffect(() => {
    userContext?.setConnectionId(
      pagination.data?.users?.__id
        ? pagination.data?.users?.__id
        : ""
    );
  }, [userContext, pagination, pagination.data, pagination.data?.users?.__id]);

  function HandleScrollEnd() {
    pagination.hasNext && !pagination.isLoadingNext && pagination.loadNext(20);
  }

  const isEmpty = pagination.data.users?.edges
    ? pagination.data.users?.edges.length <= 0
    : true;

  return (
    <>
      <div
        className={clsx(
          "flex w-full h-full rounded-b-md max-h-full overflow-hidden",
          "relative max-w-full overflow-y-scroll scrollbarwidth",
          "scrollbarhide scrollbarhide2 items-center justify-center"
        )}
      >
        <div className="absolute h-full w-full align-middle mx-auto">
          <div className={clsx(
            "w-full h-full relative max-w-full",
            "flex-col items-center justify-center")}>
            <div className="flex h-full flex-col max-w-3xl space-y-2 mx-auto ">
              <Header />

              <StayledInfinityScrollContainer
                isLoadingMore={pagination.isLoadingNext}
                isLoading={false}
                bgcolor="bg-transparent"
                onEnd={HandleScrollEnd}
                isEmptyMessage={"No HookRecords awailable"}
                isEmpty={isEmpty}
                divide
              >
                {pagination?.data?.users?.edges?.map((entity) => {
                  return entity.node ? (
                    <UserListItem
                      key={entity.node.id}
                      dataRef={entity.node}
                    />
                  ) : (
                    <></>
                  );
                })}
              </StayledInfinityScrollContainer>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

///////////////////////////////////////////
///////////////////////////////////////////

function Header() {
  return (
    <div className="flex justify-between flex-nowrap space-x-2">
      <div className="flex font-bold text-gray-800 text-md text-lg px-1 h-8">
        User List
      </div>
    </div>
  );
}
