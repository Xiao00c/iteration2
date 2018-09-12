protect();

function protect() {
    var pwd = "safetychampion";
    while (true) {
        console.log(document.cookie.indexOf("pass=1"));
        if (document.cookie.indexOf("pass=1") != -1) {
            break;
        }
        var pass = prompt('Please Enter Your Password', '');
        if (pass != pwd) {
            window.open("https://www.google.com");
        }
        if (pass == undefined) {
            window.open("https://www.google.com");
        }
        if (pass == pwd) {
            Cookies.set("pass", "1");
            break;
        }
    }
}

