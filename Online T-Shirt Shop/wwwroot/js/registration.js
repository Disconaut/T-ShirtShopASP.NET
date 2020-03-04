var registration = document.getElementById("regBut");

function validateEmail() {
    var emailRegex = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i);
    var emailAddress = document.getElementById("email").value;
    var valid = emailRegex.test(emailAddress);
    if (!valid) {
        alert("Invalid e-mail address.");
    } else {

    }
}

function validatePhoneNumb() {
    var phoneRegex = new RegExp(/^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$/);
    var phoneNumber = document.getElementById("phoneNumb").value;
    var valid = phoneRegex.test(phoneNumber);
    if (!valid) {
        alert("Invalid phone number.");
    } else {
    }
}
registration.addEventListener("click", validateEmail);
registration.addEventListener("click", validatePhoneNumb);