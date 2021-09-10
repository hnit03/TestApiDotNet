function signOut() {
    localStorage.clear();
    window.location.href = 'login.html';
}

$(document).ready(function(){
    $("#myTab a").click(function(e){
        e.preventDefault();
        $(this).tab('show');
    });
});

function SelectORM(){
    var orm = document.getElementById("select-orm").value;
}