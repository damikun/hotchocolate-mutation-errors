/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest } from "relay-runtime";
export type CreateUserInput = {
    nickName?: string | null;
    age: number;
};
export type CreateNewUserMutationVariables = {
    request?: CreateUserInput | null;
    connections: Array<string>;
};
export type CreateNewUserMutationResponse = {
    readonly createUser: {
        readonly user: {
            readonly id: string;
            readonly nickName: string | null;
            readonly age: number;
        } | null;
        readonly errors: ReadonlyArray<{
            readonly message?: string | null;
        } | null> | null;
    } | null;
};
export type CreateNewUserMutation = {
    readonly response: CreateNewUserMutationResponse;
    readonly variables: CreateNewUserMutationVariables;
};



/*
mutation CreateNewUserMutation(
  $request: CreateUserInput
) {
  createUser(request: $request) {
    user {
      id
      nickName
      age
    }
    errors {
      __typename
      ... on IBaseError {
        __isIBaseError: __typename
        message
      }
    }
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
  "name": "request"
},
v2 = [
  {
    "kind": "Variable",
    "name": "request",
    "variableName": "request"
  }
],
v3 = {
  "alias": null,
  "args": null,
  "concreteType": "GQL_User",
  "kind": "LinkedField",
  "name": "user",
  "plural": false,
  "selections": [
    {
      "alias": null,
      "args": null,
      "kind": "ScalarField",
      "name": "id",
      "storageKey": null
    },
    {
      "alias": null,
      "args": null,
      "kind": "ScalarField",
      "name": "nickName",
      "storageKey": null
    },
    {
      "alias": null,
      "args": null,
      "kind": "ScalarField",
      "name": "age",
      "storageKey": null
    }
  ],
  "storageKey": null
},
v4 = {
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
};
return {
  "fragment": {
    "argumentDefinitions": [
      (v0/*: any*/),
      (v1/*: any*/)
    ],
    "kind": "Fragment",
    "metadata": null,
    "name": "CreateNewUserMutation",
    "selections": [
      {
        "alias": null,
        "args": (v2/*: any*/),
        "concreteType": "CreateUserPayload",
        "kind": "LinkedField",
        "name": "createUser",
        "plural": false,
        "selections": [
          (v3/*: any*/),
          {
            "alias": null,
            "args": null,
            "concreteType": null,
            "kind": "LinkedField",
            "name": "errors",
            "plural": true,
            "selections": [
              (v4/*: any*/)
            ],
            "storageKey": null
          }
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
    "name": "CreateNewUserMutation",
    "selections": [
      {
        "alias": null,
        "args": (v2/*: any*/),
        "concreteType": "CreateUserPayload",
        "kind": "LinkedField",
        "name": "createUser",
        "plural": false,
        "selections": [
          (v3/*: any*/),
          {
            "alias": null,
            "args": null,
            "filters": null,
            "handle": "prependNode",
            "key": "",
            "kind": "LinkedHandle",
            "name": "user",
            "handleArgs": [
              {
                "kind": "Variable",
                "name": "connections",
                "variableName": "connections"
              },
              {
                "kind": "Literal",
                "name": "edgeTypeName",
                "value": "GQL_User"
              }
            ]
          },
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
              (v4/*: any*/)
            ],
            "storageKey": null
          }
        ],
        "storageKey": null
      }
    ]
  },
  "params": {
    "cacheID": "a389b48ecf822f4ce5566fc6b3e7002b",
    "id": null,
    "metadata": {},
    "name": "CreateNewUserMutation",
    "operationKind": "mutation",
    "text": "mutation CreateNewUserMutation(\n  $request: CreateUserInput\n) {\n  createUser(request: $request) {\n    user {\n      id\n      nickName\n      age\n    }\n    errors {\n      __typename\n      ... on IBaseError {\n        __isIBaseError: __typename\n        message\n      }\n    }\n  }\n}\n"
  }
};
})();
(node as any).hash = 'ca39090e657037d29c9aef696649b34a';
export default node;
