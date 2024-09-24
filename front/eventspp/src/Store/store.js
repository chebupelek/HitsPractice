import {legacy_createStore as createStore, combineReducers, applyMiddleware} from 'redux';
import { thunk } from 'redux-thunk';

import userReducer from '../Reducers/UserReducer';
import companyReducer from '../Reducers/CompaniesReducer';

let reducers = combineReducers({
    user: userReducer,
    company: companyReducer
});

let store = createStore(reducers, applyMiddleware(thunk));

export default store;