import React from 'react';
import {ActivityChoice} from "../../models/activity";

interface MySelectInputProps {
    options: ActivityChoice[];
    placeholder: string;
    name: string;
}

const MyOwnSelect: React.FC<MySelectInputProps> = (props) => {
    const { options, placeholder, name } = props;

    return (
        <select name={name}>
            <option value="" disabled selected>
                {placeholder}
            </option>
            {options.map((choice) => (
                <option key={choice.id} value={choice.id}>
                    {choice.title}
                </option>
            ))}
        </select>
    );
};

export default MyOwnSelect;
