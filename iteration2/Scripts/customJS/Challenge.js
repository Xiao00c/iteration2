var flag;
var currentEvent = 0;
var currentScore = 0;
var maxEvent;

checkPostcodeChanged();

//verify if current postcode == previous postcode
function checkPostcodeChanged() {
    //if first time access
    if (Cookies.get("previousPostcode") == 'undefined') {
        Cookies.set("previousPostcode", postcode + "");
    }
    //if not first time but same postcode
    else if (Cookies.get("previousPostcode") == postcode) {
    }
    //not the same
    else {
        restartChallenge();
        Cookies.set("previousPostcode", postcode + "");
    }
}

//hide questions
document.getElementById("ques").style.display = "none";
document.getElementById("introduction").style.display = "none";


//hide map and display challenge
function hideMap(factor) {
    document.getElementById("map").style.display = "none";
    document.getElementById("introduction").style.display = "block";
    document.getElementsByClassName("factor").innerHTML = factor;
}

//intro page
function continueToChallenge() {
    document.getElementById("introduction").style.display = "none";
    document.getElementById("ques").style.display = "block";
}

//level 1 - 5
function level1() {
    flag = 1;
    hideMap("Drunk");
    initQuestions(flag);

    //texts
    document.getElementById("introduction_line1").innerHTML = "DRINKING AND DRIVING";
    document.getElementById("introduction_line2").innerHTML = "The postcode you entered belongs to " + LGAs + " council(s).";
    document.getElementById("introduction_line3").innerHTML = "Did you know that driving after drinking alcohol is against the law?";
    document.getElementById("introduction_line4").innerHTML = "One out of every five cases of drunken driving in Australia has resulted in serious accidents harming the driver and those around them. ";
    document.getElementById("introduction_line5").innerHTML = "Look below to see how drunk driving has affected " + LGAs;

    document.getElementById("drunk").style.display = "block";
    document.getElementById("speeding").style.display = "none";
    document.getElementById("distraction").style.display = "none";
    document.getElementById("fatigue").style.display = "none";
    document.getElementById("general").style.display = "none";
}

function level2() {
    flag = 2;
    hideMap("Speeding");
    initQuestions(flag);

    //text
    document.getElementById("introduction_line1").innerHTML = "SPEEDING";
    document.getElementById("introduction_line2").innerHTML = "The postcode you entered belongs to " + LGAs + " council(s).";
    document.getElementById("introduction_line3").innerHTML = "One of the major causes of accidents is driving the car over the speed-limit. Speeding affects other road users as they become unsure of what to do when they see a speeding car.";
    document.getElementById("introduction_line4").innerHTML = "It looks like problems due to Speeding is " + speeding_imp + " compared to the other factors in your area.";
    document.getElementById("introduction_line5").innerHTML = "";

    document.getElementById("drunk").style.display = "none";
    document.getElementById("speeding").style.display = "block";
    document.getElementById("distraction").style.display = "none";
    document.getElementById("fatigue").style.display = "none";
    document.getElementById("general").style.display = "none";
}


function level3() {
    flag = 3;
    hideMap("Distraction");
    initQuestions(flag);

    //text
    document.getElementById("introduction_line1").innerHTML = "DISTRACTION";
    document.getElementById("introduction_line2").innerHTML = "";
    document.getElementById("introduction_line3").innerHTML = "Distracted drivers have become a huge problem all over the world. Drivers get distracted by using their mobile phones, talking while driving and watching videos.";
    document.getElementById("introduction_line4").innerHTML = "It seems like the problems due to distraction are increasing in Australia";
    document.getElementById("introduction_line5").innerHTML = "";


    document.getElementById("drunk").style.display = "none";
    document.getElementById("speeding").style.display = "none";
    document.getElementById("distraction").style.display = "block";
    document.getElementById("fatigue").style.display = "none";
    document.getElementById("general").style.display = "none";
}

