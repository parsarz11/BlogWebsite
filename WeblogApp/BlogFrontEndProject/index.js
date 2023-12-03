ShowBlogs()

function ShowBlogs(){


    const baseUrl = 'https://localhost:7207/api/'
    $.ajax({
        url: baseUrl+'Blog/AllBlogs',
        type: 'GET',
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