//testing

var flag;
var currentEvent = 0;
var currentScore = 0;
var maxEvent;


//hide questions
document.getElementById("ques").style.display = "none";

//hide map and display challenge
function hideMap() {
    document.getElementById("map").style.display = "none";
    document.getElementById("ques").style.display = "block";
}

//level 1 - 5
function level1() {
    flag = 1;
    hideMap();
    initQuestions(flag);
}

function level2() {
    flag = 2;
    hideMap();
    initQuestions(flag);
}

function level3() {
    flag = 3;
    hideMap();
    initQuestions(flag);
}

function level4() {
    flag = 4;
    hideMap();
    initQuestions(flag);
}

function level5() {
    flag = 5;
    hideMap();
    initQuestions(flag);
}

function getQuestions(flag) {

    var question;
    switch (flag) {
        case 1: question = data.Level1; break;
        case 2: question = data.Level2; break;
        case 3: question = data.Level3; break;
        case 4: question = data.Level4; break;
        case 5: question = data.Level5; break;
    }
    return question;
}

function initQuestions() {
    currentEvent = 0;
    currentScore = 0;
    maxEvent = getQuestions(flag).length;
    document.getElementById("question_desc").innerHTML = "NO." + (currentEvent + 1) + " " + getQuestions(flag)[currentEvent].question_desc;
    document.getElementById("option1").innerHTML = getQuestions(flag)[currentEvent].answers[0].answer_desc;
    document.getElementById("option2").innerHTML = getQuestions(flag)[currentEvent].answers[1].answer_desc;
    document.getElementById("option3").innerHTML = getQuestions(flag)[currentEvent].answers[2].answer_desc;
    updateProgress();
}

