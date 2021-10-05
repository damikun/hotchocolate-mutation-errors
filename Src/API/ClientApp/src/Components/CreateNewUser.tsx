import { useMutation } from "react-relay";
import StayledButton from "../UIComponents/Buttons/StayledButton"
import { graphql } from "babel-plugin-relay/macro";
import { useCallback } from "react";
import { HandleErrors } from "../Utils/ErrorHelper";
import { useToast } from "../UIComponents/Toast/ToastProvider";
import { CreateNewUserMutation } from "./__generated__/CreateNewUserMutation.graphql";
import { useFormik } from "formik";
import clsx from "clsx";
import UniversalFormInputSection from "../UIComponents/Inputs/UniversalFormInputSection.tsx"
import { generateErrors, is } from "../Utils/Validation";
import { useUserContext } from "./Home";

export default function CreateNewUser() {
    const toast = useToast();

    const [
    commit_create,
    isInFlight,
    ] = useMutation<CreateNewUserMutation>(graphql`
    mutation CreateNewUserMutation(
        $request: CreateUserInput
        $connections: [ID!]!
        ) {
        createUser(request: $request) {

            user @prependNode(
              connections: $connections
              edgeTypeName: "GQL_User"
            ){
                id
                nickName
                age
            }
            ... on CreateUserPayload {
                errors {
                    ... on IBaseError {
                        message
                    }

                    # ...on ValidationError{
                    #     fieldName
                    #     message
                    # }
                    # ...on InternalServerError{
                    #     message
                    # }
                    # ... on UnAuthorised{
                    #     message
                    # }
                }
            }
        }
    }
    `);

    const userContext = useUserContext();

    const handleCreateUser = useCallback((values: {
        nickname: string;
        age: number;
    }) => {

        if(!isInFlight){

            commit_create({
                variables: {
                    request:{
                        nickName: values.nickname,
                        age: values.age
                    },
                    connections: userContext?.connection_id
                    ? [userContext.connection_id]
                    : [],
                },
      
                onError(error) {
                  toast?.pushError("Failed to process mutation");
                  console.log(error);
                },
      
                onCompleted(response) {},

                optimisticUpdater(store){},
      
                updater(store, response) {
                  HandleErrors(toast, response.createUser?.errors);
                  if (response.createUser?.errors?.length === 0) {
                    // handle success
                  }else{
                    // handle errors
                  }
                },
              });
        }

    }, [commit_create,isInFlight,toast,userContext?.connection_id])

    
    const formik = useFormik({
        initialValues: {
            nickname: "",
            age: 0,
        },
        onSubmit: (values) => {
            !isInFlight && handleCreateUser(values)
        },
        validate: (values) => {
            return generateErrors(values, {
                nickname: [
                is.required(),
                is.minLength(2),
              ],
              age: [
                is.required(),
                is.match(() => {
                    return values.age > 0 && values.age < 100 ;
                  }, "Must be valid age"),
              ],
            });
          },
          
          validateOnChange: false,
    });
    
    return (
        <div className="flex w-full justify-center content-center ">
            <div className="flex flex-col space-y-5 p-5">
                <form
                onSubmit={formik.handleSubmit}
                className="flex flex-col max-w-3xl space-y-2">
                    <div
                        className={clsx(
                        "flex flex-row border rounded-md space-x-2",
                        "justify-center bg-white shadow-sm items-center"
                    )}>
                        <div className="flex w-full flex-row space-x-2 px-3 md:px-5">
                            <UniversalFormInputSection
                            reserveValiadtionErrorSpace
                            isInFlight={isInFlight}
                            error={formik.errors.nickname}
                            value={formik.values.nickname}
                            onChange={formik.handleChange}
                            name="nickname"
                            placeholder="Nick Name"
                            form_id="nickname"/>

                            <UniversalFormInputSection
                            reserveValiadtionErrorSpace
                            isInFlight={isInFlight}
                            error={formik.errors.age}
                            value={formik.values.age.toString()}
                            onChange={formik.handleChange}
                            name="age"
                            type="number"
                            placeholder="Age"
                            form_id="age"/> 
                        </div>

                        <div className="flex h-full bg-gray-200 md:px-2 border-l">
                            <div className={clsx(
                                "flex w-44 justify-center",
                                "items-center")}>
                                <StayledButton
                                    isloading={isInFlight}
                                    variant="secondaryblue"
                                    size="large"
                                    type="submit">
                                        Create
                                </StayledButton>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    )
  }