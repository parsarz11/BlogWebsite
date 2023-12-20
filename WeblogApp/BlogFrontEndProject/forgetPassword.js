$('#forgetPasswordBTN').on('click', function(e){
    
    forgetPassword()
})



function forgetPassword() {


    var email = $('#emailInput').val()
    const forgetPasswordOBJ = {
        "email" : email
    }
    forgetPassword
        $.ajax({
            url: 'https://localhost:7207/api/UsersAccounting/forgetPassword',
            type: 'POST',
            data:JSON.stringify(forgetPasswordOBJ),
            contentType:'application/json',
        }).done(function (result) {
            

            console.log(result)

            window.location.replace("./index.html");
        });

}