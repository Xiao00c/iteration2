
window.onload = checkProgress;

//calculate total score
var score;

function checkProgress() {
    //calculate total score
    score = parseInt(Cookies.get("Speeding")) +
        parseInt(Cookies.get("Drunk")) +
        parseInt(Cookies.get("Distraction")) +
        parseInt(Cookies.get("General")) +
        parseInt(Cookies.get("Fatigue"));

    //check if all done
    if (Cookies.get("SpeedingFinish") == "1" &&
        Cookies.get("DrunkFinish") == "1" &&
        Cookies.get("DistractionFinish") == "1" &&
        Cookies.get("GeneralFinish") == "1" &&
        Cookies.get("FatigueFinish") == "1") {
        displayFinish();
    }

    //check each factor is done or not.
    //Distraction
    //!= "1" => not finished.
    if (Cookies.get("DistractionFinish") != "1") {
        document.getElementById("distractionFactor").style.display = "block";
        document.getElementById("distractionDone").style.display = "none";
    } else {
        document.getElementById("distractionFactor").style.display = "none";
        document.getElementById("distractionDone").style.display = "block";
    }

    //Fatigue
    if (Cookies.get("FatigueFinish") != "1") {
        document.getElementById("fatigueFactor").style.display = "block";
        document.getElementById("fatigueDone").style.display = "none";
    } else {
        document.getElementById("fatigueFactor").style.display = "none";
        document.getElementById("fatigueDone").style.display = "block";
    }

    //Speeding
    if (Cookies.get("SpeedingFinish") != "1") {
        document.getElementById("speedingFactor").style.display = "block";
        document.getElementById("speedingDone").style.display = "none";
    } else {
        document.getElementById("speedingFactor").style.display = "none";
        document.getElementById("speedingDone").style.display = "block";
    }

    //General
    if (Cookies.get("GeneralFinish") != "1") {
        document.getElementById("generalFactor").style.display = "block";
        document.getElementById("generalDone").style.display = "none";
    } else {
        document.getElementById("generalFactor").style.display = "none";
        document.getElementById("generalDone").style.display = "block";
    }

    //Drunk
    if (Cookies.get("DrunkFinish") != "1") {
        document.getElementById("drunkFactor").style.display = "block";
        document.getElementById("drunkDone").style.display = "none";
    } else {
        document.getElementById("drunkFactor").style.display = "none";
        document.getElementById("drunkDone").style.display = "block";
    }

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
    Cookies.set("SpeedingFinish", "0");
    Cookies.set("DrunkFinish", "0");
    Cookies.set("DistractionFinish", "0");
    Cookies.set("GeneralFinish", "0");
    Cookies.set("FatigueFinish", "0");

    //display all factor again
    document.getElementById("distractionFactor").style.display = "block";
    document.getElementById("distractionDone").style.display = "none";
    document.getElementById("fatigueFactor").style.display = "block";
    document.getElementById("fatigueDone").style.display = "none";
    document.getElementById("speedingFactor").style.display = "block";
    document.getElementById("speedingDone").style.display = "none";
    document.getElementById("generalFactor").style.display = "block";
    document.getElementById("generalDone").style.display = "none";
    document.getElementById("drunkFactor").style.display = "block";
    document.getElementById("drunkDone").style.display = "none";
}

function notNow() {
    document.getElementById("challengeResult").style.display = "none";
}

console.log("Speeding score: " + Cookies.get("Speeding"));
console.log("Drunk score: " + Cookies.get("Drunk"));
console.log("Distraction score: " + Cookies.get("Distraction"));
console.log("General score: " + Cookies.get("General"));
console.log("Fatigue score: " + Cookies.get("Fatigue"));
