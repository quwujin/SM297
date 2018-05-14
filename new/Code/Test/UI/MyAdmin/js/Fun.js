/// <reference path="Fun.js" />
function del() {
    if (confirm("真的要删除吗？删除后无法恢复！")) {
        return true;
    } else {
      return false;
    }
}


function loginout() {
    if (confirm("确定退出吗？")) {
        return true;
    } else {
        return false;
    }
}