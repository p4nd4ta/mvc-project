$(document).ready(function () {
    var div = $("<div/ >");
    var value = "";
    var textbox = $("#TextBoxContainer");
    var AddButton = $("#AddInput");
    var index = 1;

    AddButton.click(function (e) {
        e.preventDefault();
        index++;
        var label = document.createElement("label");
        label.classList.add("control-label");
        var labeltext = document.createTextNode(`Slide ${index}`);
        label.appendChild(labeltext);
        var input = document.createElement("input");
        input.classList.add("form-control");
        input.setAttribute("name", `UrlsArr[${index}]`);
        label.appendChild(input);
        var buttonDelete = document.createElement("button");
        buttonDelete.classList.add("delete-btn");
        var deletetext = document.createTextNode("Remove");
        buttonDelete.appendChild(deletetext);
        label.appendChild(buttonDelete);
        textbox.append(label);
    });


    var deleteButtons = document.querySelectorAll(".delete-btn");
    deleteButtons(item => {
        item.click(function(e){
            e.preventDefault();
            //var currentLabel = e.target.parrent().remove();
            console.log("asdsadasdasd");
        });
    });


    //var UrlLabel = $("<label>").attr("htmlFrom")
    //var UrlBox = $("<input />").attr("type", "textbox").attr("name", "UrlsArr[" + i + "]");
    //UrlBox.val(value);
    //div.append(UrlBox);

    //var button = $("<input />").attr("type", "button").attr("value", "Remove");
    //div.append("button");
    //$("#TextBoxContainer").append(div);
    //i++;
});

//function RemoveTextBox(button) {
//    $(button).parrent().remove();
//}