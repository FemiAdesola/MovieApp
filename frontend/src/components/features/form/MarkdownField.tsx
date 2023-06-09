import React from 'react';
import { MarkdownFieldProps } from '../../types/field';
import { Field, useFormikContext } from 'formik';
import ReactMarkdown from 'react-markdown';

const MarkdownField = (props: MarkdownFieldProps) => {
    const { values } = useFormikContext<any>();
    return (
        <div className="mb-3 form-markdown">
            <div>
                <label>{props.displayName}</label>
                <div>
                    <Field name={props.field} as="textarea" className="form-textarea" />
                </div>
            </div>
            <div>
                <label>{props.displayName} (preview):</label>
                <div className="markdown-container">
                    <ReactMarkdown>{values[props.field]}</ReactMarkdown>
                </div>
            </div>
        </div>
    );
};

export default MarkdownField;