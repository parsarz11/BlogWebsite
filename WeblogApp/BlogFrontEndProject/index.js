ShowBlogs()

function ShowBlogs(){
    //document.cookie = 'token=Bearer  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJQYXJzYXJ6MTEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIm5iZiI6MTcwMjQ4NTU0OSwiZXhwIjoxNzAyNDg3MzQwLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MTY4IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzE2OCJ9.9oDZK3OMrlXhln1-V4wR-CK-mJMKDVRueTddX_TBAKc'
    const cookieValue = document.cookie
    .split("; ")
    .find((row) => row.startsWith("token="))
    ?.split("=")[1];

    console.log(cookieValue)
    const baseUrl = 'https://localhost:7207/api/'
    $.ajax({
        url: baseUrl+'Blog/AllBlogs',
        type: 'GET',
        headers: {"Authorization": cookieValue}
    }).done(function (result) {

        $.each(result, function (i, item) {
            WriteBlogs(item)
        });
    });
}


function WriteBlogs(result){
    console.log('result:')
    console.log(result)


    const cards =  `
        <div class="card col-xl-3 col-md-5  mb-4" style="width: 18rem;margin-right: 10px; padding-left:0px;padding-right:0px;">
            <a href="./blog.html?id=${result.id}">
                <img class="card-img-top" src="https://localhost:7207/api/Blog/DownloadPhotoById?id=${result.photoId}" alt="Card image cap">
                <div class="card-body">
                    <h2>${result.title}</h2>
                    <p class="card-text text-dark">${GetFirstFiftyWords(result.article)}</p>

                </div>
            </a>
    </div>`
    $('#BlogsDiv').append(cards)
    
}


function GetFirstFiftyWords(article)
{
    
    var result= article.slice(0,160)
    return result
}