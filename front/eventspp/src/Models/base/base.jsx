import { Route, Routes, Navigate, useLocation, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

import { Container } from 'react-bootstrap';



function Base() {
    const location = useLocation();

    useEffect(() => {
        console.log('Navigating to:', location.pathname);
    }, [location]);

    return (
        <Container fluid>
            <Routes>
                <Route path="/" element={localStorage.getItem('token') ? <Navigate to="/" /> : <Navigate to="/login" />} />
            </Routes>
        </Container>
    );
}

export default Base;