const cookieValue = document.cookie.split("; ").find((row) => row.startsWith("token="))?.split("=")[1];
if(cookieValue == '')
{
    window.location.replace("./loginPage.html");
}
if(cookieValue != '')
{
    $('#loginBTN').addClass('invisible')
}




const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const id = urlParams.get('id')
var isWantToUpdate = false

if(urlParams.size > 0)
{
    isWantToUpdate = true

    $.ajax({
    url: `https://localhost:7207/api/Category/GetCategoryById?id=${id}`,
    type: 'GET',
    }).done(function (result) {
        
        console.log(result)

        document.querySelector("#categoryInput").value = String(result.name)

    })
}
$('button').on('click',function(e){
    
    var categoryInput = $('#categoryInput').val()
    if (isWantToUpdate) {

        
        const categoryOBJ = {
            id: id,
            name: categoryInput
        }
        
        $.ajax({
        url: 'https://localhost:7207/api/Category/EditCategory',
        type: 'POST',
        method: 'POST',
        contentType:'application/json; charset=utf-8',
        data:JSON.stringify(categoryOBJ),
        headers: {"Authorization": cookieValue}
        }).done(function(result) {

            location.reload()
        });
    }
    else{

    
        
        const categoryOBJ = {
            name: categoryInput
        }
        
        $.ajax({
        url: 'https://localhost:7207/api/Category/AddCategory',
        type: 'POST',
        method: 'POST',
        contentType:'application/json; charset=utf-8', 
        data:JSON.stringify(categoryOBJ),
        headers: {"Authorization": cookieValue}
        }).done(function(result) {

            location.reload()
        });
}
})