function level4() {
    flag = 4;
    hideMap("Fatigue");
    initQuestions(flag);

    //text
    document.getElementById("introduction_line1").innerHTML = "FATIGUE";
    document.getElementById("introduction_line2").innerHTML = "";
    document.getElementById("introduction_line3").innerHTML = "Accidents are caused when drivers are tired and sleepy. People who are affected by fatigue could be anyone you know. Someone driving home after a whole night of studying, after working day and night without a break. But when a someone who is sleepy drives, they are at risk of sleeping behind the wheel!";
    document.getElementById("introduction_line4").innerHTML = "It looks like problems due to Fatigue is " + fatigue_imp + " compared to the other factors in your area.";
    document.getElementById("introduction_line5").innerHTML = "";

    document.getElementById("drunk").style.display = "none";
    document.getElementById("speeding").style.display = "none";
    document.getElementById("distraction").style.display = "none";
    document.getElementById("fatigue").style.display = "block";
    document.getElementById("general").style.display = "none";
}

function level5() {
    flag = 5;
    hideMap("General");
    initQuestions(flag);

    //text
    document.getElementById("introduction_line1").innerHTML = "GENERAL SAFETY";
    document.getElementById("introduction_line2").innerHTML = "";
    document.getElementById("introduction_line3").innerHTML = "Congratulations! You have unlocked the final challenge. Safety on the road is a concern for all of us. Now that you know what factors affect car accidents, spread your knowledge with people you know and help make Australian roads safer.";
    document.getElementById("introduction_line4").innerHTML = "This round will test your knowledge of general safety on the roads.";
    document.getElementById("introduction_line5").innerHTML = "";

    document.getElementById("drunk").style.display = "none";
    document.getElementById("speeding").style.display = "none";
    document.getElementById("distraction").style.display = "none";
    document.getElementById("fatigue").style.display = "none";
    document.getElementById("general").style.display = "block";
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

function initQuestions(flag) {
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
        updateProgress();
    }
    else {
        Cookies.set(flag, currentScore);
        Cookies.set(flag + "Finish", 1);
        console.log(flag + "," + currentScore);
        document.getElementById("ques").style.display = "none";
        document.getElementById("finish").style.display = "block";
    }

}

