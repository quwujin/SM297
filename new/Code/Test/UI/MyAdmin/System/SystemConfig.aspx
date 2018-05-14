<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemConfig.aspx.cs" Inherits="MyAdmin_System_SystemConfig" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>  

<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
    <uc1:common runat="server" ID="common" /> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <link href="../Order_Vue/Element-UI/index.css" rel="stylesheet" />
    <style>
  .el-row { margin-bottom: 20px;}
  .el-col { border-radius: 4px; } 
  .grid-content {
    border-radius: 4px;
    min-height: 36px;
  }
  .row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
  }
</style>

</head>
<body>
    <form id="form1" runat="server"> 
        <el-tabs type="card" v-model="Parameter.ActiveName" @tab-click="handleClick">
            <el-tab-pane label="上线必填" name="first" v-if="GroupId==2" ></el-tab-pane>
            <el-tab-pane label="文案管理" name="second" ></el-tab-pane>
            <el-tab-pane label="项目配置" name="third" ></el-tab-pane>
            <el-tab-pane label="系统开关" name="fourth" v-if="GroupId==2"></el-tab-pane>
            <el-tab-pane label="新增配置项 +" name="five" v-if="GroupId==2"></el-tab-pane>
        </el-tabs>

        <el-dialog title="配置管理" :visible.sync="dialogFormVisible" width="30%" :before-close="handleCancel"  center>
          <el-form :model="Parameter">

            <el-form-item label="配置组" :label-width="formLabelWidth">
              <el-select v-model="Parameter.TId" placeholder="请选择配置组">
                <el-option label="上线必填" value="28"></el-option>
                <el-option label="文案管理" value="27"></el-option>
                <el-option label="项目配置" value="26"></el-option>
                <el-option label="系统开关" value="25"></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="输入标题" :label-width="formLabelWidth">
              <el-input v-model="Parameter.Title" auto-complete="off"></el-input>
            </el-form-item>

            <el-form-item label="配置类型" :label-width="formLabelWidth">
              <el-select v-model="Parameter.Types" placeholder="请选择配置类型">
                <el-option label="开关" value="0"></el-option>
                <el-option label="内容" value="1"></el-option>
                <el-option label="时间" value="2"></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="配置" :label-width="formLabelWidth">

                <template v-if="Parameter.Types==0">
                    <el-switch v-model="Parameter.States"  style="margin-top:7px;" >
                    </el-switch>
                </template>
                <template v-if="Parameter.Types==1">
                    <el-input v-model="Parameter.Val"  placeholder="输入内容">
                    </el-input>
                </template>
                <template v-if="Parameter.Types==2">
                    <el-date-picker v-model="Parameter.Val" type="datetime" placeholder="选择日期时间" @change="SetStarDate"> 
                    </el-date-picker>
                </template>
                
            </el-form-item>

          </el-form>

          <div slot="footer" class="dialog-footer">
            <el-button @click="handleCancel">取 消</el-button>
            <el-button type="primary" @click="Init">确 定</el-button>
          </div>

        </el-dialog>
         
        <table width="100%"  class="table table-bordered" >
            <thead>
              <tr> 
                  <th>标识码</th>
                  <th>标题</th>
                  <th>内容</th>
                  <th>操作</th>
              </tr>
            </thead>
            <tr v-for="item in ConfigArray">
                <td>{{item.KId}}</td>
                <td >{{item.Title}}</td>
                <td style="font-size:16px;" >
                    <template v-if="item.Val=='true'">
                        <span style="color:green;">已开启</span>
                    </template>
                    <template v-else-if="item.Val=='false'">
                        <span style="color:red;">已关闭</span>
                    </template>
                    <template v-else>
                        {{item.Val}}
                    </template>
                </td>
                <td> 
                    <el-button size="small" @click="handleEdit(item)" icon="el-icon-edit">编辑</el-button>
                    <el-button size="small" type="danger" @click="handleDelete(item.Id)" v-if="GroupId==2" icon="el-icon-delete">删除</el-button> 
                </td>
            </tr>
        </table>


        <script src="../Vue/vue.min.js"></script>
        <script src="../Order_Vue/Element-UI/index.js"></script>

        <script>

            var vue = new Vue({
                el:"#form1",
                data: {
                    RequestUrl: {
                        Url: "/MyAdmin/Handler/SystemConfig.ashx"
                    },
                    ConfigArray: [],
                    dialogFormVisible: false,
                    formLabelWidth: '70px',
                    GroupId:0,
                    Parameter: {
                        ActiveName: 'third',
                        ActionName: '',//操作类型
                        Types: '',//配置类型
                        Title: '',//标题
                        Val: '',//内容
                        States: false,//开关状态
                        ConfigId: 0,
                        TId: ''
                    }

                },
                methods: {
                    Init: function () {
                         
                        $.post(vue.RequestUrl.Url, vue.Parameter, function (result) {
                            var resultData = $.parseJSON(result);

                            if (resultData.Success) {
                                vue.ConfigArray = $.parseJSON(resultData.ObjectValue);
                            }
                            if (vue.Parameter.ActionName != '') {

                                vue.Parameter.ActionName = '';
                                vue.dialogFormVisible = false;

                                if (vue.Parameter.TId > 0)
                                    vue.Parameter.ActiveName = vue.Parameter.TId == 28 ? 'first' : vue.Parameter.TId == 27 ? 'second' : vue.Parameter.TId == 26 ? 'third' : 'fourth';

                                vue.$message(resultData.ErrMessage);
                            }
                        });
                    },
                    handleClick: function (tab, event) {
                        if (tab.name == "five") {
                            vue.Parameter.ActionName = "SaveConfig";
                            vue.Parameter.ConfigId = 0;
                            vue.Parameter.States = false;
                            vue.Parameter.Val = '';
                            vue.Parameter.Types = '';
                            vue.Parameter.Title = '';
                            vue.Parameter.TId = '';
                            vue.dialogFormVisible = true;
                        }
                        else {
                            vue.Parameter.ActiveName = tab.name;
                            vue.Init();
                        }
                    }, 
                    SetStarDate: function (val) {
                        vue.Parameter.Val = val;
                    },
                    handleEdit: function (item) {
                        vue.Parameter.ActionName = "EditConfig";
                        vue.Parameter.ConfigId = item.Id;
                        vue.Parameter.States = item.States == 1 ? true : false;
                        vue.Parameter.Val = item.Val;
                        vue.Parameter.Types = item.Types + '';
                        vue.Parameter.Title = item.Title;
                        vue.Parameter.TId = item.TId + '';
                        vue.dialogFormVisible = true;
                    },
                    handleDelete: function (ConfigId) {
                        vue.Parameter.ActionName = "DeleteConfig";
                        vue.Parameter.ConfigId = ConfigId;
                        vue.Init();
                    },
                    handleCancel: function () {
                        vue.Parameter.ActionName = '';
                        vue.dialogFormVisible = false;
                    }
                },
                watch: {
                    //'Parameter.ActiveName': function (val) {
                    //    console.log(val);
                    //}

                }

            })
            vue.Init();
            vue.GroupId = '<%=GroupId%>';
        </script>

    </form>
</body>
</html>
