const queryString = window.location.search;
        const urlParams = new URLSearchParams(queryString);
        const id = urlParams.get('userId')
        const token = urlParams.get('token')
        const tokenList = location.search.substring(1).split("token=")


        confirmEmailOBJ = {
            "userId":id,
            "token":tokenList[1],
        }
        $.ajax({
        url: `https://localhost:7207/api/UsersAccounting/confirmEmail`,
        type: 'POST',
        contentType:"application/json; charset=utf-8",
        data:JSON.stringify(confirmEmailOBJ),
        }).done(function (result) {
            console.log(result)
        })