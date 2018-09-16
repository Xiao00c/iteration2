var currentEvent;
var maxEvent = data.length;
var currentScore;

init();

function init() {
    currentEvent = 0;
    currentScore = 0;
    document.getElementById("question_desc").innerHTML = "NO." + (currentEvent + 1) + " " + data[currentEvent]["question_desc"];
    document.getElementById("option1").innerHTML = data[currentEvent]["answers"][0]["answer_desc"];
    document.getElementById("option2").innerHTML = data[currentEvent]["answers"][1]["answer_desc"];
    document.getElementById("option3").innerHTML = data[currentEvent]["answers"][2]["answer_desc"];
    updateProgress();
}

function next() {
    if (currentEvent < maxEvent - 1) {
        currentEvent = currentEvent + 1;

        document.getElementById("question_desc").innerHTML = "NO." + (currentEvent + 1) + " " + data[currentEvent]["question_desc"];
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

        //return to factors page after 5 seconds
        //var time = 5;
        //setInterval(function () {
        //    time--;
        //    document.getElementById("countDown").innerHTML = time;
        //    if (time == 0) {
        //        window.location = "Factors";
        //    }
        //}, 1000);
    }
    updateProgress();
}

function displayWrong() {
    document.getElementById("correct").style.display = "none";
    document.getElementById("incorrect").style.display = "block";
}

function displayCorrect() {
    document.getElementById("correct").style.display = "block";
    document.getElementById("incorrect").style.display = "none";
}

function clickChoice1() {
    //display explanation.
    document.getElementById("result").style.display = "block";
    document.getElementById("explanation").innerHTML = data[currentEvent]["answers"][0]["explanation"];

    if (data[currentEvent]["answers"][0]["correct"] == 0) {     
        //incorrect
        displayWrong();
    } else {
        displayCorrect();
        currentScore = currentScore + 10;
        //next();
    }
}

function clickChoice2() {
    //display explanation.
    document.getElementById("result").style.display = "block";
    document.getElementById("explanation").innerHTML = data[currentEvent]["answers"][1]["explanation"];

    if (data[currentEvent]["answers"][1]["correct"] == 0) {
        //incorrect
        displayWrong();
    } else {
        displayCorrect();
        currentScore = currentScore + 10;
        //next();
    }
}

function clickChoice3() {
    //display explanation.
    document.getElementById("result").style.display = "block";
    document.getElementById("explanation").innerHTML = data[currentEvent]["answers"][1]["explanation"];

    if (data[currentEvent]["answers"][1]["correct"] == 0) {
        //incorrect
        displayWrong();
    } else {
        displayCorrect();
        currentScore = currentScore + 10;
        //next();
    }
}

function continueChallenge() {
    document.getElementById("result").style.display = "none";
    next();
}

function updateProgress() {
    var progressString = "Current Challenge NO." + (currentEvent + 1) +
        " / Final Challenge NO." + maxEvent;
    document.getElementById("progress").innerHTML = progressString;
}
