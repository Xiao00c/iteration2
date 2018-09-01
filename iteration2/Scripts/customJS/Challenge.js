var currentEvent;
var maxEvent = data.length;
var currentScore;

init();

function init() {
    currentEvent = 0;
    currentScore = 0;
    document.getElementById("question_desc").innerHTML = data[currentEvent]["question_desc"];
    document.getElementById("option1").innerHTML = data[currentEvent]["answers"][0]["answer_desc"];
    document.getElementById("option2").innerHTML = data[currentEvent]["answers"][1]["answer_desc"];
    document.getElementById("option3").innerHTML = data[currentEvent]["answers"][2]["answer_desc"];

}

function next() {
    if (currentEvent < maxEvent - 1) {
        currentEvent = currentEvent + 1;

        document.getElementById("question_desc").innerHTML = data[currentEvent]["question_desc"];
        document.getElementById("option1").innerHTML = data[currentEvent]["answers"][0]["answer_desc"];
        document.getElementById("option2").innerHTML = data[currentEvent]["answers"][1]["answer_desc"];
        document.getElementById("option3").innerHTML = data[currentEvent]["answers"][2]["answer_desc"];
    }
    else {
        Cookies.set(factor, currentScore);
        Cookies.set(factor + "Finish", "1");
        console.log(factor + "," + currentScore);
        document.getElementById("ques").style.display = "none";
        document.getElementById("finish").style.display = "block";
    }

}


function clickChoice1() {
    if (data[currentEvent]["answers"][0]["correct"] == 0) {
        //alert("good work!");
        document.getElementById("result").style.display = "block";
        next();
    } else {
        currentScore = currentScore + 10;
        next();
    }
}

function clickChoice2() {
    if (data[currentEvent]["answers"][1]["correct"] == 0) {
        //alert("good work!");
        document.getElementById("result").style.display = "block";
        next();
    } else {
        currentScore = currentScore + 10;
        next();
    }
}

function clickChoice3() {
    if (data[currentEvent]["answers"][2]["correct"] == 0) {
        //alert("good work!");
        document.getElementById("result").style.display = "block";
        next();
    } else {
        currentScore = currentScore + 10;
        next();
    }
}

function continueChallenge() {
    document.getElementById("result").style.display = "none";
}
