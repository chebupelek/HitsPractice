import {legacy_createStore as createStore, combineReducers, applyMiddleware} from 'redux';
import { thunk } from 'redux-thunk';

import userReducer from '../Reducers/UserReducer';
import companyReducer from '../Reducers/CompaniesReducer';
import bidsReducer from '../Reducers/BidsReducer';
import eventReducer from '../Reducers/EventsReducer';

let reducers = combineReducers({
    user: userReducer,
    company: companyReducer,
    bids: bidsReducer,
    events: eventReducer
});

let store = createStore(reducers, applyMiddleware(thunk));

export default store;