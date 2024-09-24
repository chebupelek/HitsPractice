import { loginApi } from "../Api/loginApi";
import { profileApi } from "../Api/profileApi";
import { registrationApi } from "../Api/registrationApi";
import { logoutApi } from "../Api/logout";

const LOGIN = "LOGIN";
const SET_ROLE = "SET_ROLE";
const LOGOUT = "LOGOUT";

let initialUserState = {
    role: 0,
    isAuth: localStorage.getItem('token') ? true : false
}

const userReducer = (state = initialUserState, action) => {
    let newState = {...state};
    switch(action.type){
        case LOGIN:
            newState.isAuth = true;
            return newState;
        case LOGOUT:
            newState.role = 0;
            newState.isAuth = false;
            return newState;
        case SET_ROLE:
            newState.isAuth = true;
            newState.role = action.role;
            return newState;
        default:
            return newState;
    }
}

export function loginHeaderActionCreator(){
    return {type: LOGIN}
}

export function setRoleActionCreator(role){
    return {type: SET_ROLE, role: role}
}

export function logoutActionCreator(){
    return {type: LOGOUT}
}

export function loginThunkCreator(data, navigate) {
    return (dispatch) => {
        localStorage.clear();
        return loginApi.login(data)
            .then(response => {
                if(response !== null){
                    dispatch(loginHeaderActionCreator());
                    navigate("/");
                }
            })
    };
}

export function studenRegistrationThunkCreator(data, navigate) {
    return (dispatch) => {
        localStorage.clear();
        return registrationApi.studentRegistration(data)
            .then(response => {
                if(response !== null){
                    dispatch(loginHeaderActionCreator());
                    navigate("/");
                }
            })
    };
}

export function employeeRegistrationThunkCreator(data, navigate) {
    return (dispatch) => {
        localStorage.clear();
        return registrationApi.employeeRegistration(data)
            .then(response => {
                if(response !== null){
                    navigate("/");
                }
            })
    };
}

export function setRoleThunkCreator(navigate) {
    return (dispatch) => {
        if (!localStorage.getItem('token')) {
            return null;
        }
        return profileApi.getRole()
            .then(response => {
                if(response !== null){
                    dispatch(setRoleActionCreator(response.role));
                }else{
                    localStorage.clear();
                    navigate("/login");
                    dispatch(logoutActionCreator());
                }
            })
    };
}

export function logoutThunkCreator(navigate) {
    return (dispatch) => {
        return logoutApi.logout()
            .then(response => {
                if(response !== null){
                    localStorage.clear();
                    dispatch(logoutActionCreator());
                    navigate("/login");
                }
            })
    };
}

export function logoutHeaderActionCreator(){
    return {type: LOGOUT}
}

export default userReducer;