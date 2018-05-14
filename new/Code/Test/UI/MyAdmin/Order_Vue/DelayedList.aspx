<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DelayedList.aspx.cs" Inherits="MyAdmin_Order_Vue_DelayedList" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>  
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title></title> 
    <uc1:common runat="server" ID="common" />   
     
</head>
<body>
    <form id="form1" runat="server">

        <div class="table-header"> 延迟订单管理 </div>

        <table width="100%" class="table table-bordered" >
            <tr>
                <td>
                    <el-date-picker v-model="Parameter.StarTimeText" Style="width:150px;"  type="date" @change="SetStarDate"  placeholder="选择开始日期" >
                    </el-date-picker>
                    -
                    <el-date-picker v-model="Parameter.EndTimeText" Style="width:150px;" type="date" @change="SetEndDate" placeholder="选择结束日期" >
                    </el-date-picker> 
                    <input type="button" value="搜 索" class="btn btn-sm btn-primary" v-on:click="Init()"  /> 
                </td>
            </tr>
        </table>
        <table id="sample-table-2" class="table table-striped table-bordered table-hover">
             <thead>
                <tr>
                    <th>序号</th>
                    <th>编号</th>
                    <th>订单Id</th> 
                    <th>状态</th>
                    <th>创建时间</th>
                    <th>延时时间</th>
                    <th>发放时间</th>
                    <th>备注</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody >
                 <tr v-for="item in TableList"> 
                      
                     <template v-for="(value,key,index) in item">
                         <td>
                            <template v-if="key=='StatusId'">
                                {{ item.StatusId == 0 ? '待发放':item.StatusId==1?'已发放':item.StatusId==-1?'发放失败':'发放中' }}
                            </template>                         
                            <template v-else>
                                {{ value }}
                            </template>
                         </td>
                     </template>
                     <td>
                        <template v-if="item.StatusId==-1">
                             <el-button type="button"  @click="Submit(item.Id)" class="btn btn-success">补发</el-button>
                        </template>       
                     </td>
                 </tr> 

            </tbody>
        </table>

        <el-pagination
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="1"
            :page-sizes="[5,10,15]"
            :page-size="Parameter.PageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="Parameter.PageCount">
        </el-pagination>
        
        <script>
            layui.use('laydate', function () {
                var laydate = layui.laydate;
            });
        </script>

        <script src="../Vue/vue.min.js"></script>

        <link href="Element-UI/index.css" rel="stylesheet" />
        <script src="Element-UI/index.js"></script>

         <script>
             
             var vue = new Vue({
                 el: "#form1",
                 data: {
                     RequestUrl: {
                         Url: "../Handler/CommonGetList.ashx"
                     },
                     TableList: [],
                     Parameter: {
                         PageCount: 0,
                         PageIndex: 1,
                         PageSize: 5,
                         ActionName: 'Delayed',
                         StarTimeText: '',//开始日期
                         EndTimeText: '',//结束日期 
                     }
                 },
                 methods: {
                     Init: function () {

                         $.post(vue.RequestUrl.Url, vue.Parameter, function (result) {
                             var resultData = $.parseJSON(result);

                             if (resultData.Success) {
                                 vue.TableList = $.parseJSON(resultData.ObjectValue);
                                 vue.Parameter.PageCount = resultData.EffectRows;
                             }
                             
                         });
                     },
                     SetStarDate: function (val) {
                         vue.Parameter.StarTimeText = val;
                     },
                     SetEndDate: function (val) {
                         vue.Parameter.EndTimeText = val;
                     },
                     handleSizeChange: function (val) {
                         vue.Parameter.PageSize = val;
                         vue.Init();
                     },
                     handleCurrentChange: function (val) {
                         vue.Parameter.PageIndex = val;
                         vue.Init();
                     },
                     Submit: function (id) {

                         var h = this.$createElement;
                         this.$msgbox({
                             title: '消息',
                             type: 'warning',
                             message: h('p', null, [h('span', null, '确认提交？ ')]),
                             showCancelButton: true,
                             confirmButtonText: '确定',
                             cancelButtonText: '取消',
                             beforeClose: function (action, instance, done) {
                                 if (action === 'confirm') {

                                     $.post("UpdateOrder.aspx", { GetType: "DelayedReissue", DelayedId: id }, function (result) {
                                         var resultData = $.parseJSON(result);

                                         vue.$notify({
                                             type: resultData.Success ? "success" : "error",
                                             message: resultData.Success ? "提交成功" : resultData.ErrMessage,
                                             offset: 100,
                                             duration: resultData.Success ? 2000 : 5000
                                         });
                                     });

                                     instance.confirmButtonLoading = false;

                                 }

                                 done();

                             }
                         })
                     }
                 }
             });

             vue.Init();

    </script>


    </form>
</body>
</html>
