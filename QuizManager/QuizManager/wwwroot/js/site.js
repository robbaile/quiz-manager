var index = (function () {
  function initQuiz() {
    $('#Quiz-Form').on('submit', (e) => {
      e.preventDefault();
      console.log("Posting");
      let questionIds = $("[name='QuestionId']");
      let answers = $("[name='Answer']")
      let answerModel = {
        "QuizId": 1,
        "AnswersToCheck": []
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

      let token = $('input[name="__RequestVerificationToken"]').val();

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
    });
  }

  function initCreateQuiz() {
    $('#Add-Question-Button').on('click', (e) => {
      e.preventDefault();

      let question = $('#Question').val();
      let correctAnswer = $('#Correct-Answer').val();
      let wrongAnswer1 = $('#Wrong-Answer-1').val();
      let wrongAnswer2 = $('#Wrong-Answer-2').val();
      let wrongAnswer3 = $('#Wrong-Answer-3').val();


      if (question == "" || correctAnswer == "" || wrongAnswer1 == "" || wrongAnswer2 == "" || wrongAnswer3 == "") {
        document.getElementById("Add-Question-Error-Text").innerHTML = `<p>Please fill in all fields</p>`
      } else {
        document.getElementById("Add-Question-Error-Text").innerHTML = "";

        let questionJson = {
          "Question": question,
          "CorrectAnswer": correctAnswer,
          "WrongAnswers": [wrongAnswer1, wrongAnswer2, wrongAnswer3]
        };

        document.getElementById('Added-Questions').innerHTML += `
    <div>
      <p>Question: ${question}</p>
      <p>Correct Answer: ${correctAnswer}</p>
      <p>Wrong Answers: ${wrongAnswer1}, ${wrongAnswer2}, ${wrongAnswer3}</p>
      <input class="question-json" type="hidden" value='${JSON.stringify(questionJson)}'  />
    </div`;
        document.getElementById('Question').value = "";
        document.getElementById('Correct-Answer').value = "";
        document.getElementById('Wrong-Answer-1').value = "";
        document.getElementById('Wrong-Answer-2').value = "";
        document.getElementById('Wrong-Answer-3').value = "";
      }
    });

    $('#Create-Quiz-Button').on('click', (e) => {
      e.preventDefault();

      let title = $('input[name="Title"]').val();
      let token = $('input[name="__RequestVerificationToken"]').val();
      let questions = $('.question-json');
      let submitModel = {
        "Title": title,
        "NewQuestions": []
      };

      for (var i = 0; i < questions.length; i++) {
        submitModel.NewQuestions.push(JSON.parse(questions[i].value))
      };

      if (questions.length == 0) {
        document.getElementById("Add-Question-Error-Text").innerHTML = `<p>Please add a question to the quiz</p>`
      } else if (title == "") {
        document.getElementById("Add-Question-Error-Text").innerHTML = `<p>Please add a title for the quiz</p>`
      } else {
        $.ajax({
          type: "POST",
          url: "/Quiz/Create",
          contentType: 'application/json',
          beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", token);
          },
          data: JSON.stringify(submitModel),
          success: function (res) {
            $('#Quiz-Body').html(`
          <p>${res}</p>
          <a href='/Home/Index'>Return home</a>
          <p>or</p>
          <a href='/Quiz/New'>Add Another</a>
        `)
          },
          dataType: "json"
        })
      }
    });
  }

  function initEditQuestion() {
    $('#Edit-Question-Form').on('submit', (e) => {
      e.preventDefault();

      let quizId = $('#Quiz-Id').val();
      let questionId = $('#Question-Id').val();
      let questionText = $('input[name="Question"]').val();
      let correctAnswer = $('input[name="CorrectAnswer"]').val();
      let token = $('input[name="__RequestVerificationToken"]').val();

      let data = {
        "QuizId": parseInt(quizId),
        "QuestionId": parseInt(questionId),
        "Question": {
          "Id": parseInt(questionId),
          "QuestionText": questionText,
          "CorrectAnswer": correctAnswer,
          "WrongAnswers": []
        }
      };

      let wrongAnswers = $('.input__wrong-answer');

      for (var i = 0; i < wrongAnswers.length; i++) {
        data.Question.WrongAnswers.push(wrongAnswers[i].value);
      };

      console.log(data);

      $.ajax({
        type: "POST",
        url: "/Quiz/EditQuestion",
        contentType: 'application/json',
        beforeSend: function (request) {
          request.setRequestHeader("RequestVerificationToken", token);
        },
        data: JSON.stringify(data),
        success: function (res) {
          if (res == true) {
            window.location.href = `/Quiz/Edit?id=${quizId}`
          } else {
            window.location.href = `/Quiz/Edit?id=${quizId}&questionId=${questionId}`
          }
        },
        dataType: "json"
      })

    })
  };

  function initCreateQuestion() {
    $('#Create-Question-Form').on('submit', (e) => {
      e.preventDefault();

      let quizId = $('input[name="QuizId"]').val(); 
      let question = $('input[name="Question"]').val();
      let correctAnswer = $('input[name="CorrectAnswer"]').val();
      let token = $('input[name="__RequestVerificationToken"]').val();
      let wrongAnswer1 = $('input[name="WrongAnswers-1"]').val();
      let wrongAnswer2 = $('input[name="WrongAnswers-2"]').val();
      let wrongAnswer3 = $('input[name="WrongAnswers-3"]').val();

      if (quizId == "" || question == "" || correctAnswer == "" || token == "" || wrongAnswer1 == "" || wrongAnswer2 == "" || wrongAnswer3 == "") {
        $('.error-text').html("<p>Please fill in all fields before submitting</p>");
      } else {
        let data = {
          "QuizId": quizId,
          "Question": question,
          "CorrectAnswer": correctAnswer,
          "WrongAnswers": [wrongAnswer1, wrongAnswer2, wrongAnswer3]
        };

        $.ajax({
          type: "POST",
          url: "/Quiz/CreateQuestion",
          contentType: 'application/json',
          beforeSend: function (request) {
            request.setRequestHeader("RequestVerificationToken", token);
          },
          data: JSON.stringify(data),
          success: function (res) {
            if (res == true) {
              let quizId = $('input[name="QuizId"]').val();
              window.location.href = `/Quiz/Edit?id=${quizId}`
            } else {
              $('.error-text').html(`<p>We could not save your new question. Please try again</p>`);
            }
          },
          dataType: "json"
        })
      }
    })
  }

  return {
    initQuiz: initQuiz,
    initCreateQuiz: initCreateQuiz,
    initEditQuestion: initEditQuestion,
    initCreateQuestion: initCreateQuestion
  }
})();