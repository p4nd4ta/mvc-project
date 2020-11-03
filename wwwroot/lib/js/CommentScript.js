function CheckValidationComment() {
    var input = document.getElementById("NewCommentText");
    if (input.value.length == 0) {
        input.classList.add('is-invalid');
        return false;
    }
    else if (input.value.length > 200) {
        input.classList.add('is-invalid');
        var validation = document.getElementById("comment-validation");
        validation.innerText = "Comment cannot exceed 200 symbols !";
        return false;
    } else { document.getElementById("form-for-newComment").submit(); }
}