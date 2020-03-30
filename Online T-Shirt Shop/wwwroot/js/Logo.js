function animate({ timing, draw, duration }) {

    let start = window.performance.now();

    requestAnimationFrame(function animate(time) {
        let timeFraction = (time - start) / duration;
        if (timeFraction > 1) timeFraction = 1;

        let progress = timing(timeFraction);

        draw(progress);

        if (timeFraction < 1) {
            requestAnimationFrame(animate);
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
}

var bounceOut = makeEaseOut(bounce);

function Logo(canvas, logoText) {
    const width = canvas.width;
    const height = canvas.height;
    const text = logoText;

    const spaceForLetter = width / text.length;
    var letterTiming = bounceOut;

    const context = canvas.getContext("2d");
    context.font = "bold 27px Arial";
    context.fillStyle = getComputedStyle(document.querySelector(":root")).getPropertyValue("--first-color");
    context.textAlign = "center";
    context.textBaseline = "middle";

    this.setTextColor = function(color) {
        context.fillStyle = color;
    }

    this.setLetterTiming = function (timing) {
        letterTiming = timing;
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
            letterProgress = letterTiming(letterProgress);
            context.fillText(text[i],
                spaceForLetter * i + spaceForLetter / 2,
                startOfLetter + (traceLength * letterProgress));
        }
    }
}

function AnimatedLogo(canvas, logoText, letterTiming) {
    Logo.call(this, canvas, logoText);
    this.setLetterTiming(letterTiming);

    this.setLetterTiming = undefined;

    var animationArgs = { timing: (x) => x, draw: this.draw };

    this.draw = function (duration) {
        animationArgs.duration = duration;
        animate(animationArgs);
    };
}