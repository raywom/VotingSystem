import React, { useState } from 'react';
import { useField, useFormikContext } from 'formik';
import { Form, Label, Select, Button, Input } from 'semantic-ui-react';

interface Option {
    title: string;
}

interface Props {
    placeholder: string;
    name: string;
    label?: string;
}

export default function MyOwnSelectInput(props: Props) {
    const [field, meta, helpers] = useField(props.name);
    const [options, setOptions] = useState<Option[]>([]); // State to store the options
    const { setFieldValue } = useFormikContext(); // Form's setFieldValue function

    const handleOptionChange = (index: number, value: string) => {
        const updatedOptions = [...options];
        updatedOptions[index].title = value;
        setOptions(updatedOptions);
        setFieldValue(props.name, updatedOptions);
    };

    const addOption = () => {
        const newOption: Option = {
            title: ''
        };
        setOptions([...options, newOption]);
    };

    const removeOption = (index: number) => {
        const updatedOptions = [...options];
        updatedOptions.splice(index, 1);
        setOptions(updatedOptions);
        setFieldValue(props.name, updatedOptions);
    };

    return (
        <Form.Field error={meta.touched && !!meta.error}>
            <label>{props.label}</label>
            {options.map((option, index) => (
                <div key={index}>
                    <Input
                        placeholder="Title"
                        title={option.title}
                        onChange={(e) => handleOptionChange(index, e.target.value)}
                    />
                    <Button type={"button"} onClick={() => removeOption(index)}>Remove</Button>
                </div>
            ))}
            <Button onClick={addOption} type={"button"}>Add Option</Button>
            <Select
                clearable
                options={options}
                value={field.value || null}
                onChange={(e, d) => helpers.setValue(d.value)}
                onBlur={() => helpers.setTouched(true)}
                placeholder={props.placeholder}
            />
            {meta.touched && meta.error ? (
                <Label basic color="red">
                    {meta.error}
                </Label>
            ) : null}
        </Form.Field>
    );
}