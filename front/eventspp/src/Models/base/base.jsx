import { Route, Routes, Navigate, useLocation, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

import { Container } from 'react-bootstrap';

import Login from '../login/login';
import RegisterStudent from '../register/studentRegister';
import RegisterEmployee from '../register/employeeRegister';

function Base() {
    const location = useLocation();

    useEffect(() => {
        console.log('Navigating to:', location.pathname);
    }, [location]);

    return (
        <Container fluid>
            <Routes>
                <Route path="/" element={localStorage.getItem('token') ? <Navigate to="/register/student" /> : <Navigate to="/login" />} />
                <Route path="/login" element={<Login />}/>
                <Route path="/register/student" element={<RegisterStudent />}/>
                <Route path="/register/company" element={<RegisterEmployee />}/>
            </Routes>
        </Container>
    );
}

export default Base;