//click an option
function clickChoice(id) {
    //display explanation.
    document.getElementById("result").style.display = "block";
    document.getElementById("explanation").innerHTML = getQuestions(flag)[currentEvent]["answers"][id]["explanation"];

    //hide choices
    document.getElementById("choices").style.display = "none";
    if (getQuestions(flag)[currentEvent]["answers"][id]["correct"] == 0) {
        //incorrect
        displayWrong();
    } else {
        displayCorrect();
        currentScore = currentScore + 10;
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
    document.getElementById("choices").style.display = "block";
    next();
}

function updateProgress() {
    var progressString = "NO." + (currentEvent + 1) +
        " / NO." + maxEvent;
    document.getElementById("progress").innerHTML = progressString;
    //set Cookies for section total score
    Cookies.set(flag + "FactorT", maxEvent * 10);
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
    if (Cookies.get("2Finish") == 1 &&
        Cookies.get("1Finish") == 1 &&
        Cookies.get("3Finish") == 1 &&
        Cookies.get("5Finish") == 1 &&
        Cookies.get("4Finish") == 1) {
        displayFinish();
    }
}

//check each factor is done or not.
checkProgress()

//check progress and grey out images
function checkProgress() {
    //!= "1" => not finished.

    //1
    if (Cookies.get("1Finish") != 1) {
        document.getElementById("1Factor").style.display = "block";
        document.getElementById("1Done").style.display = "none";
        document.getElementById("final").style.display = "none";
        document.getElementById("Locked0Trophy").style.display = "block";
    }
    //2
    else if (Cookies.get("2Finish") != 1) {
        document.getElementById("1Factor").style.display = "none";
        document.getElementById("1Done").style.display = "block";
        document.getElementById("Locked0Trophy").style.display = "none";
        document.getElementById("Locked1Trophy").style.display = "block";
        document.getElementById("2Locked").style.display = "none";
        document.getElementById("2Factor").style.display = "block";
        document.getElementById("2Done").style.display = "none";
    }
    //3
    else if (Cookies.get("3Finish") != 1) {
        document.getElementById("2Locked").style.display = "none";
        document.getElementById("2Factor").style.display = "none";
        document.getElementById("2Done").style.display = "block";
        document.getElementById("Locked1Trophy").style.display = "none";
        document.getElementById("Locked2Trophy").style.display = "block";
        document.getElementById("3Locked").style.display = "none";
        document.getElementById("3Factor").style.display = "block";
        document.getElementById("3Done").style.display = "none";
    }
    //4
    else if (Cookies.get("4Finish") != 1) {
        document.getElementById("3Locked").style.display = "none";
        document.getElementById("3Factor").style.display = "none";
        document.getElementById("3Done").style.display = "block";
        document.getElementById("Locked2Trophy").style.display = "none";
        document.getElementById("Locked3Trophy").style.display = "block";
        document.getElementById("4Locked").style.display = "none";
        document.getElementById("4Factor").style.display = "block";
        document.getElementById("4Done").style.display = "none";
    }
    //5
    else if (Cookies.get("5Finish") != 1) {
        document.getElementById("4Locked").style.display = "none";
        document.getElementById("4Factor").style.display = "none";
        document.getElementById("4Done").style.display = "block";
        document.getElementById("Locked3Trophy").style.display = "none";
        document.getElementById("Locked4Trophy").style.display = "block";
        document.getElementById("5Locked").style.display = "none";
        document.getElementById("5Factor").style.display = "block";
        document.getElementById("5Done").style.display = "none";
    }
    //final
    else {
        //grey out 5
        document.getElementById("5Locked").style.display = "none";
        document.getElementById("5Factor").style.display = "none";
        document.getElementById("5Done").style.display = "block";

        //grey out 1
        document.getElementById("1Factor").style.display = "none";
        document.getElementById("1Done").style.display = "block";

        //display final
        document.getElementById("Locked0Trophy").style.display = "none";
        document.getElementById("Locked1Trophy").style.display = "none";
        document.getElementById("Locked2Trophy").style.display = "none";
        document.getElementById("Locked3Trophy").style.display = "none";
        document.getElementById("Locked4Trophy").style.display = "none";
        document.getElementById("final").style.display = "block";
    }
}

function displayFinish() {
    document.getElementById("map").style.display = "none";
    document.getElementById("challengeResult").style.display = "block";
    document.getElementById("challengeScore").innerHTML =
        "Congratulations! You have successfully finished the SAFETY CHALLENGE with a final score of " + score + ".";
    document.getElementById("inputName").style.display = "block";
    //document.getElementById("downloadCertificate_btn").style.display = "block";
    if (Cookies.get("inserted") == 'undefined') {
        SaveDocument();
        Cookies.set("inserted", 1);
    }
    
}

function restartChallenge() {
    document.getElementById("nameText").innerHTML = "What about have your certificate? But we need your name to personalize it.";
    document.getElementById("inputName").style.display = "block";
    //document.getElementById("downloadCertificate_btn").style.display = "block";
    Cookies.set("inserted", 'undefined');

    document.getElementById("map").style.display = "block";
    document.getElementById("challengeResult").style.display = "none";

    //reset all scores
    Cookies.set("1Finish", 0);
    Cookies.set("2Finish", 0);
    Cookies.set("3Finish", 0);
    Cookies.set("4Finish", 0);
    Cookies.set("5Finish", 0);

    //display all factor again
    document.getElementById("1Factor").style.display = "block";
    document.getElementById("1Done").style.display = "none";

    //lock 2-5
    document.getElementById("2Locked").style.display = "block";
    document.getElementById("2Factor").style.display = "none";
    document.getElementById("2Done").style.display = "none";

    document.getElementById("3Locked").style.display = "block";
    document.getElementById("3Factor").style.display = "none";
    document.getElementById("3Done").style.display = "none";

    document.getElementById("4Locked").style.display = "block";
    document.getElementById("4Factor").style.display = "none";
    document.getElementById("4Done").style.display = "none";

    document.getElementById("5Locked").style.display = "block";
    document.getElementById("5Factor").style.display = "none";
    document.getElementById("5Done").style.display = "none";

    //lock treasure
    document.getElementById("Locked1Trophy").style.display = "none";
    document.getElementById("Locked2Trophy").style.display = "none";
    document.getElementById("Locked3Trophy").style.display = "none";
    document.getElementById("Locked4Trophy").style.display = "none";
    document.getElementById("final").style.display = "none";
    document.getElementById("Locked0Trophy").style.display = "block";

}

function notNow() {
    document.getElementById("map").style.display = "block";
    document.getElementById("challengeResult").style.display = "none";
}

console.log("1 score: " + Cookies.get("1"));
console.log("2 score: " + Cookies.get("2"));
console.log("3 score: " + Cookies.get("3"));
console.log("4 score: " + Cookies.get("4"));
console.log("5 score: " + Cookies.get("5"));