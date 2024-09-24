import routers from "../Router/router";

function getCompanies(navigate) {
    return fetch(routers.companiesList, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        }
    }).then(response => {
        if(!response.ok){
            if (response.status === 401) {
                localStorage.clear();
                navigate("/login");
                return null;
            } else if (response.status === 400) {
                alert('Invalid arguments for filtration/pagination');
                return null;
            } else if (response.status === 500) {
                alert('Internal Server Error');
                return null;
            } else {
                alert(`HTTP error! Status: ${response.status}`);
                return null;
            }
        }
        return response.json();
    }).then(data => {
        return data;
    }).catch(error => {
        console.log(error);
        return null;
    });
}

function getNames() {
    return fetch(routers.companiesName, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(response => {
        if(!response.ok){
            if (response.status === 400) {
                alert('Invalid arguments for filtration/pagination');
                return null;
            } else if (response.status === 500) {
                alert('Internal Server Error');
                return null;
            } else {
                alert(`HTTP error! Status: ${response.status}`);
                return null;
            }
        }
        return response.json();
    }).then(data => {
        return data;
    }).catch(error => {
        console.log(error);
        return null;
    });
}

function addCompany(navigate, data){
    return fetch(routers.companiesAdd, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify(data)
    }).then(response => {
        if(!response.ok){
            if (response.status === 401) {
                localStorage.clear();
                navigate("/login");
                return null;
            } else if (response.status === 404) {
                alert('Not Found');
                return null;
            } else if (response.status === 500) {
                alert('Internal Server Error');
                return null;
            } else {
                alert(`HTTP error! Status: ${response.status}`);
                return null;
            }
        }
        return true;
    }).then(data =>{
        return data;
    }).catch(error=>{
        console.log(error.message);
        return null;
    });
}

function removeCompany(navigate, data){
    return fetch(routers.companiesRemove, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}`
        },
        body: JSON.stringify(data)
    }).then(response => {
        if(!response.ok){
            if (response.status === 401) {
                localStorage.clear();
                navigate("/login");
                return null;
            } else if (response.status === 404) {
                alert('Not Found');
                return null;
            } else if (response.status === 500) {
                alert('Internal Server Error');
                return null;
            } else {
                alert(`HTTP error! Status: ${response.status}`);
                return null;
            }
        }
        return true;
    }).then(data =>{
        return data;
    }).catch(error=>{
        console.log(error.message);
        return null;
    });
}

export const companiesApi = {
    getCompanies : getCompanies,
    getNames : getNames,
    addCompany : addCompany,
    removeCompany : removeCompany
}