function next() {
    if (currentEvent < maxEvent - 1) {
        currentEvent = currentEvent + 1;

        document.getElementById("question_desc").innerHTML = "NO." + (currentEvent + 1) + " " + getQuestions(flag)[currentEvent]["question_desc"];
        document.getElementById("option1").innerHTML = getQuestions(flag)[currentEvent]["answers"][0]["answer_desc"];
        document.getElementById("option2").innerHTML = getQuestions(flag)[currentEvent]["answers"][1]["answer_desc"];
        document.getElementById("option3").innerHTML = getQuestions(flag)[currentEvent]["answers"][2]["answer_desc"];
    }
    else {
        Cookies.set(flag, currentScore);
        Cookies.set(flag + "Finish", "1");
        console.log(flag + "," + currentScore);
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

function clickChoice1() {
    //display explanation.
    document.getElementById("result").style.display = "block";

    document.getElementById("explanation").innerHTML = getQuestions(flag)[currentEvent]["answers"][0]["explanation"];

    if (getQuestions(flag)[currentEvent]["answers"][0]["correct"] == 0) {
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
    document.getElementById("explanation").innerHTML = getQuestions(flag)[currentEvent]["answers"][1]["explanation"];

    if (getQuestions(flag)[currentEvent]["answers"][1]["correct"] == 0) {
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
    document.getElementById("explanation").innerHTML = getQuestions(flag)[currentEvent]["answers"][1]["explanation"];

    if (getQuestions(flag)[currentEvent]["answers"][1]["correct"] == 0) {
        //incorrect
        displayWrong();
    } else {
        displayCorrect();
        currentScore = currentScore + 10;
        //next();
    }
}

function backToMap() {
    document.getElementById("map").style.display = "block";
    document.getElementById("ques").style.display = "none";
    document.getElementById("finish").style.display = "none";
    checkProgress();
}

function displayWrong() {
    document.getElementById("correct").style.display = "none";
    document.getElementById("incorrect").style.display = "block";
}

function displayCorrect() {
    document.getElementById("correct").style.display = "block";
    document.getElementById("incorrect").style.display = "none";
}

function continueChallenge() {
    document.getElementById("result").style.display = "none";
    next();
}

function updateProgress() {
    var progressString = "NO." + (currentEvent + 1) + 
        " / NO." + maxEvent;
    document.getElementById("progress").innerHTML = progressString;
}

//window.onload = checkProgress;

//calculate total score
var score;

function getTreasure() {
    //calculate total score
    score = parseInt(Cookies.get("2")) +
        parseInt(Cookies.get("1")) +
        parseInt(Cookies.get("3")) +
        parseInt(Cookies.get("5")) +
        parseInt(Cookies.get("4"));

    //check if all done
    if (Cookies.get("2Finish") == "1" &&
        Cookies.get("1Finish") == "1" &&
        Cookies.get("3Finish") == "1" &&
        Cookies.get("5Finish") == "1" &&
        Cookies.get("4Finish") == "1") {
        displayFinish();
    }

    //check each factor is done or not.
    //3
    //!= "1" => not finished.
    //if (Cookies.get("3Finish") != "1") {
    //    document.getElementById("3Factor").style.display = "block";
    //    document.getElementById("3Done").style.display = "none";
    //} else {
    //    document.getElementById("3Factor").style.display = "none";
    //    document.getElementById("3Done").style.display = "block";
    //}

    ////4
    //if (Cookies.get("4Finish") != "1") {
    //    document.getElementById("4Factor").style.display = "block";
    //    document.getElementById("4Done").style.display = "none";
    //} else {
    //    document.getElementById("4Factor").style.display = "none";
    //    document.getElementById("4Done").style.display = "block";
    //}

    ////2
    //if (Cookies.get("2Finish") != "1") {
    //    document.getElementById("2Factor").style.display = "block";
    //    document.getElementById("2Done").style.display = "none";
    //} else {
    //    document.getElementById("2Factor").style.display = "none";
    //    document.getElementById("2Done").style.display = "block";
    //}

    ////5
    //if (Cookies.get("5Finish") != "1") {
    //    document.getElementById("5Factor").style.display = "block";
    //    document.getElementById("5Done").style.display = "none";
    //} else {
    //    document.getElementById("5Factor").style.display = "none";
    //    document.getElementById("5Done").style.display = "block";
    //}

    ////1
    //if (Cookies.get("1Finish") != "1") {
    //    document.getElementById("1Factor").style.display = "block";
    //    document.getElementById("1Done").style.display = "none";
    //} else {
    //    document.getElementById("1Factor").style.display = "none";
    //    document.getElementById("1Done").style.display = "block";
    //}

}

function displayFinish() {
    document.getElementById("challengeResult").style.display = "block";
    document.getElementById("challengeScore").innerHTML =
        "This certificate is awarded to YOU for having successfully achieved for " +
        "all factors' challenges for the score of " + score + ".";
}

function restartChallenge() {
    document.getElementById("challengeResult").style.display = "none";

    //reset all scores
    Cookies.set("2Finish", "0");
    Cookies.set("1Finish", "0");
    Cookies.set("3Finish", "0");
    Cookies.set("5Finish", "0");
    Cookies.set("4Finish", "0");

    //display all factor again
    document.getElementById("3Factor").style.display = "block";
    document.getElementById("3Done").style.display = "none";
    document.getElementById("4Factor").style.display = "block";
    document.getElementById("4Done").style.display = "none";
    document.getElementById("2Factor").style.display = "block";
    document.getElementById("2Done").style.display = "none";
    document.getElementById("5Factor").style.display = "block";
    document.getElementById("5Done").style.display = "none";
    document.getElementById("1Factor").style.display = "block";
    document.getElementById("1Done").style.display = "none";
}

function notNow() {
    document.getElementById("challengeResult").style.display = "none";
}

console.log("2 score: " + Cookies.get("2"));
console.log("1 score: " + Cookies.get("1"));
console.log("3 score: " + Cookies.get("3"));
console.log("5 score: " + Cookies.get("5"));
console.log("4 score: " + Cookies.get("4"));
