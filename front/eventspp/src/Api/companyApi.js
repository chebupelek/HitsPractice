import routers from "../Router/router";

function getCompanies() {
    return fetch(routers.companiesList, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}`
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

export const companiesApi = {
    getCompanies : getCompanies,
    getNames : getNames
}