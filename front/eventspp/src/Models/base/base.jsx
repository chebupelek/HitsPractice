import { Route, Routes, Navigate, useLocation, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

import { Container } from 'react-bootstrap';

import Login from '../login/login';
import RegisterStudent from '../register/studentRegister';

function Base() {
    const location = useLocation();

    useEffect(() => {
        console.log('Navigating to:', location.pathname);
    }, [location]);

    return (
        <Container fluid>
            <Routes>
                <Route path="/" element={localStorage.getItem('token') ? <Navigate to="/login" /> : <Navigate to="/login" />} />
                <Route path="/login" element={<Login />}/>
                <Route path="/register/student" element={<RegisterStudent />}/>
            </Routes>
        </Container>
    );
}

export default Base;