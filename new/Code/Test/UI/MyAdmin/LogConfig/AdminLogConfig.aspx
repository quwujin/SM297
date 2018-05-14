<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogConfig.aspx.cs" Inherits="AdminLogConfig" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>  

<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
    <uc1:common runat="server" ID="common" /> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Order_Vue/Element-UI/index.css" rel="stylesheet" />
    <style>
        .el-row {
            margin-bottom: 20px;
        }

        .el-col {
            border-radius: 4px;
        }

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
            <el-tab-pane label="日志级别配置" name="1" ></el-tab-pane>
            <el-tab-pane label="本地文件输出" name="2" ></el-tab-pane>
            <el-tab-pane label="邮件输出" name="3" ></el-tab-pane>
            <el-tab-pane label="数据库输出" name="4"></el-tab-pane>
            <el-tab-pane label="中央日志中心输出" name="5"></el-tab-pane>
        </el-tabs>

        <el-dialog title="日志管理" :visible.sync="dialogFormVisible" width="30%" :before-close="handleCancel"  center>
          <el-form :model="Parameter">

            <el-form-item label="日志级别" :label-width="formLabelWidth" >
              <el-select v-model="Parameter.TabId" disabled="disabled">
                <el-option label="日志级别配置" value="1"></el-option>
                <el-option label="本地文件输出" value="2"></el-option>
                <el-option label="邮件输出" value="3"></el-option>
                <el-option label="数据库输出" value="4"></el-option>
                <el-option label="中央日志中心输出" value="5"></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="配置标题" :label-width="formLabelWidth">
              <el-input v-model="Parameter.Title" auto-complete="off"  disabled="disabled"></el-input>
            </el-form-item>

            <el-form-item label="配置类型" :label-width="formLabelWidth">
              <el-select v-model="Parameter.Types" placeholder="请选择配置类型" disabled="disabled">
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
                  <th>标题</th>
                  <th>内容</th>
                  <th>操作</th>
              </tr>
            </thead>
            <tr v-for="item in ConfigArray">
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
                </td>
            </tr>
        </table>

        <script src="../Vue/vue.min.js"></script>
        <script src="../Order_Vue/Element-UI/index.js"></script>
        <script>
            var vue = new Vue({
                el: "#form1",
                data: {
                    RequestUrl: {
                        Url: "/MyAdmin/Handler/LogConfig.ashx"
                    },
                    ConfigArray: [],
                    dialogFormVisible: false,
                    formLabelWidth: '100px',
                    Parameter: {
                        ActiveName: '1',
                        ActionName: '',//操作类型
                        Types: '',//配置类型
                        Title: '',//标题
                        Val: '',//内容
                        States:false, //开关状态
                        TabId: 1
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

                                vue.$message(resultData.ErrMessage);
                            }
                                

                        });
                    },
                    handleClick: function (tab, event) {
                        vue.Parameter.TabId = tab.name;
                        vue.Init();
                    },
                    SetStarDate: function (val) {
                        vue.Parameter.Val = val;
                    },
                    handleEdit: function (item) {
                        vue.Parameter.ActionName = "EditConfig";
                        vue.Parameter.States = item.Types == 0 && item.Val == 'true' ? true : false;
                        vue.Parameter.Val = item.Val;
                        vue.Parameter.Types = item.Types + '';
                        vue.Parameter.Title = item.Title;
                        vue.Parameter.TabId = item.TabId + '';
                        vue.dialogFormVisible = true;
                    },
                    handleCancel: function () {
                        vue.Parameter.ActionName = '';
                        vue.dialogFormVisible = false;
                    }
                },
                watch: {
                }

            })
            vue.Init();
        </script>

    </form>
</body>
</html>
