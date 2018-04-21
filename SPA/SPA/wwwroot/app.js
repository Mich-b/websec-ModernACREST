function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}


document.getElementById("login").addEventListener("click", login, false);
document.getElementById("logout").addEventListener("click", logout, false);


var config = {
    authority: "http://identityserver:8080",
    client_id: "SPA",
    redirect_uri: "http://singlepageapp:8082/callback.html",
    response_type: "id_token",
    scope: "openid profile role",
    post_logout_redirect_uri: "http://singlepageapp:8082/index.html",
};
var mgr = new Oidc.UserManager(config);


mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});



function login() {
    mgr.signinRedirect();
}

function logout() {
    mgr.signoutRedirect();
}