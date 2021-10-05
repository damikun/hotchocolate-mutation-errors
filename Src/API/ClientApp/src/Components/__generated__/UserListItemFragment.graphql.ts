/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ReaderFragment } from "relay-runtime";
import { FragmentRefs } from "relay-runtime";
export type UserListItemFragment = {
    readonly id: string;
    readonly nickName: string | null;
    readonly age: number;
    readonly " $refType": "UserListItemFragment";
};
export type UserListItemFragment$data = UserListItemFragment;
export type UserListItemFragment$key = {
    readonly " $data"?: UserListItemFragment$data;
    readonly " $fragmentRefs": FragmentRefs<"UserListItemFragment">;
};



const node: ReaderFragment = {
  "argumentDefinitions": [],
  "kind": "Fragment",
  "metadata": null,
  "name": "UserListItemFragment",
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
  "type": "GQL_User",
  "abstractKey": null
};
(node as any).hash = '9393282d178c6e5db54c808871315300';
export default node;
