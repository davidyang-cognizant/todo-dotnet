import { Button, Form } from "react-bootstrap";
import { useState } from "react";

export const TodoForm = (props) => {
    const [newTask, setNewTask] = useState(""),
    onInput = ({target:{value}}) => setNewTask(value),
    onFormSubmit = e => {
        e.preventDefault();
        setNewTask("");
        props.handleSubmit(e, newTask)
    }

    return(
        <Form onSubmit={onFormSubmit} className="d-flex justify-content-center py-5">
            <Form.Control value={newTask} onChange={onInput} type="text" autoComplete="false" placeholder="become successful twitch streamer..." className="w-50"/>
            <Button className="ms-1 addTaskBtn" type="submit">add</Button>
        </Form>
    )
};
