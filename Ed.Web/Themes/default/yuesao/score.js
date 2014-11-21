
$(function () {
    $("#test_PInfoScomment").val($("#PInfoScomment").val());
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
    s = s + Number($("#PInfoSpicture").val());
    s = s + Number($("#PInfoScomment").val());
    $("#PInfoStotal").val(s);
}

function yanzheng(obj) {
    var fenzhi = $(obj).val();
    var g = /^[0-9]*[0-9][0-9]*$/;
    if (g.test(fenzhi)) {
        selectvalue();
    }
    else {
        alert("请输入正整数");
        $(obj).val("0");
    }
}
function yanzheng2(obj) {
    var fenzhi = $(obj).val();
    var g = /^[0-9]*[0-9][0-9]*$/;
    if (g.test(fenzhi)) {
        $("#PInfoScomment").val($(obj).val());
        selectvalue();
    }
    else {
        alert("请输入正整数");
        $(obj).val("0");
    }
}