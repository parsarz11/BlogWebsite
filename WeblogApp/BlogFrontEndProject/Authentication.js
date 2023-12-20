$("#submitBTN").on('click',function (e) {
    var codeInput = document.querySelector("#codeInput").value
    var isPersistent = document.querySelector('#isPersistentInput').checked

    $.ajax({
        url: 'https://localhost:7207/api/UsersAccounting/Login',
        type: 'POST',
        data:JSON.stringify(loginOBJ),
        contentType:'application/json',
    }).done(function (result) {
        
        document.cookie = 'token=Bearer '+result
        console.log(result)

        window.location.replace("./index.html");
    });
})