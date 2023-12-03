const baseUrl = 'https://localhost:7207/'
//const btn = document.querySelector("button");
const categoryList = []




$('#addCategoryBTN').on('click',function(e) {
    addCategory()
})
function addCategory() {
    let categoryInput = $('#categoryInput').val()



    $('#categoryList').append(`<li class="list-group-item">${categoryInput}</li>`)
    categoryList.push(categoryInput)
    $('#categoryInput').val('')
}
// $.ajax({
//         method: 'GET',
//         url: baseUrl+'api/Category/Getcategories',
//         //contentType:"application/json; charset=utf-8",

//         //data: JSON.stringify(blogObj),
        
        
//     }).done(function (result) {

//         console.log(result)
//         console.log(result.id)
//         console.log(result.name)
        
//         $.each(result, function (i, item) {
//             $('#CategorySelection').append($('<option>', { 
//                 value: item.id,
//                 text : item.name 
//             }));
//         });
//     })
var filename = ''    
$("#submitBTN").on( "click",function(e) {
    
    console.log('SendRequest is worked')
    SendRequest(filename)
    console.log('categoryList',categoryList)
    var categoryListOBJ = {}
    $.ajax({
        url: baseUrl+'api/Category/AddCategoriesToblog?blogId=1',
        type: 'POST',
        method: 'POST',
        contentType:'application/json; charset=utf-8',
        //processData: false, 
        //contentType: false, 
        data:JSON.stringify(categoryList),
        
    }).done(function(result) {

    console.log('result' , result)
    });
});


$('#UploadPhoto').on("change",function(e){
    console.log('photo is changed')
    
    
    SendPhoto()
    console.log('filename = ',filename)
})

function SendPhoto(){
    const file = document.getElementById('UploadPhoto').files[0];

    $('#submitBTN').addClass('disabled')

    console.log('filename:',file.name)
    var isUploaded = false
    
    var data = new FormData();   
    data.append( 'file' , file);
    data.append('fileName',file.name)       
    filename = file.name
    
    console.log("submitted")
    $.ajax({
        url: baseUrl+'api/Blog/UploadPhoto',
        type: 'POST',
        contentType:"multipart/form-data",
        //processData: false, 
        //contentType: false, 
        data:data,
        cache: false,
        contentType: false,
        processData: false
    }).done(function(result) {

        isUploaded = result
        if (isUploaded == false) {
            $('#submitBTN').addClass('disabled')
        }if(isUploaded == true){
            $('#submitBTN').removeClass('disabled')
        }



    });
}
// btn.addEventListener("click", () => {
function SendRequest(photoName){


    console.log("event raised")

    //var reader = new FileReader();
    

    
    //bvar name_input = document.querySelector("#Name").value
    
    var article_input = document.querySelector("#Article").value
    
    var title_input = document.querySelector("#Title").value
    
    var photoUpload_input = document.querySelector("#UploadPhoto").value
    
    var author_input = document.querySelector("#author").value
    //var selectionOption = $('#CategorySelection').val();
    var blogObj = {
            
        "name": '0',
        "article": article_input,
        "author": author_input,
        "title": title_input,
        "photoName": photoName,
    }
    
          

    $.ajax({
        method: 'POST',
        url: baseUrl+'api/Blog/AddBLog',
        contentType:"application/json; charset=utf-8",

        data: JSON.stringify(blogObj),
        
        
    }).done(function (result) {

        console.log(result)
        //location.reload()
    });
    
}
var myFile = ''
function readURL( e ) {
               
    if ( this.files && this.files[0] ) {
       
        var reader = new FileReader();           
        reader.onload = ( function( e ) {
            $( 'img' ).attr( 'src' , e.target.result );
        });           
        reader.readAsDataURL( this.files[0] );
       
        myFile = this.files[0];      // store file in global variable
       
    }
   
}


    
