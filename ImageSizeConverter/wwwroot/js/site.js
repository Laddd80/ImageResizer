// Write your Javascript code.
const slider = document.querySelector("#slider");
const sliderText = document.querySelector("#slider-value");
const heigthWidthText = document.querySelector("#cb-text");
const sizeCheckBox = document.querySelector("#size-cb");

function SliderDrag() {
    sliderText.value = slider.value;
}

function NumberEntered() {
    slider.value = sliderText.value;
}

function CheckBox() {
    if (heigthWidthText.innerHTML === "HEIGHT") {
        heigthWidthText.innerHTML = "WIDTH"
        sizeCheckBox.checked = true;
    } else {
        heigthWidthText.innerHTML = "HEIGHT"
        sizeCheckBox.checked = false;
    }
}