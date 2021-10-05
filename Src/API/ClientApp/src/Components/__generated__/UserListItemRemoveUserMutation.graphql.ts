/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest } from "relay-runtime";
export type UserListItemRemoveUserMutationVariables = {
    userId: string;
    connections: Array<string>;
};
export type UserListItemRemoveUserMutationResponse = {
    readonly removeUser: {
        readonly errors: ReadonlyArray<{
            readonly message?: string | null;
        } | null> | null;
        readonly removedId: string | null;
    } | null;
};
export type UserListItemRemoveUserMutation = {
    readonly response: UserListItemRemoveUserMutationResponse;
    readonly variables: UserListItemRemoveUserMutationVariables;
};



/*
mutation UserListItemRemoveUserMutation(
  $userId: ID!
) {
  removeUser(userId: $userId) {
    errors {
      __typename
      ... on IBaseError {
        __isIBaseError: __typename
        message
      }
    }
    removedId
  }
}
*/

const node: ConcreteRequest = (function(){
var v0 = {
  "defaultValue": null,
  "kind": "LocalArgument",
  "name": "connections"
},
v1 = {
  "defaultValue": null,
  "kind": "LocalArgument",
  "name": "userId"
},
v2 = [
  {
    "kind": "Variable",
    "name": "userId",
    "variableName": "userId"
  }
],
v3 = {
  "kind": "InlineFragment",
  "selections": [
    {
      "alias": null,
      "args": null,
      "kind": "ScalarField",
      "name": "message",
      "storageKey": null
    }
  ],
  "type": "IBaseError",
  "abstractKey": "__isIBaseError"
},
v4 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "removedId",
  "storageKey": null
};
return {
  "fragment": {
    "argumentDefinitions": [
      (v0/*: any*/),
      (v1/*: any*/)
    ],
    "kind": "Fragment",
    "metadata": null,
    "name": "UserListItemRemoveUserMutation",
    "selections": [
      {
        "alias": null,
        "args": (v2/*: any*/),
        "concreteType": "RemoveUserPayload",
        "kind": "LinkedField",
        "name": "removeUser",
        "plural": false,
        "selections": [
          {
            "alias": null,
            "args": null,
            "concreteType": null,
            "kind": "LinkedField",
            "name": "errors",
            "plural": true,
            "selections": [
              (v3/*: any*/)
            ],
            "storageKey": null
          },
          (v4/*: any*/)
        ],
        "storageKey": null
      }
    ],
    "type": "Mutation",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [
      (v1/*: any*/),
      (v0/*: any*/)
    ],
    "kind": "Operation",
    "name": "UserListItemRemoveUserMutation",
    "selections": [
      {
        "alias": null,
        "args": (v2/*: any*/),
        "concreteType": "RemoveUserPayload",
        "kind": "LinkedField",
        "name": "removeUser",
        "plural": false,
        "selections": [
          {
            "alias": null,
            "args": null,
            "concreteType": null,
            "kind": "LinkedField",
            "name": "errors",
            "plural": true,
            "selections": [
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "__typename",
                "storageKey": null
              },
              (v3/*: any*/)
            ],
            "storageKey": null
          },
          (v4/*: any*/),
          {
            "alias": null,
            "args": null,
            "filters": null,
            "handle": "deleteEdge",
            "key": "",
            "kind": "ScalarHandle",
            "name": "removedId",
            "handleArgs": [
              {
                "kind": "Variable",
                "name": "connections",
                "variableName": "connections"
              }
            ]
          }
        ],
        "storageKey": null
      }
    ]
  },
  "params": {
    "cacheID": "251b39ac3dcadd93536ba641686ce835",
    "id": null,
    "metadata": {},
    "name": "UserListItemRemoveUserMutation",
    "operationKind": "mutation",
    "text": "mutation UserListItemRemoveUserMutation(\n  $userId: ID!\n) {\n  removeUser(userId: $userId) {\n    errors {\n      __typename\n      ... on IBaseError {\n        __isIBaseError: __typename\n        message\n      }\n    }\n    removedId\n  }\n}\n"
  }
};
})();
(node as any).hash = '314b90668b00fa0f7d3c43e381f916f8';
export default node;
