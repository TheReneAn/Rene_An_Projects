function formValidation() {
    clearerrors();
    var result = validateFirstname();
    result = validateAge() && result;
    result = validateLastname() && result;
    result = validateUsername() && result;
    result = validatePassword() && result;
    result = checkPassword() && result;
    result = checkStatus() && result;
    result = checkSelect() && result;
    if(result){
        alert("Success");
    }
    return result;
 }

function validateAge(){
    var errors = document.querySelector("#ageErr");
    var input = document.signup.age.value.trim();
    document.querySelector("#ageErr").innerHTML = "";
    if(parseInt(input) < 18 || parseInt(input) > 60){
        errors.innerHTML += "<span>Please enter between 18 and 60.</span>"
        document.signup.age.focus();
        document.signup.age.select();
        return false;
    }
    if(parseInt(input) != input){
        errors.innerHTML += "<span>Please enter.</span>"
        document.signup.age.focus();
        document.signup.age.select();
        return false;
    }
    return true;
}

 function validateFirstname() {
    var errors = document.querySelector("#fnameErr");
    var allAlpha = true;
    var cap = true;
    var elem = document.getElementById("fname");
    var inputValue = elem.value.trim();
    document.querySelector("#fnameErr").innerHTML = "";
    if (inputValue.length == 0) {      /* check the length */
       errors.innerHTML += "<span>Enter the First name</span>";
       elem.focus();
       return false;
    }
    if(inputValue.charAt(0) < "A" || inputValue.charAt(0) > "Z"){
        cap = false;
    }
    inputValue = inputValue.toUpperCase();  
    for (var i = 0; i < inputValue.length; i++) {
         // check all character are letters
         if (inputValue.charAt(i) < "A" || inputValue.charAt(i) > "Z" )  { allAlpha = false; }
    }  // for
    if (!allAlpha){
       errors.innerHTML += "<span>Validate Only letter</span>";
       elem.focus();
       return false;
    }
    else if(!cap){
        errors.innerHTML += "<span>Start Only Capital letter</span>";
        elem.focus();
        return false;
    }
    return true;
 } 

 function validateLastname() {
    var errors = document.querySelector("#lnameErr");
    var allAlpha = true;
    var cap = true;
    var elem = document.getElementById("lname");
    var inputValue = elem.value.trim();
    document.querySelector("#lnameErr").innerHTML = "";
    if (inputValue.length == 0) {      /* check the length */
       errors.innerHTML += "<span>Enter the Last name</span>";
       elem.focus();
       return false;
    }
    if(inputValue.charAt(0) < "A" || inputValue.charAt(0) > "Z"){
        cap = false;
    }
    inputValue = inputValue.toUpperCase();  
    for (var i = 0; i < inputValue.length; i++) {
         // check all character are letters
         if (inputValue.charAt(i) < "A" || inputValue.charAt(i) > "Z" )  { allAlpha = false; }
    }  // for
    if (!allAlpha){
       errors.innerHTML += "<span>Validate Only letter</span>";
       elem.focus();
       return false;
    }
    else if(!cap){
        errors.innerHTML += "<span>Start Only Capital letter</span>";
        elem.focus();
        return false;
    }
    return true;
 }

 function validateUsername() {
    var errors = document.querySelector("#unameErr");
    var elem = document.getElementById("uname");
    var inputValue = elem.value.trim();
    document.querySelector("#unameErr").innerHTML = "";
    if(inputValue.length == 0) {
        errors.innerHTML += "<span>Enter the User name</span>";
        elem.focus();
        return false;
    }
    else if(inputValue.length < 6){
        errors.innerHTML += "<span>User name must have at least 6 character</span>";
        elem.focus();
        return false;
    }
    inputValue = inputValue.toUpperCase();
    if(inputValue.charAt(0) < "A" || inputValue.charAt(0) > "Z"){
        errors.innerHTML += "<span>First letter must be letter</span>";
        elem.focus();
        return false;
    }
    return true;
 }

var getValue;
 function validatePassword() {
    var errors = document.querySelector("#pwErr");
    var elem = document.getElementById("pw");
    var inputValue = elem.value.trim();
    var passAlpha = false;
    var passDigit = false;
    var alphString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var digitString = "1234567890";
    document.querySelector("#pwErr").innerHTML = "";
    
    if(inputValue.length == 0) {
        errors.innerHTML = "<span>Enter the Password</span>";
        elem.focus();
        return false;
    }
    else if(inputValue.length != 6) {
        errors.innerHTML = "<span>Enter 6 Characters </span>";
        elem.focus();
        return false;
    }
    var firstLetter = inputValue.toUpperCase();
    if(firstLetter.charAt(0) < "A" || firstLetter.charAt(0) > "Z"){
        errors.innerHTML += "<span>First letter must be letter</span>";
        elem.focus();
        return false;
    }
    for(var i = 0; i < inputValue.length; i++) {
        if (alphString.indexOf(inputValue.substr(i,1))>= 0) { 
            passAlpha = true; 
        }
    }
    if(!passAlpha) {
        errors.innerHTML += "<span>Input at least 1 Capital letter</span>"
        elem.focus();
        return false;
    }
    for(var i = 0; i < inputValue.length; i++) {
        if (digitString.indexOf(inputValue.substr(i,1)) >= 0) {
            passDigit = true;
        }
    }
    if(!passDigit) {
        errors.innerHTML += "<span>Input at least 1 Digit</span>"
        elem.focus();
        return false;
    }
    getValue = inputValue;
    return true;
 }

 function checkPassword() {
    var errors = document.querySelector("#pw2Err");
    var elem = document.getElementById("pw2");
    var inputValue = elem.value.trim();
    document.querySelector("#pw2Err").innerHTML = "";
    if(inputValue.length == 0) {
        errors.innerHTML = "<span>Enter the Password</span>";
        elem.focus();
        return false;
    }
    if(inputValue != getValue) {
        errors.innerHTML = "<span>Not match password</span>";
        elem.focus();
        return false;
    }
    return true;
 }

function checkStatus() {
    var errors = document.querySelector("#eduStatErr");
    var radio_num = document.signup.edustat.length;
    var onechecked = false;  
    document.querySelector("#eduStatErr");
    for (var i = 0; i < radio_num; i++) {
        if (document.signup.edustat[i].checked == true) { // true  = checked 
           onechecked = true;
        } 
    } 

    if (!onechecked) {                                        
         errors.innerHTML = "<span>None checked</span>"; 
         return false;
    }
    
    return true;  		
} 

function checkSelect() {
    var errors = document.querySelector("#eduLevelErr");
    var optionLength = document.signup.edu.options.length;
    var seleted = document.signup.edu.selectedIndex;
    document.querySelector("#eduLevelErr").innerHTML = "";

    if(seleted == 0) {
        errors.innerHTML += "<span>None seleted</span>";
        return false;
    }
    else{
        return true;
    }
}

function clearerrors() {     
    document.querySelector("#fnameErr").innerHTML = "";
    document.querySelector("#ageErr").innerHTML = "";
    document.querySelector("#lnameErr").innerHTML = "";
    document.querySelector("#unameErr").innerHTML = "";
    document.querySelector("#pwErr").innerHTML = "";
    document.querySelector("#pw2Err").innerHTML = "";
    document.querySelector("#eduStatErr").innerHTML = "";
    document.querySelector("#eduLevelErr").innerHTML = "";
 }

