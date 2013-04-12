var app = app || {};

app.MainViewModel = new QuestionListViewModel();

$(function () {
    ko.applyBindings(app.MainViewModel);
});