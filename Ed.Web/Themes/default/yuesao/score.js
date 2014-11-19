
$(function () {
    //判断是否选中
    $("#pregnanterinfo_form2 .c_note_item").click(function () {
        selectvalue();
    });
});

function selectvalue()
{
    var s = 0;
    $('input[name="C_PInfoCert"]:checked').each(function(){ 
        s+=10; 
    });
    $('input[name="C_PInfoQuality"]:checked').each(function () {
        var aa = $(this).val();
        var bb = '高级';
        if (aa.indexOf(bb) >= 0) {
            s += 30;
        }
        else {
            s += 10;
        }
    });
    $('input[name="C_PInfoService"]:checked').each(function () {
        var aa = $(this).val();
        var bb = '高级';
        if (aa.indexOf(bb) >= 0) {
            s += 30;
        }
        else {
            s += 10;
        }
    });
    $('input[name="C_PInfoKnowledge"]:checked').each(function () {
        var aa = $(this).val();
        var bb = '高级';
        if (aa.indexOf(bb) >= 0) {
            s += 30;
        }
        else {
            s += 10;
        }
    });
    $('input[name="C_PInfoEdu"]:checked').each(function () {
            s += 50;
    });
    $("#PInfoStotal").val(s);
}