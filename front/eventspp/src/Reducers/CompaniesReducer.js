import {companiesApi} from '../Api/companyApi';

const SET_NAMES = "SET_NAMES";
const SET_COMPANIES = "SET_COMPANIES";

let initialCompanyState = {
    names: [],
    companies: []
}

const companyReducer = (state = initialCompanyState, action) => {
    let newState = {...state};
    switch(action.type){
        case SET_NAMES:
            newState.names = action.names;
            return newState;
        case SET_COMPANIES:
            newState.companies = action.companies;
            return newState;
        default:
            return newState;
    }
}

export function setNamesActionCreator(names){
    return {type: SET_NAMES, names: names}
}

export function setCompaniesActionCreator(companies){
    return {type: SET_COMPANIES, companies: companies}
}

export function getNamesThunkCreator() {
    return (dispatch) => {
        return companiesApi.getNames()
            .then(response => {
                if(response !== null){
                    dispatch(setNamesActionCreator(response.companies));
                }
            })
    };
}

export function getCompaniesThunkCreator(navigate) {
    return (dispatch) => {
        return companiesApi.getCompanies(navigate)
            .then(response => {
                if(response !== null){
                    dispatch(setCompaniesActionCreator(response.companies));
                }
            })
    };
}

export function addCompanyThunkCreator(navigate, data) {
    return (dispatch) => {
        return companiesApi.addCompany(navigate, data)
            .then(response => {
                if(response !== null){
                    dispatch(getCompaniesThunkCreator(navigate));
                }
            });
    };
}

export function removeCompanyThunkCreator(navigate, data) {
    return (dispatch) => {
        return companiesApi.removeCompany(navigate, data)
            .then(response => {
                if(response !== null){
                    dispatch(getCompaniesThunkCreator(navigate));
                }
            });
    };
}

export default companyReducer;