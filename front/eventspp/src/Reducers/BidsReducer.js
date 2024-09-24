import { bidsApi } from '../Api/bidsApi';

const BIDS_LIST = "BIDS_LIST";

let initialBidsState = {
    bidsList: []
}

const bidsReducer = (state = initialBidsState, action) => {
    let newState = {...state};
    switch(action.type){
        case BIDS_LIST:
            newState.bidsList = action.bidsList;
            return newState;
        default:
            return newState;
    }
}

export function getBidsListActionCreator(bidsList){
    return {type: BIDS_LIST, bidsList: bidsList}
}

export function getBidsListThunkCreator(navigate) {
    return (dispatch) => {
        return bidsApi.getBidList(navigate)
            .then(response => {
                if(response !== null){
                    dispatch(getBidsListActionCreator(response.bidsList));
                }
            })
    };
}

export function accessBidThunkCreator(data, navigate) {
    return (dispatch) => {
        return bidsApi.accessBid(navigate, data)
            .then(response => {
                dispatch(getBidsListThunkCreator(navigate));
            })
    };
}

export default bidsReducer;