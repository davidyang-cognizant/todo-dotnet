import { Button, Table, Form, Container } from "react-bootstrap"
import Modal from 'react-modal'
import { useState } from "react";

const customStyles = {
    content: {
        padding: '50px',
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        backgroundColor: '#edaf87',
    },
};

Modal.setAppElement('#root');

export const TodoTable = (props) => {

    const [modalIsOpen, setIsOpen] = useState(false);
    const [taskToEdit, setTaskToEdit] = useState({}),
    onInput = ({target:{value}}) => setTaskToEdit({id:taskToEdit.id, task: value}),
    onFormSubmit = e => {
        e.preventDefault();
        props.handleEdit(e, taskToEdit)
        setTaskToEdit({})
        setIsOpen(false);
    };

    function openModal(e, task) {
        setTaskToEdit(task);
        setIsOpen(true);
    }

    function closeModal() {
        setIsOpen(false);
    }


    return (
        <Table hover>
            <thead>
                <tr>
                    <th>task</th>
                    <th>actions</th>
                </tr>
            </thead>
            <tbody>
                {props.todoList.map((item, index) => {
                    return (
                        <tr key={index}>
                            <td style={{ width: "100%" }} key={index}>{item.task}</td>
                            <td>
                                <div className="d-flex">
                                    <Button className="remove me-1" onClick={() => props.handleDelete(item.id)}>remove</Button>
                                    <Button className="edit" onClick={(e) => openModal(e, item)}>edit</Button>
                                </div>
                            </td>
                        </tr>
                    )
                })}
            </tbody>
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                style={customStyles}
                contentLabel="edit"
            >   
                <Container>
                    <p className="semibold">Edit the task</p>
                <Form onSubmit={onFormSubmit} className="d-flex">
                    <Form.Control onChange={onInput} value={taskToEdit.task}/>
                    <Button className="edit ms-1" type="submit">edit</Button>
                </Form>
                </Container>
                
            </Modal>
        </Table>
    )
};
