import {legacy_createStore as createStore, combineReducers, applyMiddleware} from 'redux';
import { thunk } from 'redux-thunk';

import userReducer from 'react';

let reducers = combineReducers({
    user: userReducer
});

let store = createStore(reducers, applyMiddleware(thunk));

export default store;