$(document).ready(function () {
    var textbox = $("#TextBoxContainer");
    var AddButton = $("#AddInput");
    var index = 1;
    var submitButton = $("#sub");

    AddButton.click(function (e) {
        e.preventDefault();
        var label = document.createElement("label");
        label.classList.add("control-label");
        var labeltext = document.createTextNode(`Slide ${index+1}`);
        label.appendChild(labeltext);
        var input = document.createElement("input");
        input.classList.add("form-control");
        input.setAttribute("id", `UrlsArr_${index}_`);
        input.setAttribute("name", `UrlsArr[${index}]`);
        label.appendChild(input);
        index++;
        var buttonDelete = document.createElement("button");
        buttonDelete.classList.add("delete-btn","btn","btn-danger");
        buttonDelete.addEventListener("click", function (e) {
            e.preventDefault();
            var labelContainer = e.target.parentElement;
            console.log(labelContainer);
            labelContainer.remove();
        });
        var deletetext = document.createTextNode("Remove");
        buttonDelete.appendChild(deletetext);
        label.appendChild(buttonDelete);
        textbox.append(label);
    });

    submitButton.click(function (e) {
        document.getElementById("UrlCounter").value = index; 
        $("#CreateProductForm").submit();
    });
});