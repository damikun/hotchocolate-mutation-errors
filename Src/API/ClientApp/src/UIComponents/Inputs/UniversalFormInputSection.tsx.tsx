import StayledFormInput from "./StayledFormInput";
import Section from "../../UIComponents/Section/Section";
import SectionTitle from "../../UIComponents/Section/SectionTitle";
import { HTMLInputTypeAttribute } from "react";

export type UniversalFormInputSectionProps = {
  isInFlight: boolean;
  error: string | undefined;
  value: string | null;
  onChange: any;
  name: string;
  form_id: string;
  placeholder: string;
  reserveValiadtionErrorSpace?: boolean;
  className?: string;
  disabled?: boolean;
  focusOnMount?: boolean;
  type?:HTMLInputTypeAttribute | undefined;
};

export default function UniversalFormInputSection({
  isInFlight,
  error,
  value,
  onChange,
  name,
  placeholder,
  reserveValiadtionErrorSpace = false,
  form_id,
  focusOnMount,
  className,
  disabled,
  type
}: UniversalFormInputSectionProps) {
  return (
    <Section className={className}>
      <SectionTitle name={name} />
      <div className="flex flex-col">
        <StayledFormInput
          reserveValidationErrorSpace={reserveValiadtionErrorSpace}
          disabled={disabled}
          variant="inrow"
          isError={error}
          id={form_id}
          name={form_id}
          type={type}
          value={value ? value : ""}
          placeholder={placeholder}
          onChange={onChange}
          tabIndex={0}
          focuseOnMount={focusOnMount}
        />
      </div>
    </Section>
  );
}
