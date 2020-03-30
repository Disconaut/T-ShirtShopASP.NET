var registration = document.getElementById("regBut");
var genPassBut = document.getElementById("gen-password");
var fnameField = document.getElementById("fname");
var lnameField = document.getElementById("lname");
var emailField = document.getElementById("email");
var bdateField = document.getElementById("bdate");
var phoneField = document.getElementById("phoneNumb");
var passwordField = document.getElementById("password");
//var confirmField = document.getElementById("confirmation");
var colorDefault = fnameField.style.borderColor;
var colorInvalid = "red";
var colorValid = "lightgreen";

function validateEmail(email) {
    var emailRegex = new RegExp(/^[A-Za-z0-9]+@[A-Za-z]+\.[a-z]{2,}$/);
    return emailRegex.test(email);
}

function validatePhoneNumb(phone) {
    var phoneRegex = new RegExp(/^\+380\d{9}$/);
    return phoneRegex.test(phone);
}

function validateName(name) {
    var nameRegex = new RegExp(/^[A-Z][a-z]+'?[a-z]$/);
    return nameRegex.test(name);
}

function validatePassword(password) {
    return password.length >= 8 && password.length <= 20;
}

function confirmPassword(password, confirmation) {
    return password === confirmation;
}

function validateForm() {
    var valid = true;

    if (fnameField.value.length === 0) {
        valid = false;
        fnameField.style.borderColor = colorDefault;
    } else if (!validateName(fnameField.value)) {
        valid = false;
        fnameField.style.borderColor = colorInvalid;
    } else {
        fnameField.style.borderColor = colorValid;
    }

    if (lnameField.value.length === 0) {
        valid = false;
        lnameField.style.borderColor = colorDefault;
    } else if (!validateName(lnameField.value)) {
        valid = false;
        lnameField.style.borderColor = colorInvalid;
    } else {
        lnameField.style.borderColor = colorValid;
    }

    if (emailField.value.length === 0) {
        valid = false;
        emailField.style.borderColor = colorDefault;
    } else if (!validateEmail(emailField.value)) {
        valid = false;
        emailField.style.borderColor = colorInvalid;
    } else {
        emailField.style.borderColor = colorValid;
    }

    if (bdateField.value.lenght === 0) {
        valid = false;
        bdateField.style.borderColor = colorDefault;
    } else if (!bdateField.checkValidity()) {
        valid = false;
        bdateField.style.borderColor = colorInvalid;
    } else {
        bdateField.style.borderColor = colorValid;
    }

    if (phoneField.value.length === 0) {
        valid = false;
        phoneField.style.borderColor = colorDefault;
    } else if (!validatePhoneNumb(phoneField.value)) {
        valid = false;
        phoneField.style.borderColor = colorInvalid;
    } else {
        phoneField.style.borderColor = colorValid;
    }

    if (passwordField.value.length === 0) {
        valid = false;
        passwordField.style.borderColor = colorDefault;
    } else if (!validatePassword(passwordField.value)) {
        valid = false;
        passwordField.style.borderColor = colorInvalid;
    } else {
        passwordField.style.borderColor = colorValid;
    }

    if (valid) {
        registration.disabled = false;
    } else {
        registration.disabled = true;
    }
}

function getRandInteger(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function generatePassword() {
    var intCount = getRandInteger(2, 14);
    var lowerLetterCount = getRandInteger(2, 16 - intCount);
    var upperLetterCount = getRandInteger(2, 18 - intCount - lowerLetterCount);
    var specCount = getRandInteger(2, 20 - intCount - lowerLetterCount - upperLetterCount);
    const length = intCount + lowerLetterCount + upperLetterCount + specCount;

    var counts = [intCount, lowerLetterCount, upperLetterCount, specCount];
    var generators = [
        () => getRandInteger(0, 9),
        () => String.fromCharCode(getRandInteger('a'.charCodeAt(0), 'z'.charCodeAt(0))),
        () => String.fromCharCode(getRandInteger('A'.charCodeAt(0), 'Z'.charCodeAt(0))),
        () => {
            const symbols = "%:*#@%&";
            return symbols[getRandInteger(0, symbols.length - 1)];
        }
    ];

    /*
     int => 0
     lower char => 1
     upper char => 2
     spec char => 3
    */
    var passwordTemplate = [];
    for (var i = 0; i < length; ++i) {
        var generated = false;
        do {
            var symbolType = getRandInteger(0, 3);
            if (counts[symbolType] === 0) continue;
            --counts[symbolType];
            passwordTemplate.push(symbolType);
            generated = true;
        } while (!generated);
    }

    var password = "";

    passwordTemplate.forEach((el, ind) => {
        var generated = false;
        do {
            var char = generators[el]();
            if (char.toString() === password[ind - 1]) continue;
            password += char;
            generated = true;
        } while (!generated);
    });

    return password;
}

[fnameField, lnameField, bdateField, phoneField, passwordField, emailField].forEach(
    el => el.addEventListener("change", validateForm));

passwordField.addEventListener("keydown", () => document.getElementById("password-generated").style.display = "none");

genPassBut.addEventListener("click",
    function() {
        passwordField.value = generatePassword();
        document.getElementById("password-generated").style.display = "inline";
        document.getElementById("password-generated").innerText = `Password: ${passwordField.value}`;
    });