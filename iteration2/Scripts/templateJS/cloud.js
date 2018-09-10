function start(){	
	
};

function startF(){	
	setTimeout(function () {
		$('.car1').css({marginRight:-500}).stop().delay(100).animate({marginRight:0},1200,'easeOutBack');

	}, 200);
};

function showSplash(){
	setTimeout(function () {
		$('.menu').stop().animate({marginLeft:-2000},800,'easeInOutExpo', function(){ $(this).css({display:'none'}); });
		$('.bot1').stop().delay(400).animate({marginTop:200},800,'easeInOutBack');
		$('.plane1').stop().delay(600).animate({marginTop:0, marginLeft:0},800,'easeInOutBack');
		$('.cloud1').stop().delay(700).animate({marginTop:0},800,'easeInOutBack');
		$('.cloud1 .txt').css({display:'block'}).stop().delay(1200).animate({opacity:1},800,'easeOutExpo');
		$('.slogan1').stop().delay(800).animate({marginTop:0},800,'easeInOutBack');
		$('header').stop().delay(900).animate({marginTop:0},800,'easeInOutBack');
		$('.close').stop().animate({opacity:0},800,'easeOutExpo', function(){ $(this).css({display:'none'}); });
		$('.enter').css({display:'block'}).stop().delay(500).animate({opacity:1},800,'easeOutExpo');





	}, 100);	
};
function hideSplash(){ 	
	$('header').stop().delay(0).animate({marginTop:-110},800,'easeInOutBack');
	$('.cloud1').stop().delay(200).animate({marginTop:-110},800,'easeInOutBack');
	$('.cloud1 .txt').stop().animate({opacity:0},800,'easeOutExpo', function(){ $(this).css({display:'none'}); });
	$('.slogan1').stop().delay(100).animate({marginTop:-110},800,'easeInOutBack');
	$('.plane1').stop().delay(300).animate({marginTop:-250, marginLeft:170},800,'easeInOutBack');
	$('.bot1').stop().delay(400).animate({marginTop:-340},800,'easeInOutBack');
	$('.enter').stop().animate({opacity:0},800,'easeOutExpo', function(){ $(this).css({display:'none'}); });
	$('.close').css({display:'block'}).stop().delay(500).animate({opacity:1},800,'easeOutExpo');
	$('.menu').css({display:'block'}).stop().delay(800).animate({marginLeft:0},800,'easeOutExpo');


};
function hideSplashQ(){	
	$('header').css({marginTop:-110});
	$('.cloud1').css({marginTop:-110});
	$('.cloud1 .txt').css({opacity:0, display:'none'});
	$('.slogan1').css({marginTop:-110});
	$('.plane1').css({marginTop:-250, marginLeft:170});
	$('.bot1').css({marginTop:-340});
	$('.enter').css({opacity:0, display:'none'});
	$('.close').css({display:'block', opacity:1});
	$('.menu').css({display:'block', marginLeft:0});

	
};

///////////////////

$(document).ready(function() {
	////// sound control	
	$("#jquery_jplayer").jPlayer({
		ready: function () {
			$(this).jPlayer("setMedia", {
				mp3:"music.mp3"
			});
			//$(this).jPlayer("play");
			var click = document.ontouchstart === undefined ? 'click' : 'touchstart';
          	var kickoff = function () {
            $("#jquery_jplayer").jPlayer("play");
            document.documentElement.removeEventListener(click, kickoff, true);
         	};
          	document.documentElement.addEventListener(click, kickoff, true);
		},
		
		repeat: function(event) { // Override the default jPlayer repeat event handler				
				$(this).bind($.jPlayer.event.ended + ".jPlayer.jPlayerRepeat", function() {
					$(this).jPlayer("play");
				});			
		},
		swfPath: "js",
		cssSelectorAncestor: "#jp_container",
		supplied: "mp3",
		wmode: "window"
	});

	var user_agent = navigator.userAgent.toLowerCase();
	var mobile = false;
	if ((/up.browser|up.link|mmp|symbian|smartphone|midp|wap|vodafone|o2|pocket|kindle|mobile|pda|psp|treo|blackberry|opera mini|android|iphone|ipod|ipad/.test(user_agent))) {	mobile = true;}
	
	if (mobile) {		
	}
	else{	
	  	$('#clouds1').pan({fps: 30, speed: 2.5, dir: 'left', depth: 10});	  	
	  	$('#clouds2').pan({fps: 30, speed: 0.9, dir: 'left', depth: 10});
	  	//$('#bot1').pan({fps: 30, speed: 0.5, dir: 'right', depth: 10});
	}
		
 });  ////////



