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


WriteArticle()

// this function write article in html page
function WriteArticle()
{

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id')
    
    const baseUrl = 'https://localhost:7207/api/'

    //ajax call for get blog by Id 
    $.ajax({
        url: baseUrl+`Blog/GetBlogById?id=${id}`,
        type: 'GET',
    }).done(function (result) {
        
        var blogTitle = document.querySelector('#blogTitle')
        blogTitle.innerHTML = result.title
        
        var articleId = document.querySelector('#articleId')
        articleId.innerHTML = result.article

        var postInfo = document.querySelector('#PostInfo')
        postInfo.innerHTML = `Posted on ${result.date}, by ${result.author}`


        $('#updateBlogLink').attr('href','./AddBlog.html?id='+result.id)
        Category(id)

        BlogPhoto(result.photoId)
    })
}


//this function write blog categories
function Category(blogId)
{
    const baseUrl = 'https://localhost:7207/api/'

    //ajax call for get cateogry by blog id
    $.ajax({
        url: baseUrl+`Category/FindCategoriesByBLog?blogId=${blogId}`,
        type: 'GET',
    }).done(function (result) {

        //add blog's cateogries
        $.each(result, function (i, item) {
            var category = `<a class=" badge bg-secondary text-decoration-none link-light " href="./category.html?id=${item.id}" style="margin-right: 6px;">${item.name}</a>`
            $('#category').append(category)
        });
        
        
    })
}

//this function download blog thumbnail
function BlogPhoto(photoId)
{
    //set source of blog Image
    $("#blogImage").attr("src",`https://localhost:7207/api/Blog/DownloadPhotoById?id=${photoId}`);
}
