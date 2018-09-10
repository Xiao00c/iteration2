protect();

function protect() {
    var pwd = "safetychampion";
    while (true) {
        var pass = prompt('Please Enter Your Password', ' ');
        if (pass != pwd) {
            window.open("https://www.google.com");
        }
        if (pass == undefined) {
            window.open("https://www.google.com");
        }
        if (pass == pwd) {
            break;
        }
    }
}