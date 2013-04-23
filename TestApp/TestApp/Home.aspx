<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TestApp.Home" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="dvSpaContainer">
        <div class="container">
            <span>Create question:</span>
            <div>
                <input data-bind="value: UserName" placeholder="User Name" />
            </div>
            <div>
                <input data-bind="value: NewQuestionText" placeholder="Ask a question" />
            </div>
            <div>
                <button type="button" data-bind="click: addQuestion">Add</button>
            </div>
        </div>
        <div class="container">
            <h3>Questions</h3>
        
            <span>Filter:</span>
            <select id="lstQuestionsFilter">
                <option value="All">All</option>
                <option value="Opened">Opened</option>
                <option value="Closed">Closed</option>
            </select>

            <ul data-bind="foreach: Questions, visible: Questions().length > 0">
                <li>
                    <div>
                        <span>User Name:</span>
                        <span data-bind="text: UserName"></span>
                    </div>
                    <div>
                        <span>Status:</span>
                        <span data-bind="text: Status"></span>
                    </div>
                    <div>
                        <div>Text:</div>
                        <div data-bind="text: Text"></div>
                    </div>
                    <a href="#" data-bind="click: $parent.removeQuestion">Delete</a> | 
                    <a href="#" data-bind="click: $parent.getQuestionDetails, attr: { href: '#question-details-' + Id }">Details</a>
                </li> 
            </ul>
        </div>
    </div>
    <div id="dvQuestionDetailsContainer"></div>
<div></div>
</asp:Content>