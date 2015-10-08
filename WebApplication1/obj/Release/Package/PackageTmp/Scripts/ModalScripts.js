$("#submit-button").click(function () {
    if ($("#num1").val() == '' || $("#num2").val() == '' || $("#num3").val() == '' || $("#num4").val() == ''
        || $("#num5").val() == '' || $("#num6").val() == '') {
        alert("Please input 6 numbers");
    } else {
        var max = Math.max(
                $("#num1").val(),
                $("#num2").val(),
                $("#num3").val(),
                $("#num4").val(),
                $("#num5").val(),
                $("#num6").val()
            );
        $("#result").val(max);
    }
});

$("#sum-button").click(function () {
    var nums = [
            $("#sum1").val(),
            $("#sum2").val(),
            $("#sum3").val(),
            $("#sum4").val(),
            $("#sum5").val(),
            $("#sum6").val()
    ];
    var total = 0;
    for (var i = 0; i < nums.length; i++) {
        if (nums[i] == '') {
            total += 0;
        } else {
            total += parseFloat(nums[i]);
        }
    }
    $("#sum-result").val(total);
});

$("#product-button").click(function () {
    var nums = [
            $("#sum1").val(),
            $("#sum2").val(),
            $("#sum3").val(),
            $("#sum4").val(),
            $("#sum5").val(),
            $("#sum6").val()
    ];
    var total = 1;
    for (var i = 0; i < nums.length; i++) {
        if (nums[i] == '') {
            total *= 1;
        } else {
            total *= parseFloat(nums[i]);
        }
    }
    $("#sum-result").val(total);
});

$("#fact-button").click(function () {
    var numToFact = $("#numToFact").val();
    var total = 1;
    for (var i = numToFact; i > 0; i--) {
        total *= i;
    }
    $("#fact-result").val(total);
});

$("#palin-button").click(function () {
    var stringToCheck = $("#checkPalin").val().toUpperCase();
    var reverse = stringToCheck.split("").reverse().join("");
    var result = stringToCheck === reverse;
    if (result) {
        $("#palin-result").val("This is a palindrome");
    } else {
        $("#palin-result").val("This is NOT a palindrome");
    }
   
});

$("#fizz-button").click(function () {
    var textBox = $("#fizzResult");
    var text = "";
    for (var i = 1; i < 101; i++) {
        if (i % 5 == 0 && i % 3 == 0) {
            text += "FizzBuzz\n";
        } else if (i % 5 == 0) {
            text += "Buzz\n";
        } else if (i % 3 == 0) {
            text += "Fizz\n";
        } else {
            text += i + "\n";
        }
    }
    textBox.text(text);
});