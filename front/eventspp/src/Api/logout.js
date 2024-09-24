import routers from "../Router/router";

function logout(navigate) {
    return fetch(routers.userLogout, {
        method: "POST",
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
            } else if (response.status === 500) {
                alert('Internal Server Error');
                return null;
            } else {
                alert(`HTTP error! Status: ${response.status}`);
                return null;
            }
        }
        return true;
    }).then(data => {
        localStorage.clear();
        return true;
    }).catch(error => {
        console.log(error);
        return null;
    });
}

export const logoutApi = {
    logout : logout
}