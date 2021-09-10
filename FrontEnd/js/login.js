function viewModel() {
    self = this;
    self.userName = ko.observable("");
    self.passWord = ko.observable("");

    self.login = function() {
        $.ajax({
            type: "POST",
            dataType: 'json',
            contentType: "application/json",
            url:"https://localhost:44330/api/account/login",
            data: JSON.stringify({
                userName: self.userName(),
                passWord: self.passWord()
            })
        }).done(function(data) {
            localStorage.setItem("token", data.token);
            localStorage.setItem("username", data.username);
            window.location.href = 'dashboard.html';
        }).catch(function(err) {
            console.log(err);
            alert("Invalid Email Address Or Password!");
        });
    }
}

var vm = new viewModel();
ko.applyBindings(vm);