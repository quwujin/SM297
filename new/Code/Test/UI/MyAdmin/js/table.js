 
<!--
$(document).ready(function(){
 $('.cooltable tbody tr:even').addClass('odd');
 $('.cooltable tbody tr').hover(
  function() {$(this).addClass('highlight');},
  function() {$(this).removeClass('highlight');}
 );
 // 如果复选框默认情况下是选择的，变色.
 $('.cooltable input[type="checkbox"]:checked').parents('tr').addClass('selected');
 // 复选框
 $('.cooltable tbody tr td').click(
  function() {
   if (!$(this).hasClass('oper')) {
    if ($(this).parents('tr').hasClass('selected')) {
     $(this).parents('tr').removeClass('selected');
     $(this).parents('tr').find('input[type="checkbox"]').removeAttr('checked');
    } else {
     $(this).parents('tr').addClass('selected');
     $(this).parents('tr').find('input[type="checkbox"]').attr('checked','checked');
    }
   }
  }
 );
});
function checkAllLine() { 
 if ($("#checkedAll").attr("checked") == true) { // 全选 
  $('.cooltable tbody tr').each(
   function() {
    $(this).addClass('selected');
    $(this).find('input[type="checkbox"]').attr('checked','checked');
   }
  );
 } else { // 取消全选 
  $('.cooltable tbody tr').each(
   function() {
    $(this).removeClass('selected');
    $(this).find('input[type="checkbox"]').removeAttr('checked');
   }
  );
 }
}
//-->
 