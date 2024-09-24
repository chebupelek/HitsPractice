import React from 'react';
import { Modal, Button, ListGroup } from 'react-bootstrap';

const RegisteredListModal = ({ isOpen, onClose, registeredList }) => {
    return (
        <Modal show={isOpen} onHide={onClose} size="lg">
            <Modal.Header closeButton>
                <Modal.Title>Список зарегистрированных</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <ListGroup>
                    {registeredList.length > 0 ? (
                        registeredList.map((student, index) => (
                            <ListGroup.Item key={index}>
                                <strong>{student.name}</strong> - {student.email}
                            </ListGroup.Item>
                        ))
                    ) : (
                        <ListGroup.Item>Нет зарегистрированных</ListGroup.Item>
                    )}
                </ListGroup>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onClose}>Закрыть</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default RegisteredListModal;