// JavaScript Document
function selectTag(showContent,selfObj){

	var tag = document.getElementById("salesb").getElementsByTagName("li");
	var taglength = tag.length;
	for(i=0; i<taglength; i++){
		tag[i].className = "";
	}
	selfObj.parentNode.className = "selectTag";
	
	for(i=0; j=document.getElementById("saleContent"+i); i++){
		j.style.display = "none";
	}
	document.getElementById(showContent).style.display = "block";
	
}