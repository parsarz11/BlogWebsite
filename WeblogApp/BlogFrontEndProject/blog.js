


WriteArticle()


function WriteArticle()
{

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id')
    console.log("🚀 ~ file: blog.js:35 ~ id:", id)
    const baseUrl = 'https://localhost:7207/api/'

    
    $.ajax({
        url: baseUrl+`Blog/GetBlogById?id=${id}`,
        type: 'GET',
    }).done(function (result) {

        console.log(result)
        console.log(result.article)
    
    

        
        
    
        console.log('vdc')
        
        console.log(result)

        var blogTitle = document.querySelector('#blogTitle')
        blogTitle.innerHTML = result.title
        var articleId = document.querySelector('#articleId')
        articleId.innerHTML = result.article

        //Posted on January 1, 2023 by Start Bootstrap
        var postInfo = document.querySelector('#PostInfo')
        postInfo.innerHTML = `Posted on ${result.date}, by ${result.author}`


        //<a class="badge bg-secondary text-decoration-none link-light" href="#!">Web Design</a>
        
        Category(id)

        BlogPhoto(result.photoId)
    })
}



function Category(categoryId)
{
    const baseUrl = 'https://localhost:7207/api/'
    $.ajax({
        url: baseUrl+`Category/FindCategoriesByBLog?blogId=${categoryId}`,
        type: 'GET',
    }).done(function (result) {

        console.log(result)
        $.each(result, function (i, item) {
            var category = `<a class=" badge bg-secondary text-decoration-none link-light " href="#!" style="margin-right: 6px;">${item.name}</a>`
            $('#category').append(category)
        });
        
        
    })
}

function BlogPhoto(photoId)
{

    $("#blogImage").attr("src",`https://localhost:7207/api/Blog/DownloadPhotoById?id=${photoId}`);
        

}
