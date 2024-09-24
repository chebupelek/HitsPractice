import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';

import Header from "./Models/header/header";
import Base from './Models/base/base';
import store from "./Store/store";
import { Container } from 'react-bootstrap';

function App() {
  return (
    <BrowserRouter basename=''>
      <Provider store={store}>
        <Container >
        <Header/>
        <Base/>
        </Container>
      </Provider>
    </BrowserRouter>
  );
}

export default App;
