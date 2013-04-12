function Question(data) {
    var self = this;

    this.Id = data.Id;
    this.UserName = ko.observable(data.UserName);
    this.Text = ko.observable(data.Text);
    this.IsClosed = ko.observable(data.IsClosed);
    this.Status = ko.observable(self.IsClosed() ? 'Closed' : 'Opened');
}

function Answer(data) {
    this.Id = data.Id;
    this.QuestionId = data.QuestionId;

    this.UserName = ko.observable(data.UserName);
    this.Text = ko.observable(data.Text);
}