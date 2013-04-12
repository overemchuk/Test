function AnswerListViewModel(question, answers) {
    // Data
    var self = this;

    self.Question = question;
    self.UserName = ko.observable("");
    self.NewAnswerText = ko.observable("");
    self.Status = ko.observable(self.Question.IsClosed() ? 'Closed' : 'Opened');

    self.Answers = ko.observableArray(answers);

    // Operations

    self.closeQuestion = function() {
        $.ajax({
            type: "POST",
            data: JSON.stringify({ questionId: self.Question.Id }),
            contentType: "application/json; charset=utf-8",
            url: "Home.aspx/CloseQuestion",
            success: function(data) {
                self.Status("Closed");
            },
            error: function(data) {
                alert('Error!');
            },
            dataType: "json"
        });
    };

    self.addAnswer = function () {
        if (self.NewAnswerText().trim().length < 1 ||
            self.UserName().trim().length < 1) {
            alert('User Name and Text a required!');
            
            return;
        }

        $.ajax({
            type: "POST",
            data: JSON.stringify({ questionId: self.Question.Id, text: self.NewAnswerText(), userName: self.UserName() }),
            contentType: "application/json; charset=utf-8",
            url: "Home.aspx/AddAnswer",
            success: function (data) {
                self.Answers.push(new Answer({
                    Id: data.d,
                    QuestionId: self.Question.Id,
                    Text: self.NewAnswerText(),
                    UserName: self.UserName()
                }));

                self.NewAnswerText("");
                self.UserName("");
            },
            error: function (data) {
                alert('Error!');
            },
            dataType: "json"
        });
    };

    self.goBackToHomePage = function () {
        $('#dvQuestionDetailsContainer').hide();
        $('#dvSpaContainer').show();
    };
}

function QuestionListViewModel() {
    // Data
    var self = this;

    self.UserName = ko.observable("");
    self.AllQuestions = [];
    self.Questions = ko.observableArray([]);
    self.NewQuestionText = ko.observable("");

    // Operations
    self.addQuestion = function () {
        if (self.NewQuestionText().trim().length < 1 ||
            self.UserName().trim().length < 1) {
            alert('User Name and Text a required!');

            return;
        }

        $.ajax({
            type: "POST",
            data: JSON.stringify({ text: self.NewQuestionText(), userName: self.UserName() }),
            contentType: "application/json; charset=utf-8",
            url: "Home.aspx/AddQuestion",
            success: function (data) {
                self.Questions.push(new Question({
                    Id: data.d,
                    Text: self.NewQuestionText(),
                    UserName: self.UserName(),
                    IsClosed: false
                }));

                self.UserName("");
                self.NewQuestionText("");
            },
            error: function (data) {
                alert('Error!');
            },
            dataType: "json"
        });
    };

    self.removeQuestion = function (question) {
        $.ajax({
            type: "POST",
            data: JSON.stringify({ questionId: question.Id }),
            contentType: "application/json; charset=utf-8",
            url: "Home.aspx/DeleteQuestion",
            success: function (data) {
                self.AllQuestions.pop(question);

                self.Questions.remove(question);
            },
            error: function (data) {
                alert('Error!');
            },
            dataType: "json"
        });
    };

    self.getQuestionDetails = function(question) {
        $.ajax({
            type: "POST",
            data: JSON.stringify({ questionId: question.Id }),
            contentType: "application/json; charset=utf-8",
            url: "Home.aspx/GetQuestionDetails",
            success: function (data) {
                $('#dvQuestionDetailsContainer').show();
                $('#dvSpaContainer').hide();

                var jsonData = $.parseJSON(data.d);

                ko.cleanNode($('#dvQuestionDetailsContainer')[0]);

                $('#dvQuestionDetailsContainer').html(jsonData.template);

                var answers = [];

                for (var i = 0; i < jsonData.answers.length; i++) {
                    answers.push(new Answer(jsonData.answers[i]));
                }

                ko.applyBindings(new AnswerListViewModel(question, answers), $('#dvQuestionDetailsContainer')[0]);
            },
            error: function(data) {
                alert('Error!');
            },
            dataType: "json"
        });
    };

    self.filterQuestions = function(filterType) {
        self.Questions(ko.utils.arrayFilter(self.AllQuestions, function (question) {
            switch (filterType) {
            case "All":
                return true;
            case "Opened":
                return !question.IsClosed();
            case "Closed":
                return question.IsClosed();
            default:
                return true;
            }
        }));
    };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Home.aspx/GetAllQuestions",
        success: function (data) {
            var jsonData = $.parseJSON(data.d);

            for (var i = 0; i < jsonData.length; i++) {
                self.AllQuestions.push(new Question(jsonData[i]));
            }

            self.filterQuestions("All");
        },
        error: function (data) {
            alert('Error!');
        },
        dataType: "json"
    });

    $('#lstQuestionsFilter').change(function () {
        self.filterQuestions($(this).val());
    });
}