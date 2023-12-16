window.onload = getBlogs()

const cookieValue = document.cookie.split("; ").find((row) => row.startsWith("token="))?.split("=")[1];
if(cookieValue == '')
{
    $('#signOutBTN').addClass('invisible')
    $('#loginBTN').removeClass('invisible')
}
if(cookieValue != '')
{
    $('#loginBTN').addClass('invisible')
}
function getBlogs(){






    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id')
    const baseUrl = 'https://localhost:7207/api/'

    $.ajax({
        url: baseUrl+`Category/FindBlogsByCategoryId?categoryId=${id}`,
        type: 'GET',
    }).done(function (result) {

        console.log(result)
        $.each(result, function (i, item) {
            CreateTable(item,i)
        });
        
        
    })
}

function CreateTable(result,i){

    const tcontent = `
    <tr>
        <th scope="row"><a href="./blog.html?id=${result.id}">${result.id}</a></th>
        <td>${result.title}</td>
        <td>${result.author}</td>
        <td>${result.date}</td>
    </tr>`
    $('#blogsTable').append(tcontent)
}