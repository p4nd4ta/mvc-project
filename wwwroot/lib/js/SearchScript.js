function CheckValidationSearch() {
var input = document.getElementById("searchTerm");
    if ((input.value.length == 0) || !(input.value.replace(/\s/g, '').length) ) {
        input.classList.add('is-invalid');
        return false;
    }
    else {
        return true;
    }
}