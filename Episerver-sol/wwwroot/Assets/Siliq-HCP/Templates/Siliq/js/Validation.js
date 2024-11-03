
function validateZipCode() {
    var watchZipInput = document.getElementById("locator_zip").value;
    if (watchZipInput == '')
    {
        document.getElementById("zipCodeError").innerHTML = "Please enter a valid ZIP Code";
        return false;
    }
    if (!isZipCodeValid())
    {
        document.getElementById("zipCodeError").innerHTML = "Please enter correct Zip Code";
        return false;
    }
    else {
        document.getElementById("zipCodeError").innerHTML = "";
        localStorage.setItem("zip", document.getElementById("locator_zip").value);
        
        initMap();
        keepInput(document.getElementById("locator_zip"));
        return true;
    }
};
function allowAlphaNumericSpace(e) {
    var code = ('charCode' in e) ? e.charCode : e.keyCode;
    if (!(code == 32) && // space
        !(code > 47 && code < 58) && // numeric (0-9)
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123)) { // lower alpha (a-z)
        e.preventDefault();
    }
};
function isZipCodeValid() {
    var watchZipInput = document.getElementById("locator_zip").value;
    if (watchZipInput == '')
    {
        document.getElementById("zipCodeError").innerHTML = "Please enter a valid ZIP Code";
        return false;
    }
    var answer = true; 
    var isValidZip = /(^\d{5}$)|(^\d{5}-\d{4}$)/.test(watchZipInput);
    if (!isValidZip)
    {
        answer = false;
        //document.getElementById("zipCodeError").innerHTML = "Please enter correct Zip Code";
    }
    return answer;
}
function keepInput(input) {
    var key = "zip"; var storedValue = localStorage.getItem(key); if (storedValue)
        input.value = storedValue; if (storedValue == null) { document.getElementById("results").style.display = "none"; }
        else { document.getElementById("results").style.display = "block"; }
    input.addEventListener('input', function () { localStorage.setItem(key, input.value); }); window.localStorage.removeItem(key, input.value);
}