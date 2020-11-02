$(document).ready(function () {

	$("#NewCommentText").keyup(function () {

		var characterCount = $(this).val().length;

		$("#char-count").html(characterCount);
		if (characterCount <= 100) {
			$("#char-count").removeClass();
			$("#char-count").addClass("text-success");
		} else if (characterCount > 100 && characterCount < 190) {
			$("#char-count").removeClass();
			$("#char-count").addClass("text-warning");
		} else if (characterCount >= 190) {
			$("#char-count").removeClass();
			$("#char-count").addClass("text-danger");
		}
	});
});