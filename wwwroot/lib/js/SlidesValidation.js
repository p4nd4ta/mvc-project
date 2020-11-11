$(document).ready(function () {
    var textbox = $("#TextBoxContainer");
    var AddButton = $("#AddInput");
    var buttonDelete = $(".delete-btn");
    if (document.getElementById("UrlCounter").value != 0){
        var index = document.getElementById("UrlCounter").value;
    } else {
        var index = 1;
    }

    var submitButton = $("#sub");
    var PreviewButton = $("#preview");

    AddButton.click(function (e) {
        e.preventDefault();
        var label = document.createElement("label");
        label.classList.add("control-label","input-group");
        var labeltext = document.createTextNode(`Slide ${parseInt(index)+1}`);
        label.appendChild(labeltext);
        var input = document.createElement("input");
        input.classList.add("form-control");
        input.setAttribute("id", `UrlsArr_${index}_`);
        input.setAttribute("name", `UrlsArr[${index}]`);
        label.appendChild(input);
        index++;
        textbox.append(label);
    });

        buttonDelete.on("click", function () {
            if (index > 1) {
                index--;
                $(`#UrlsArr_${index}_`).parent().remove();
            }
        });

    submitButton.click(function (e) {
        document.getElementById("UrlCounter").value = index;
        if (($(".validation-checker").val.length == 0) || !( $(".validation-checker").val().replace(/\s/g, '').length ) ){
            $(".validation-checker").addClass("is-invalid");
            return false;
        }
        else {
            return true;
            $("#CreateProductForm").submit();
        }
    });

    PreviewButton.click(function (e) {
        document.getElementById("UrlCounter").value = index;
        if (($(".validation-checker").val.length == 0) || !($(".validation-checker").val().replace(/\s/g, '').length)) {
            $(".validation-checker").addClass("is-invalid");
            return false;
        }
        else {
            var prevAction = $("#CreateProductForm").attr('action');
            console.log(prevAction);
            $("#CreateProductForm").attr('action', '/Admin/Preview');
            $("#CreateProductForm").attr('target', '_blank');
            $("#CreateProductForm").submit();
            $("#CreateProductForm").attr('action', `${prevAction}`);
            $("#CreateProductForm").removeAttr('target');
            return true;
        }
    });
});