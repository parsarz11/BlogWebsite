var IsWantToLogin = true
$('#signUpBTN').on('click', function(e){
    $('#RegisterDiv').removeClass('invisible')
    IsWantToLogin = false
})




$('#formSubmit').on('click',function(e){
    var userName = $('#userNameInput').val()
    var password = $('#passwordInput').val()
    var email = $('#emailInput').val()
    var isPersistent = document.querySelector('#isPersistentInput').checked
    
    if(!IsWantToLogin)
    {
        $("#forgetPasswordBTN").AddClass("invisible")
        var registerOBJ = {
            "userName": userName,
            "email": email,
            "password": password,
            "domainName":window.location.origin,
        }
        console.log(registerOBJ)
        $.ajax({
            url: 'https://localhost:7207/api/UsersAccounting/RegisterUser',
            type: 'POST',
            data:JSON.stringify(registerOBJ),
            contentType:'application/json',
        }).done(function (result) {
    
            console.log(result)
            document.cookie = 'token=Bearer '+result
        });
    }
    else{
        $("#forgetPasswordBTN").removeClass("invisible")
        var loginOBJ = {
            
            "userName": userName,
            "password": password,
            "isPersistent": isPersistent
              
        }
        console.log(loginOBJ)
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
    }
})






