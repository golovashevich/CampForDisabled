jQuery.validator.addMethod('numericlessthan', function (value, element, params) {
    value = value.trim();
    var otherValue = $(params.element).val().trim();

    return isNaN(value) && isNaN(otherValue)
        || "" == value || "" == otherValue //skip comparison if one of values is empty
        || (params.allowequality === 'True'
            ? parseFloat(value) <= parseFloat(otherValue)
            : parseFloat(value) < parseFloat(otherValue));
}, '');


jQuery.validator.unobtrusive.adapters.add('numericlessthan', ['other', 'allowequality'], function (options) {
    var prefix = options.element.name.substr(0, options.element.name.lastIndexOf('.') + 1),
    other = options.params.other,
    fullOtherName = appendModelPrefix(other, prefix),
    otherElement = $(options.form).find(':input[name=' + fullOtherName + ']')[0];

    options.rules['numericlessthan'] = {
        allowequality: options.params.allowequality,
        element: otherElement };
    if (options.message) {
        options.messages['numericlessthan'] = options.message;
    }
});


jQuery.validator.addMethod('numericcoupled', function (value, element, params) {
    $(params.element).valid();
    $(element).removeClass("input-validation-error");
    return true;
}, '');


jQuery.validator.unobtrusive.adapters.add('numericcoupled', ['other'], function (options) {
    var prefix = options.element.name.substr(0, options.element.name.lastIndexOf('.') + 1),
    other = options.params.other,
    fullOtherName = appendModelPrefix(other, prefix),
    otherElement = $(options.form).find(':input[name=' + fullOtherName + ']')[0];

    options.rules['numericcoupled'] = { element: otherElement };
    if (options.message) {
        options.messages['numericcoupled'] = options.message;
    }
});


function appendModelPrefix(value, prefix) {
    if (value.indexOf('*.') === 0) {
        value = value.replace('*.', prefix);
    }
    return value;
}


//Modifies standard jQuery number validator so that it ignores preceeding and trailing spaces
$.extend($.validator.methods, {
    number: function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(value.trim());
    }
});


jQuery.validator.addMethod('phonedigitscountrange', function (value, element, params) {
    var dig_count = value.replace(/[^\d.]/g, "").length;

    return "" == value //skip comparison if one of values is empty
        || (dig_count >= params.min && dig_count <= params.max);
}, '');


jQuery.validator.unobtrusive.adapters.add('phonedigitscountrange', ['min', 'max'], function (options) {
    options.rules['phonedigitscountrange'] = { min: options.params.min, max: options.params.max };
    if (options.message) {
        options.messages['phonedigitscountrange'] = options.message;
    }
});


jQuery.validator.addMethod('phonenumberwithplusvalidator', function (value, element, params) {
    if (value == "" || !/^( +)?\+([^\+]+)?$/.test(value)) {
        return true;
    }
    var dig_count = value.replace(/[^\d.]/g, "").length;
    return dig_count == 11 || dig_count == 12;

}, '');


jQuery.validator.unobtrusive.adapters.add('phonenumberwithplusvalidator', [], function (options) {
    options.rules['phonenumberwithplusvalidator'] = {};
    if (options.message) {
        options.messages['phonenumberwithplusvalidator'] = options.message;
    }
});

