const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const id = urlParams.get('userId')
console.log("🚀 ~ file: changePassword.js:4 ~ id:", id)
const token = urlParams.get('token')
const tokenList = location.search.substring(1).split("token=")

$("#submitBTN").on("click",function (e) {
    var password = document.querySelector("#passwordInput").value
    var repassword = document.querySelector("#rePasswordInput").value

    if (password  == repassword) {
        
    const resetPasswordOBJ ={
        
        "password": password,
        "token": tokenList[1],
        "userId": id,
    }
    $.ajax({
        method: 'POST',
        url: "https://localhost:7207/api/UsersAccounting/resetPassword",
        contentType:"application/json; charset=utf-8",

        data: JSON.stringify(resetPasswordOBJ),
        //headers: {"Authorization": cookieValue}
        
    }).done(function (result) {
        window.location.replace("./index.html");
    });
    
    }
    else{
        alert("passwords are not same")
    }
})
