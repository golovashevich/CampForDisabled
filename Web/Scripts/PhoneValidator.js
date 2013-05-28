function testPhoneValidation() {
    return true; 
}


function testPhoneValidator_Negative() {
    return false;
}


jQuery.validator.addMethod('phonenumber', function (value, element, params) {
    return isValidPhone(value);
}, '');


jQuery.validator.unobtrusive.adapters.add('phonenumber', [], function (options) {
    options.rules['phonenumber'] = {};
    if (options.message) {
        options.messages['phonenumber'] = options.message;
    }
});


function isValidPhone(phone) {
    var MIN_DIGITS_COUNT = 5;
    var MAX_DIGITS_COUNT = 20;
    var ALLOWED_WASTE_SYMBOLS = /\s|\(|\)|-|\—|\#|_/g;
    var ALLOWED_SYMBOLS = /^( +)?\+?[- 0-9#()]+?$/;

    if (typeof phone == "undefined") {
        return true;
    }

    if (phone.replace(/\s/g, "") == "") {
        return true;
    }

    phone = phone.replace(ALLOWED_WASTE_SYMBOLS, "");

    if (!ALLOWED_SYMBOLS.test(phone)) {
        return false;
    }

    var onlyDigitsPhone = phone.replace(/[^\d]/g, "").toString();
    if (phone.charAt(0) != "+") {
        if (onlyDigitsPhone.length < MIN_DIGITS_COUNT || onlyDigitsPhone.length > MAX_DIGITS_COUNT) {
            return false;
        }
    } else {
        if (onlyDigitsPhone.length != 11 && onlyDigitsPhone.length != 12) {
            return false;
        }
    }

    return true;
}