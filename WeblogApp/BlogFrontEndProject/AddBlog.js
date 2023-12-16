const cookieValue = document.cookie.split("; ").find((row) => row.startsWith("token="))?.split("=")[1];
if(cookieValue == '')
{

    window.location.replace("./loginPage.html");
}
if(cookieValue != '')
{
    $('#loginBTN').addClass('invisible')
}


const baseUrl = 'https://localhost:7207/'
const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const id = urlParams.get('id')

var filename = ''    
const categoryList = []
let IsBlogWantToUpdate = false
let isWantInsert = false
var blogInputClickCount = 0

if(urlParams.size > 0)
{
    IsBlogWantToUpdate =true

    $.ajax({
        url: baseUrl+`api/Blog/GetBlogById?id=${id}`,
        type: 'GET',
    }).done(function (response) {
        var result = response
        

        document.querySelector("#Title").value = String(result.title)
            
        document.querySelector("#Article").value = result.article

        document.querySelector("#author").value = String(result.author)

        $('#photoDiv').addClass('invisible')
    })

}


$("#submitBTN").on( "click",function(e) {

    if (isWantInsert) {
        uploadBlog()

    }else{
        SendRequest(filename)
    }
    

});



$('#UploadPhoto').on("change",function(e){

    
    SendPhoto()

})


$('#addCategoryBTN').on('click',function(e) {
    addCategory()
})


$('#blogInputsBTN').on('click',function(e) {
    if(blogInputClickCount % 2 === 0)
    {
        $('#blogInputsBTN').val('insert')
        $('#blogInputDiv').addClass('invisible')
        $('#insertBlogDiv').removeClass('invisible')

        isWantInsert = true
        document.querySelector('#blogInputsBTN').value = 'Write'
    }else{

        isWantInsert = false
        document.querySelector('#blogInputsBTN').value = 'Insert'
        $('#blogInputDiv').removeClass('invisible')
        $('#insertBlogDiv').addClass('invisible')

    }
    
    
    blogInputClickCount++
})

function addCategory() {

    let categoryInput = $('#categoryInput').val()
    $('#categoryList').append(`<li class="list-group-item">${categoryInput}</li>`)
    categoryList.push(categoryInput)
    $('#categoryInput').val('')
}

    

//this function send blog photo
function SendPhoto(){
    const file = document.getElementById('UploadPhoto').files[0];

    $('#submitBTN').addClass('disabled')

    
    //get photo File data and add it to request
    var data = new FormData();   
    data.append( 'file' , file);
    data.append('fileName',file.name)       
    filename = file.name

    //ajax call for Upload photo
    $.ajax({
        url: baseUrl+'api/Blog/UploadPhoto',
        type: 'POST',
        contentType:"multipart/form-data",
        data:data,
        cache: false,
        contentType: false,
        processData: false,
        headers: {"Authorization": cookieValue}
    }).done(function(isUploaded) {


        
        if (isUploaded == false) {
            $('#submitBTN').addClass('disabled')
        }if(isUploaded == true){
            $('#submitBTN').removeClass('disabled')
        }
    });

}


//send request for add/update blog
function SendRequest(photoName){

    //get values of fields
    var article_input = document.querySelector("#Article").value
    
    var title_input = document.querySelector("#Title").value
    
    var photoUpload_input = document.querySelector("#UploadPhoto").value
    
    var author_input = document.querySelector("#author").value


    //initailize blog url and object
    var blogUrl= ''
    blogObj ={}
    if(IsBlogWantToUpdate)
    {
        //update blog url
        blogUrl = baseUrl+'api/Blog/UpdateBlog'

        //update blog object
         blogObj = {
            
            "id": id,
            "name": "0",
            "article": article_input,
            "author": author_input,
            "title": title_input,    
        }

    }else{
        //add blog url
        blogUrl = baseUrl+'api/Blog/AddBLog'
        
        //add blog object
        blogObj = {
            "name": '0',
            "article": article_input,
            "author": author_input,
            "title": title_input,
            "photoName": photoName,
        }

        
        
    }
    
    
          
    //ajax call for Add/Update blog 
    $.ajax({
        method: 'POST',
        url: blogUrl,
        contentType:"application/json; charset=utf-8",

        data: JSON.stringify(blogObj),
        headers: {"Authorization": cookieValue}
        
    }).done(function (result) {
        AddCategoriesToBlog()

        setTimeout(location.reload(), 3000);
        
    });
    
}
//this function Add categories to blog
function AddCategoriesToBlog()
{
    //ajax call for add categories to blog
    $.ajax({
        url: baseUrl+'api/Category/AddCategoriesToblog?blogId=0',
        type: 'POST',
        method: 'POST',
        contentType:'application/json; charset=utf-8',
        data:JSON.stringify(categoryList),
        headers: {"Authorization": cookieValue}
    }).done(function(result) {

    });
}


function uploadBlog() {
    const file = document.getElementById('uploadFile').files[0];

    $('#submitBTN').addClass('disabled')

    
    //get photo File data and add it to request
    var data = new FormData();   
    data.append( 'file' , file);
    data.append('fileName',file.name)       
    filename = file.name

    //ajax call for Upload photo
    $.ajax({
        url: baseUrl+'api/Blog/AddBlogByFile',
        type: 'POST',
        contentType:"multipart/form-data",
        data:data,
        cache: false,
        contentType: false,
        processData: false,
        headers: {"Authorization": cookieValue}
    }).done(function(isUploaded) {

        if (isUploaded == false) {
            $('#submitBTN').addClass('disabled')
        }if(isUploaded == true){
            $('#submitBTN').removeClass('disabled')
        }

        setTimeout(location.reload(), 3000);
    });
}

    
