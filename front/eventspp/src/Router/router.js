const base = "https://localhost:7099";
const routers = {
    bidAccess: `${base}/api/bids/access`,
    bidList: `${base}/api/bids/getList`,
    companiesList: `${base}/api/companies/list`,
    companiesName: `${base}/api/companies/names`,
    companiesAdd: `${base}/api/companies/create`,
    companiesRemove: `${base}/api/companies/delete`,
    eventList: `${base}/api/events/list`,
    eventCreate: `${base}/api/events/create`,
    eventChange: `${base}/api/events/change`,
    eventDelete: `${base}/api/events/delete`,
    eventSignup: `${base}/api/events/signup`,
    eventUnsign: `${base}/api/events/unsignup`,
    eventRegisteredlist: `${base}/api/events/registered`,
    userStudent: `${base}/api/users/registration/student`,
    userEmployee: `${base}/api/users/registration/employee`,
    userLogin: `${base}/api/users/login`,
    userLogout: `${base}/api/users/logout`,
    userRole: `${base}/api/users/getRole`
};
export default routers;