import { eventsApi } from "../Api/eventsApi";

const GET_EVENTS = "GET_EVENTS";
const GET_REGISTERED = "GET_REGISTERED";

let initialEventState = {
    events: [],
    registered: []
}

const eventReducer = (state = initialEventState, action) => {
    let newState = {...state};
    switch(action.type){
        case GET_EVENTS:
            newState.events = action.events;
            return newState;
        case GET_REGISTERED:
            newState.registered = action.registered;
            return newState;
        default:
            return newState;
    }
}

export function eventsActionCreator(events){
    return {type: GET_EVENTS, events: events}
}

export function registeredActionCreator(registered){
    return {type: GET_REGISTERED, registered: registered}
}

export function getEventsThunkCreator(date, navigate) {
    return (dispatch) => {
        return eventsApi.getEvents(navigate, date)
            .then(response => {
                if(response !== null){
                    dispatch(eventsActionCreator(response.events));
                }
            })
    };
}

export function addEventThunkCreator(data, navigate) {
    return (dispatch) => {
        return eventsApi.addEvent(navigate, data)
    };
}

export function changeEventThunkCreator(data, navigate) {
    return (dispatch) => {
        return eventsApi.changeEvent(navigate, data)
    };
}

export function deleteEventThunkCreator(data, navigate, ) {
    return (dispatch) => {
        return eventsApi.deleteEvent(navigate, data)
    };
}

export function signUpThunkCreator(data, navigate, ) {
    console.log(data);
    return (dispatch) => {
        return eventsApi.signUp(navigate, data)
    };
}

export function unsignThunkCreator(data, navigate, ) {
    return (dispatch) => {
        return eventsApi.unsign(navigate, data)
    };
}

export function getRegisteredThunkCreator(date, navigate) {
    return (dispatch) => {
        return eventsApi.registeredList(navigate, date)
            .then(response => {
                if(response !== null){
                    dispatch(registeredActionCreator(response.students));
                }
            })
    };
}

export default eventReducer;