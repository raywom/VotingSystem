import React, { useState } from 'react';
import { ActivityChoice } from "../../models/activity";

interface MySelectInputProps {
    options: ActivityChoice[];
    placeholder: string;
    name: string;
    onChange: (selectedValue: string) => void; // Add onChange prop
}

const MyOwnSelect: React.FC<MySelectInputProps> = (props) => {
    const { options, placeholder, name, onChange } = props;
    const [selectedValue, setSelectedValue] = useState('');

    const handleSelectChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const value = event.target.value;
        setSelectedValue(value);
        onChange(value); // Call the onChange prop with the selected value
    };

    return (
        <select name={name} value={selectedValue} onChange={handleSelectChange}>
            <option value="" disabled>
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
