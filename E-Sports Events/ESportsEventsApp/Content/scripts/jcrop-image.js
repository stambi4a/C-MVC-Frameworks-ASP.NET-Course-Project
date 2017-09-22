var imageCropWidth = 0;
var imageCropHeight = 0;
var cropPointX = 0;
var cropPointY = 0;
var jcropApi;
$(document).ready(function () {
    initCrop();
});
$("#hlcropImage").on("click", function (e) {
    /*
    The event.preventDefault() method stops the default action of
    an element from happening. For example: Prevent a submit button
    from submitting a form. Prevent a link from following the URL
    */
    e.preventDefault();
    cropImage();
});
function initCrop() {
    $('#imgEmpPhoto').Jcrop({
        onChange: setCoordsAndImgSize,
        aspectRatio: 0, // 1 means will be same for height and weight
        onSelect: setCoordsAndImgSize
    }, function () { jcropApi = this });
}
function showCoordinate() {
    $("#lblWidth").text(imageCropWidth + "px");
    $("#lblHeight").text(imageCropHeight + "px");
}
function setCoordsAndImgSize(e) {
    imageCropWidth = e.w;
    imageCropHeight = e.h;
    cropPointX = e.x;
    cropPointY = e.y;
    $("#lblWidth").text(imageCropWidth + "px");
    $("#lblHeight").text(imageCropHeight + "px");
}
function cropImage() {
    if (imageCropWidth == 0 && imageCropHeight == 0) {
        alert("Please select crop area.");
        return;
    }
    var img = $("#imgEmpPhoto").attr("src");
    /*Show cropped image*/
    showCroppedImage();
}

function showCroppedImage() {
    var x1 = cropPointX;
    var y1 = cropPointY;
    var width = imageCropWidth;
    var height = imageCropHeight;
    $("#X").val(x1);
    $("#Y").val(y1);
    $("#Height").val(height);
    $("#Width").val(width);
    var canvas = $("#canvas")[0];
    var context = canvas.getContext("2d");
    var img = new Image();
    img.onload = function () {
        canvas.height = height;
        canvas.width = width;
        context.drawImage(img, x1, y1, width, height, 0, 0, width, height);
    };
    img.src = $('#imgEmpPhoto').attr("src");
}

function readFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        /*Destroy jcrop initialization other wise it will hold it previous image in img tag*/
        if (jcropApi != null) {
            jcropApi.destroy();
        }
        reader.onload = function (e) {
            $('#imgEmpPhoto').attr('src', "");
            var img = $('#imgEmpPhoto').attr('src', e.target.result);
           /*Current uploaded image size*/
           var width = img[0].height;
            var height = img[0].width;
            $("#lblWidth").text(width + "px");
            $("#lblHeight").text(height + "px");
            //InitCrop must call here otherwise it will not work
            initCrop();
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$('#flPhoto').change(function () {
    readFile(this);
    //initCrop();
});