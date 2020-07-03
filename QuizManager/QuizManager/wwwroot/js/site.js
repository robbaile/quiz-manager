// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#Quiz-Form').on('submit', (e) => {
  e.preventDefault();
  console.log("Posting");
  var questionIds = $("[name='QuestionId']");
  var answers = $("[name='Answer']")
  var answerModel = {
    "QuizId" : 1,
    "AnswersToCheck" : []
  }

  for (var i = 0; i < questionIds.length; i++) {
    let questionId = $(questionIds[i]).val();
    let answer = answers[i].value;

    let result = {
      "QuestionId": questionId,
      "Answer": answer
    }

    answerModel.AnswersToCheck.push(result);
  }

  var token = $('input[name="__RequestVerificationToken"]').val();

  $.ajax({
    type: "POST",
    url: "/Quiz/Results",
    contentType: 'application/json',
    beforeSend: function (request) {
      request.setRequestHeader("RequestVerificationToken", token);
    },
    data: JSON.stringify(answerModel),
    success: function (res) {
      $('#Quiz-Body').html(`
        <p>You have scored</p>
        <p>${res.score}</p>
        <p>Out of a possible</>
        <p>${res.total}</p>
        <a href='/Home/Index'>Return home</a>
        <p>or</p>
        <a href='/Quiz?id=${res.quizId}'>Try again</a>
      `)
    },
    dataType: "json"
  })
})