import React, { useState, useEffect } from 'react';
import { Alert, Button, Form, ListGroup, Modal } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { getBidsListThunkCreator, accessBidThunkCreator } from '../../Reducers/BidsReducer';
import { useNavigate } from 'react-router-dom';

const Bids = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const bids = useSelector((state) => state.bids.bidsList);

    useEffect(() => {
        dispatch(getBidsListThunkCreator(navigate)); 
    }, [bids, dispatch, navigate]);

    const handleAccess = (id) => {
        dispatch(accessBidThunkCreator({bidId: id, isAccepted: true}, navigate));
    };

    const handleDeny = (id) => {
        dispatch(accessBidThunkCreator({bidId: id, isAccepted: false}, navigate));
    };

    const dateFormat = (date) => {
        return new Date(date).toLocaleString();
    }

    return ( 
        <div className="container mt-4">
            <div className="mt-4">
                <ListGroup>
                    {bids.map((bid) => (
                        <ListGroup.Item key={bid.id} className="d-flex justify-content-between align-items-center">
                            <div>
                                Имя: {bid.fullName}<br />
                                Почта: {bid.email}<br />
                                Компания: {bid.company}<br />
                                Дата заявки: {dateFormat(bid.createTime)}<br />
                            </div>
                            <Button variant="success" onClick={() => handleAccess(bid.id)}>Подтвердить</Button>
                            <Button variant="danger" onClick={() => handleDeny(bid.id)}>Отказать</Button>
                        </ListGroup.Item>
                    ))}
                </ListGroup>
            </div>
        </div>
    ); 
};

export default Bids;