function animate({ timing, draw, duration, callback }) {

    let start = window.performance.now();

    requestAnimationFrame(function animate(time) {
        let timeFraction = (time - start) / duration;
        if (timeFraction > 1) timeFraction = 1;

        let progress = timing(timeFraction);

        draw(progress);

        if (timeFraction < 1) {
            requestAnimationFrame(animate);
        } else {
            if (callback !== undefined) {
                callback();
            }
        }

    });
}

function makeEaseOut(timing) {
    return function (timeFraction) {
        return 1 - timing(1 - timeFraction);
    }
}

function bounce(timeFraction) {
    for (let a = 0, b = 1, result; 1; a += b, b /= 2) {
        if (timeFraction >= (7 - 4 * a) / 11) {
            return -Math.pow((11 - 6 * a - 11 * timeFraction) / 4, 2) + Math.pow(b, 2);
        }
    }
    return undefined;
}

function back(x, timeFraction) {
    return Math.pow(timeFraction, 2) * ((x + 1) * timeFraction - x);
}

function makeUnary(func, arg1) {
    return function(arg2) {
        return func(arg1, arg2);
    }
}

var bounceOut = makeEaseOut(bounce);

function Logo(canvas, logoText) {
    const width = canvas.width;
    const height = canvas.height;
    const text = logoText;

    const spaceForLetter = width / text.length;
    var letterInTiming = (x) => x;
    var letterOutTiming = (x) => x;

    const context = canvas.getContext("2d");
    context.font = "bold 27px Arial";
    context.fillStyle = getComputedStyle(document.querySelector(":root")).getPropertyValue("--first-color");
    context.textAlign = "center";
    context.textBaseline = "middle";

    this.setTextColor = function (color) {
        context.fillStyle = color;
    }

    this.setFont = function(font)
    {
        context.font = font;
    }

    this.setLetterInTiming = function (timing) {
        letterInTiming = timing;
    }

    this.setLetterOutTiming = function (timing) {
        letterOutTiming = timing;
    }

    this.draw = function (progress = 1) {
        context.clearRect(0, 0, width, height);

        const durationForLetter = 0.6;
        const delayBetweenLetters = 0.1;
        const startOfLetter = -50;
        const traceLength = height / 2 - startOfLetter;

        for (let i = 0; i < logoText.length; ++i) {
            let letterProgress = (progress - delayBetweenLetters * i) / durationForLetter;
            if (letterProgress <= 0) break;
            else if (letterProgress > 1) letterProgress = 1;
            letterProgress = letterInTiming(letterProgress);
            context.fillText(text[i],
                spaceForLetter * i + spaceForLetter / 2,
                startOfLetter + (traceLength * letterProgress));
        }
    }

    this.hideLogo = function (progress = 1) {
        context.clearRect(0, 0, width, height);

        const durationForLetter = 0.6;
        const delayBetweenLetters = 0.1;
        const endOfLetter = -50;
        const traceLength = height / 2 - endOfLetter;

        for (let i = 0; i < logoText.length; ++i) {
            let letterProgress = (progress - delayBetweenLetters * i) / durationForLetter;
            if (letterProgress <= 0) letterProgress = 0;
            else if (letterProgress > 1) letterProgress = 1;
            letterProgress = letterOutTiming(letterProgress);
            context.fillText(text[i],
                spaceForLetter * i + spaceForLetter / 2,
                height / 2 - (traceLength * letterProgress));
        }
    }
}

function AnimatedLogo(canvas, logoText, letterInTiming, letterOutTiming) {
    Logo.call(this, canvas, logoText);
    this.setLetterInTiming(letterInTiming);
    this.setLetterOutTiming(letterOutTiming);

    this.setLetterInTiming = undefined;
    this.setLetterOutTiming = undefined;

    var drawAnimationArgs = { timing: (x) => x, draw: this.draw };

    this.draw = function (duration, callback = undefined) {
        drawAnimationArgs.duration = duration;
        drawAnimationArgs.callback = callback;
        animate(drawAnimationArgs);
    };

    var hideAnimationArgs = { timing: (x) => x, draw: this.hideLogo };

    this.hideLogo = function (duration, callback = undefined) {
        hideAnimationArgs.duration = duration;
        hideAnimationArgs.callback = callback;
        animate(hideAnimationArgs);
    }
}

function makeWithoutParameter(func, ...args) {
    return function() {
        return func(args);
    }
}

var preloaderLogo = new AnimatedLogo($(".preloader-overlay > canvas.logo-loader")[0], "VETOL", bounceOut, makeUnary(back, 2.5));
preloaderLogo.setTextColor("#ffffff");
preloaderLogo.setFont("bold 52px Roboto");

var loaderInterval = setInterval(function() {
        preloaderLogo.draw(2500, makeWithoutParameter(preloaderLogo.hideLogo, 1000));
    },
    3500);

$(window).on("load", function () {
    $(".preloader-overlay > canvas.logo-loader").fadeOut().stop().delay(400).fadeOut('slow');
    $(".preloader-overlay").fadeOut().stop().delay(400).fadeOut('slow');
    clearInterval(loaderInterval);
})