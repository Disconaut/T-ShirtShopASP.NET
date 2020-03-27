var salesChart = document.getElementById("sales-chart");
var upperBound = document.getElementById("upperbound");

function drawHistogram(chartCanvas, data, upperBound) {
    const chartCtx = chartCanvas.getContext("2d");
    const colMargin = Math.round(chartCanvas.width / data.length / 12 / 2);
    console.log(colMargin);
    const colWidth = Math.round(chartCanvas.width / data.length - colMargin * 2);
    console.log(colWidth);
    const pxPerCount = Math.round(chartCanvas.height / upperBound);
    const chartWidth = chartCanvas.width;
    const chartHeight = chartCanvas.height;
    chartCtx.fillStyle = "#ff6800";

    data.forEach((el, ind) => {
        var startX = ind * colMargin + (ind + 1) * colMargin + ind * colWidth;
        var height = el * pxPerCount;
        var startY = chartHeight - height;

        chartCtx.rect(startX, startY, colWidth, height);
    });

    chartCtx.fill();
}

var salesData = [9.5, 4, 3.43, 2.23, 6.23, 8.87, 7.94, 6.56, 8.9, 5.6, 7.86, 6.31];
var upper = Math.round(Math.max(...salesData));

drawHistogram(salesChart, salesData, upper);
upperBound.innerText = upper + "K";