//get login token cookie
const cookieValue = document.cookie.split("; ").find((row) => row.startsWith("token="))?.split("=")[1];

//check if exist
if(cookieValue == '')
{
    //invisible signOut button and show login button
    $('#signOutBTN').addClass('invisible')
    $('#loginBTN').removeClass('invisible')
}
if(cookieValue != '')
{
    //invisible login button
    $('#loginBTN').addClass('invisible')
}
window.onload = getCategories()

//this function get categories from api
function getCategories(){



    const baseUrl = 'https://localhost:7207/api/'

    //ajax call for get categories
    $.ajax({
        url: baseUrl+`Category/Getcategories`,
        type: 'GET',
    }).done(function (result) {

        //make row for each category
        $.each(result, function (i, item) {
            CreateTable(item)
        });
        
        
    })
}


//this function create a table body for categories
function CreateTable(result){

    const tRow = `
    <tr>
        <th scope="row"><a href="./category.html?id=${result.id}">${result.id}</a></th>
        <td>${result.name}</td>
        <td><a href="./AddCategory.html?id=${result.id}"><button class="btn btn-primary">update</button></a></td>
        
    </tr>`

    // append table row to table body
    $('#categoriesTable').append(tRow)
}

//this function shows delete category form
$('#ShowDeleteForm').on('click',function(e){
    $('form').removeClass('invisible')
})

//this function will delete caategory
$('#deleteCategory').on('click',function(e){

    var categoryDeleteId = $('#categoryId').val()

    //ajax call for delete categories
    $.ajax({
    url: 'https://localhost:7207/api/Category/DeleteCategory?id='+ categoryDeleteId,
    type: 'GET',
    headers: {"Authorization": cookieValue}
    })
})