
function LoadImage(input) {
    
    /*console.log(input.files[0])*/
    let maxSize = 1 * 1024 * 1024;
    let fileSize = input.files[0].size;
    let typeFile = input.files[0].type;

    


    
    if (fileSize > maxSize) {
        $('#text-fileDanger').html("you only can upload file under 1MB")
        $('#text-fileDanger').show()
        $('#targetImage').hide();

        $('#fileupload').val('');
        return false;
    }
    else if (typeFile != "image/jpg" && typeFile != "image/png" && typeFile != "image/jpeg") {
        $('#text-fileDanger').html("you only can upload Image")
        $('#text-fileDanger').show()
        $('#targetImage').hide();

        $('#fileupload').val('');
        return false;
    }
    else {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                console.log("inside onload e", e);
                $("#targetImage").attr("src", e.target.result);

            };
            $('#text-fileDanger').hide()
           
            reader.readAsDataURL(input.files[0]);



        }

        $('#targetImage').show();
    }

}






$('#btnUpload').click(function () {
    if (window.FormData !== undefined) {
        console.log('Start-----')
       

        var files = $("#FileUpload1")[0].files;
        var formData = new FormData();
        formData.append("file", files[0]);
        console.log(files);
        console.log(formData);
        $.ajax({
            url: '/api/app/upload-image/ckeditor-upload-file',
            data: formData,
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            
            
            success: function (result) {
                var parsedData = JSON.parse(result);
                var data = parsedData['path']

                
                alert(data);
            },
            error: function (err) {
                alert(err);
            }
        });
        
    }
    else {
        alert("FormData is not supported.");
    }
});