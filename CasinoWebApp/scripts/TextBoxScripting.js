// This script formats the text boxes of a web-form on state.
function clearPlaceholder(defaultText, textBoxControl) {
    if (textBoxControl.value == defaultText) {
        textBoxControl.value = "";
        textBoxControl.style.color = "white"
    }
}

function resetPlaceHolder(defaultText, textBoxControl) {
    if (textBoxControl.value == "") {
        textBoxControl.value = defaultText;
        textBoxControl.style.color = "gray";
    }
}