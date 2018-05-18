<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminOrderInfo.aspx.cs" Inherits="MyAdmin_AdminOrderInfo" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>  
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
    <uc1:common runat="server" ID="common" />  
    <script src="../OCRImgApi/js/GetImgApi.js?a=<%=DateTime.Now.ToString("yyyyMMddhhmmssffff") %>"></script>
    <style>
        .txt_notes{display:none; border:1px solid #ccc;padding:5px;width:200px; background:#efefef; margin:5px;text-align:center;}
        .AddMenu input,img{width:100%;margin-top:5px}  
         .el-select .el-input {
            width: 130px;
          }
    </style>
    
</head>
<body style="background: #fff;">
    <form id="form1" runat="server">  
         
        <div class="main-content" style="width: 99%">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-header">
                         订单列表
                    </div> 

                    <el-dialog title="提示" :visible.sync="popoverVisible" width="30%" >
                      <template v-if="popoverType=='Success'"> 
                            <p>确定通过吗？</p>
                            <el-row  style="margin-top:20px;"> 
                                <template v-for="item in SuccessVal"> 
                                    <el-col :span="5" > 
                                        <el-button type="info" @click="ChooseVal(item)" plain>{{item}}</el-button>
                                    </el-col>
                                </template>
                            </el-row>
                        </template>
                        <template v-if="popoverType=='Fail'">
                            <p>确定作废吗？</p> 
                            <el-row  style="margin-top:20px;"> 
                                <template v-for="item in FailVal"> 
                                    <el-col style="margin-top:20px;"> 
                                        <el-button type="info" @click="ChooseVal(item)" plain>{{item}}</el-button>
                                    </el-col>
                                </template>
                            </el-row> 
                        </template>

                        <div style="text-align: right; margin: 0">
                            <el-button size="mini" type="text" @click="popoverVisible = false" plain>取消</el-button> 
                        </div>
                    </el-dialog> 
                   
                    <div>
                    <!-------------------搜索--------------------->
                         
                    <table id="checkSelect" width="100%" class="table table-bordered" >
                        <tr class="info">
                             
                            <td > 搜索 ：
                                <el-input placeholder="请输入内容" v-model="Parameter.CheckInputText" style="width: 280px;">
                                    <el-select v-model="Parameter.SearchOptionText" clearable filterable="true" placeholder="请选择搜索项" slot="prepend">
                                        <el-option v-for="item in OrderInfoKeyName"  :key="item.Key" :label="item.Name" :value="item.Key"> </el-option>
                                    </el-select>
                                </el-input>

<%--                                <el-select v-model="Parameter.SearchOptionText" clearable placeholder="请选择搜索项" slot="prepend">
                                     <el-option v-for="item in OrderInfoKeyName"  :key="item.Key" :label="item.Name" :value="item.Key"> </el-option>
                                 </el-select>
                                <input v-model="Parameter.CheckInputText" type="text" placeholder="请输入搜索内容" style="width:130px;">--%>
                             </td>
                           <%--<td > 状态：
                                 <el-select v-model="Parameter.StatusOptionText" placeholder="请选择">
                                    <el-option  label="未审核" value="0"> </el-option>
                                    <el-option  label="已审核" value="1"> </el-option>
                                    <el-option  label="已作废" value="-1"> </el-option> 
                                </el-select>   
                            </td>--%>
                             <td > 奖项： 
                                 <el-select v-model="Parameter.PrizeOptionText" clearable placeholder="请选择">
                                    <el-option v-for="item in AwardsOptions" :key="item.value" :label="item.label" :value="item.value">
                                    </el-option>
                                </el-select>    
                            </td> 
                            <td > 订单时间：
                                 <el-date-picker v-model="Parameter.StarTimeText" Style="width:150px;"  type="date" @change="SetStarDate"  placeholder="选择开始日期" >
                                 </el-date-picker>
                                 <el-date-picker v-model="Parameter.EndTimeText" Style="width:150px;" type="date" @change="SetEndDate" placeholder="选择结束日期" >
                                 </el-date-picker> 
                                <input type="button" value="搜 索" class="btn btn-sm btn-primary" v-on:click="Init(Parameter,0)"  /> 
                                 <input type="button" value="导 出" class="btn btn-sm btn-success" v-on:click="Export()"/> 
                            </td> 
                      </tr>
                      <tr class="info">
                          <td > 是否调用OCR： 
                                <el-select v-model="Parameter.OcrOptionText" clearable placeholder="请选择">
                                    <el-option  label="是" value="是"> </el-option>
                                    <el-option  label="否" value="否"> </el-option>  
                                </el-select>   
                          </td>
                          <td > 是否查询退款/过期订单： 
                                <el-select v-model="Parameter.IsRefund" clearable placeholder="请选择">
                                    <el-option  label="是" value="是"> </el-option>
                                    <el-option  label="否" value="否"> </el-option>  
                                </el-select>   
                            </td>
                          <td>

                          </td>
                      </tr>
                    </table>

                    <div class="table-header" v-on:click="CheckedNames=CheckedNames=='none'?'block':'none'">
                         点我显示勾选列表 <span class="glyphicon glyphicon-plus"></span>
                    </div>

                    <table width="100%"  class="table table-striped table-bordered table-hover"  >  
                        <el-row  style="margin-top:20px;" v-bind:style="{ display: CheckedNames }" > 
                             <%--勾选列表--%>
                            <el-checkbox-group v-model="OrderInfoCheckedNames">
                                <template v-for="(value,key,index) in OrderInfoKeyName"> 
                                    <el-col :span="2" v-if="value.disabled==false" > 
                                        <el-checkbox  :label="key" :disabled="value.disabled">{{value.Name}}</el-checkbox> 
                                    </el-col>
                                </template>
                            </el-checkbox-group>
                            
                        </el-row>
                    </table>
                    <!-------------------搜索--------------------->

                    <div class="tabbable">
						<ul class="nav nav-tabs" id="myTab">
							<li v-bind:class="{active: Parameter.OrderStatusType==1 }">
								<a v-on:click="GetStatusOrderList(1)" >
									全部订单
                                    <span class="badge badge-success"><%=orderQty %></span>
								</a>
							</li>

							<li v-bind:class="{active: Parameter.OrderStatusType==2 }" >
								<a v-on:click="GetStatusOrderList(2)">
									待审核
									<span class="badge badge-success"><%=orderQty0 %></span>
								</a>
							</li>

							<li v-bind:class="{active: Parameter.OrderStatusType==3 }" >
								<a v-on:click="GetStatusOrderList(3)">
									已完成
									<span class="badge badge-success"><%=orderQty1 %></span>
								</a>
							</li>

                            <li v-bind:class="{active: Parameter.OrderStatusType==4 }" >
								<a v-on:click="GetStatusOrderList(4)" > 
									已作废
									<span class="badge badge-success"><%=orderQty2 %></span>
								</a>
							</li>
						</ul>

						<div class="tab-content">
							<div id="home" class="tab-pane fade active in">
                               <!-------------------------订单主体--------------------->
                                                                              
                               <div class="gallerys">
                                   <table width="100%"  class="table table-bordered" >
                                    <tr>
                                        <td>
                                            <el-checkbox v-model="checkAll" @change="handleCheckAllChange">全选</el-checkbox>
  
                                            <div style="float:right;"> 
                                                <div  v-if="GroupId==2" class="btn btn-sm btn-danger "  v-on:click="MessageBox('BatchDelete',0)">
												    <i class="icon-fire bigger-110"></i>
												    <span class="bigger-110 no-text-shadow" >批量删除</span>
											    </div>

                                                <div class="btn btn-sm btn-warning "  v-on:click="MessageBox('BatchFail',0)">
												    <i class="icon-fire bigger-110"></i>
												    <span class="bigger-110 no-text-shadow" >批量作废</span>
											    </div>
                                            </div>
                                            <div  class="btn btn-sm btn-success "  onclick="CloseOcr()" >
												<i class="icon-fire bigger-110"></i>
												<span class="bigger-110 no-text-shadow" >X 关闭旋转自动识别</span>
											</div>
                                            <div class="btn btn-sm btn-success "  v-on:click="MessageBox('BatchRecorded',0)" >
												<i class="icon-fire bigger-110"></i>
												<span class="bigger-110 no-text-shadow" >批量预审勾选订单</span>
											</div>
                                            <div class="btn btn-sm btn-warning "   v-on:click="MessageBox('BatchRecordedAll',0)" >
												<i class="icon-fire bigger-110"></i>
												<span class="bigger-110 no-text-shadow" >批量预审未录入订单</span>
											</div>
                                            
                                        </td>
                                    </tr>
                                   </table>

                                   <table id="sample-table-2" class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr> 
                                            <template v-for="(value,key,index) in OrderInfoKeyName"> 
                                                <th v-bind:style="{display:value.IsShow?'':'none'}">{{ value.Name }}</th> 
                                            </template>
                                            
                                            <th style="width:200px;">备注</th>
                                            <th>操作</th>
                                        </tr>
                                    </thead>
                                        
                                    <tbody id="body-colum">
                                         
                                        <tr v-for="item in OrderInfoList">  
                                             
                                           <template v-for="(value,key,index) in item">
                                            
                                            <td  v-bind:style="{display:IsShow(index)}" > 
                                                 <%--{{ index }}: {{ key }}: {{ value }}--%> 
                                                <template v-if="key=='Id'"> 
                                                     <el-checkbox-group v-model="BatchIdArray" > 
                                                        <el-checkbox  v-bind:label="item.Id" >
                                                            <span class="label label-sm label-warning">{{ item.Id }}</span>
                                                        </el-checkbox>
                                                     </el-checkbox-group>
                                                </template>
                                                
                                                <template v-else-if="key=='FilesId'">
                                                     <div style="width:240px; height:170px;background:#ccc;">
                                                        <div style="width:120px; height:170px; float:left;border:1px solid #fff;padding:3px; ">
                                                            <img  v-bind:src="item.NewImgurl==null?item.FileName:item.NewImgurl"  v-bind:id="'rotImg'+item.Id" width="100" height="140" class="FileImg" v-on:click="openPhotoGallery(item.Id)"/>
                                                            ({{ item.HashCount }}) <a v-bind:href="item.SaveName" target="_blank">查看原图</a>
                                                        </div> 
                                                        <div class="AddMenu" style="width:120px; height:170px;border:1px solid #fff;padding:3px;float:right;">
                                                          <input type="button" value="旋转 90 °C" class='btn btn-info' v-on:click="ImgApi(90,item.NewImgurl,item.FileName,item.NewImgsign,item.Hashdata,item.OrderCode,item.CreateTime,item.Id)"/>
                                                          <input type="button" value="旋转 180 °C" class='btn btn-info' v-on:click="ImgApi(180,item.NewImgurl,item.FileName,item.NewImgsign,item.Hashdata,item.OrderCode,item.CreateTime,item.Id)"/>
                                                          <input type="button" value="旋转 270 °C" class='btn btn-info' v-on:click="ImgApi(270,item.NewImgurl,item.FileName,item.NewImgsign,item.Hashdata,item.OrderCode,item.CreateTime,item.Id)"/>
                                                          <input type="button" value="云处理" class="btn btn-purple  ocr" v-on:click="ImgApi(0,item.NewImgurl,item.FileName,item.NewImgsign,item.Hashdata,item.OrderCode,item.CreateTime,item.Id)"/>
                                                        </div>
                                                    </div>
                                                </template>

                                                <template v-else-if="key=='States'">
                                                    <span class="btn btn-xs  btn-white" style="width:100px;">
                                                        <span v-if="item.States=='0'">未审核</span>
                                                        <span v-else-if="item.States=='1'"><span style='color:green'>已审核</span></span>
                                                        <span v-else-if="item.States=='5'">无法审核</span>
                                                        <span v-else-if="item.States=='2'">未抽奖</span>
                                                        <span v-else-if="item.States=='3'">二等奖未完善地址</span>
                                                        <span v-else ><span style='color:red'>作废</span></span>
                                                    </span>
                                                    <br />
                                                    <span v-if="item.Note==null||item.Note.length==0"></span>
                                                    <span v-else ><span class='btn btn-xs btn-danger' style='width:100px;margin-top:10px;'>{{ item.Note }}</span></span>
                                                </template>
						                        
                                                <template v-else-if="key=='Mob'">
                                                    {{ item.Mob!=null? item.Mob.substr(0,4)+'****':'' }}{{ item.Mob!=null? item.Mob.substr(8,4):'' }}
                                                </template>

                                                <template v-else-if="key=='HbOrderCode'&&item.isRefund==1">
                                                    <el-button type="success" v-on:click="MessageBox('ReissueHb',item.Id)">补发退款/过期订单红包</el-button>
                                                </template>

                                                <template v-else-if="key=='PrizeCode'">
                                                    {{ item.PrizeCode!=null? item.PrizeCode.substr(0,4)+'****':'' }}
                                                </template>

                                                <template v-else-if="key=='Code'">
                                                    {{ item.Code!=null? item.Code.substr(0,4)+'****':'' }}
                                                </template>

                                                <template v-else-if="key=='Adds'">
                                                    {{ item.Adds!=null? item.Adds.substr(0,4)+'****':'' }}
                                                </template>

                                                <template v-else-if="key=='RedPackMoney'">
                                                    {{ item.RedPackMoney!=null? item.RedPackMoney/100:item.RedPackMoney }}
                                                </template>
                                                
                                                <template v-else>
                                                    {{ value }}
                                                </template>
                                               
                                            </td>

                                           </template>
                                             
                                           <td>
                                                <div class='list_btn'>
                                                    <input type="button" value="增加备注+" v-on:click="ShowNotes(item.Id)" class="notes btn-minier btn btn-info"/> 
                                                </div>
                                                <div v-bind:id="'txt_notes_'+item.Id" class="txt_notes"  style="background:#fff;" >

                                                        <div class="input-group">
                                                            <div class="input-group-addon">门店</div>
                                                            <input type="text" v-bind:id="'store'+item.Id" class="form-control" v-bind:value="item.Title"  />
                                                        </div> 
                                                        <div class="input-group">
                                                            <div class="input-group-addon">金额</div>
                                                            <input type="text"  v-bind:id="'price'+item.Id" class="form-control" v-bind:value="item.Age"  />
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">流水</div>
                                                            <input type="text"  v-bind:id="'nums'+item.Id" class="form-control" v-bind:value="item.Texts"  />
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">日期</div>
                                                            <input type="text"  v-bind:id="'times'+item.Id" class="form-control" v-bind:value="item.Tdate" onclick="layui.laydate({ elem: this, istime: false, format: 'YYYY-MM-DD' })"  />
                                                        </div>
                                                        <input type="button" value="保存" v-on:click="MessageBox('SaveRemark',item.Id)" class="btn btn-sm btn-success" />
                                                </div>
                                            </td>

                                           <td>
                                                <div v-if="item.States=='0'">
                                                    <botton type="button"  v-bind:id="'successid'+item.Id" v-on:click="MessageBox('Success',item.Id)" class="btn  btn-success"  >
                                                        <i class='ace-icon fa fa-check'></i>审核通过
                                                    </botton><br>

                                                    <botton type="button" v-bind:id="'fail'+item.Id" v-on:click="MessageBox('Fail',item.Id)" class="btn  btn-danger" style='margin-top:10px;' >
                                                        <i class='ace-icon fa fa-trash-o'></i>作废订单
                                                    </botton><br>
                                                    <div v-if="item.Note==null">
                                                        <botton type="button" value="无法审核" v-on:click="MessageBox('Unable',item.Id)" class="btn btn-info" style='margin-top:10px;' />
                                                         <i class='ace-icon fa fa-exclamation-circle'></i>无法审核
                                                        </botton>
                                                    </div>
                                                </div>

                                                <div v-else-if="item.States=='1'">
                                                    <span   class='btn btn-xs btn-primary btn-white'>已完成</span>
                                                </div>

                                                <div v-else-if="item.States=='-1'">

                                                    <span   class='zf btn btn-xs btn-primary btn-white'>已作废</span>

                                                    <botton v-if="GroupId==2" type="button" class="btn  btn-success" v-on:click="MessageBox('Recovery',item.Id)" >
                                                        <i class='ace-icon fa fa-check'></i>恢复订单
                                                    </botton>
                                                </div>

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
                                    
                               </div>
                                 
							</div>
												 
						</div>
					</div>
                         
                    </div>
                </div>
            </div>
        </div>  
         
        <script>
             layui.use('laydate', function () {
                 var laydate = layui.laydate;
             });
       </script>

        <script src="../Vue/vue.min.js"></script>

        <link href="Element-UI/index.css" rel="stylesheet" />
        <script src="Element-UI/index.js"></script>

        <script>
            
            var OrderIdOptions = [];//存储当前页订单ID
            
            var vm=new Vue({
                el:"#form1",
                data: {
                    urls: {
                        ChangOrderUrl: "UpdateOrder.aspx"
                    },
                    GroupId:0,
                    OrderInfoCheckedNames: [],//勾选字段复选框
                    BatchIdArray: [],//订单编号勾选组 值：订单ID
                    checkAll: false,//控制订单编号是否全选
                    AwardsOptions:[],//奖项
                    CheckedNames:"none",
                    OrderInfoKeyName: [
                    { Key: "a.rowNum", Name: "序号", IsShow: false, disabled: true },
                    { Key: "a.Id", Name: "编号", IsShow: true, disabled: false },
                    { Key: "a.OrderCode", Name: "订单号", IsShow: false, disabled: false },
                    { Key: "a.OpenId", Name: "OpenId", IsShow: false, disabled: false },
                    { Key: "a.NickName", Name: "微信昵称", IsShow: false, disabled: false },
                    { Key: "a.HeadImgurl", Name: "微信头像", IsShow: false, disabled: false },
                    { Key: "a.Texts", Name: "备注流水号", IsShow: false, disabled: false },
                    { Key: "a.FilesId", Name: "小票", IsShow: true, disabled: false },
                    { Key: "a.States", Name: "状态", IsShow: true, disabled: false },
                    { Key: "a.Number", Name: "Number", IsShow: false, disabled: false },
                    { Key: "a.Name", Name: "姓名", IsShow: true, disabled: false },
                    { Key: "a.Mob", Name: "手机号", IsShow: true, disabled: false },
                    { Key: "a.CreateTime", Name: "创建时间", IsShow: true, disabled: false },
                    { Key: "a.Title", Name: "备注门店名称", IsShow: false, disabled: false },
                    { Key: "a.Age", Name: "备注金额", IsShow: false, disabled: false },
                    { Key: "a.Tdate", Name: "备注日期", IsShow: false, disabled: false },
                    { Key: "a.Jp", Name: "奖品", IsShow: true, disabled: false },
                    { Key: "a.Jx", Name: "奖项", IsShow: true, disabled: false },
                    { Key: "a.PrizeCode", Name: "奖品串码", IsShow: false, disabled: false },
                    { Key: "a.IDCard", Name: "身份证号", IsShow: false, disabled: false },
                    { Key: "a.DateStamp", Name: "时间戳", IsShow: false, disabled: false },
                    { Key: "a.Code", Name: "激活码", IsShow: false, disabled: false },
                    { Key: "a.HbOrderCode", Name: "红包订单号", IsShow: false, disabled: false },
                    { Key: "a.Ip", Name: "IP", IsShow: false, disabled: false },
                    { Key: "a.Types", Name: "Types", IsShow: false, disabled: false },
                    { Key: "a.Adds", Name: "地址", IsShow: true, disabled: false },
                    { Key: "a.RedPackMoney", Name: "红包金额", IsShow: true, disabled: false },
                    { Key: "a.MobHome", Name: "手机号地归", IsShow: false, disabled: false },
                    { Key: "a.IpAddress", Name: "IP地归", IsShow: false, disabled: false },
                    { Key: "a.Province", Name: "省", IsShow: true, disabled: false },
                    { Key: "a.City", Name: "市", IsShow: true, disabled: false },
                    { Key: "a.Area", Name: "区", IsShow: true, disabled: false },
                    { Key: "a.Sources", Name: "来源", IsShow: false, disabled: false },
                    { Key: "a.UpdateTime", Name: "修改时间", IsShow: true, disabled: false },
                    { Key: "a.Account", Name: "操作人", IsShow: true, disabled: false },
                    { Key: "a.Note", Name: "订单备注", IsShow: false, disabled: false },
                    { Key: "AwardId", Name: "抽奖ID", IsShow: false, disabled: true },
                    { Key: "RedAwardId", Name: "红包抽奖ID", IsShow: false, disabled: true },
                    { Key: "IsBack", Name: "是否回库", IsShow: false, disabled: true },
                    { Key: "IsGrant", Name: "是否已发放奖品", IsShow: false, disabled: true },
                    { Key: "GrantTime", Name: "发放奖品时间", IsShow: false, disabled: true },
                    { Key: "IsThrottle", Name: "是否节流订单", IsShow: false, disabled: true },
                    { Key: "NewImgurl", Name: "旋转后小票路径", IsShow: false, disabled: true },
                    { Key: "NewImgsign", Name: "旋转后小票签名", IsShow: false, disabled: true },
                    { Key: "Hashdata", Name: "小票签名", IsShow: false, disabled: true },
                    { Key: "FileName", Name: "压缩后的路径图片名称", IsShow: false, disabled: true },
                    { Key: "SaveName", Name: "路径图片名称", IsShow: false, disabled: true },
                    { Key: "HashCount", Name: "小票重复个数", IsShow: false, disabled: true },
                    { Key: "isXRefund", Name: "红包是否过期", IsShow: false, disabled: true },
                    ],
                    OrderInfoList: [], //订单集合
                    Parameter: {
                        PageCount: 0,
                        PageIndex: 1,
                        PageSize: 5,
                        GetType: 'InitData',
                        Store: '',//门店名
                        Nums: '',//流水号
                        Price: '',//金额
                        Times: '',//日期
                        CheckInputText: '',//搜索内容
                        SearchOptionText: '',//搜索项
                        StatusOptionText: '',//状态选择项
                        PrizeOptionText: '',//奖项选择项
                        OcrOptionText: '',//OCR选择项
                        StarTimeText: '',//开始日期
                        EndTimeText: '',//结束日期
                        OrderStatusType: 2,//搜索订单状态 1：全部订单 2：待审核
                        BatchId: "",//订单编号勾选组 值：订单ID
                        OrderId: 0,
                        IsRefund: '',//是否查询红包退款
                        ExportData: "",//导出字段名
                        popoverVal: "",//作废原因或通过原因
                    },
                    popoverVisible: false,//作废弹窗
                    popoverType:"",//弹窗显示类型
                    SuccessVal: [],
                    FailVal: ['时间不清楚','门店信息不清楚','流水号不清楚','产品信息不清楚'],

                },
                methods:{
                    Init: function (Parameter, Type) {

                        Parameter.GetType = Type == 0 ? "InitData" : Parameter.GetType;//0：初始化和搜索 1：提交事件
                        
                        $.post(vm.urls.ChangOrderUrl, Parameter, function (result) {

                            var resultData = $.parseJSON(result);
                            
                            var ObjectValue = $.parseJSON(resultData.ObjectValue);
                             
                            if (ObjectValue != null) {
                                vm.OrderInfoList = $.parseJSON(resultData.ObjectValue);
                                vm.Parameter.PageCount = resultData.EffectRows; 
                                 
                                OrderIdOptions = [];
                                vm.BatchIdArray = [];
                                vm.checkAll = false;
                                //存储当前页Id，全选时需要
                                for (var i = 0; i < vm.OrderInfoList.length; i++)
                                    OrderIdOptions.push(vm.OrderInfoList[i].Id);
                            }

                            if (Type == 1 || resultData.ErrMessage.length>0) {
                                vm.$notify({
                                    type: resultData.Success ? "success" : "error",
                                    message: resultData.Success ? "提交成功" : resultData.ErrMessage,
                                    offset: 100,
                                    duration: resultData.Success ? 2000 : 5000
                                });
                            }
                             
                        });

                        
                    },
                    InitCheckedNames: function () {
                        //初始化显示勾选项组
                        for (var i = 0; i < vm.OrderInfoKeyName.length; i++)
                        {
                            if (vm.OrderInfoKeyName[i].IsShow) {
                                vm.OrderInfoCheckedNames.push(i);
                            }
                        }
                            
                    },
                    ShowNotes: function (Id) {
                        $("#txt_notes_" + Id).toggle();
                    }, 
                    ImgApi: function (angle, NewImgurl, FileName, NewImgsign, Hashdata, OrderCode, CreateTime, Id) {
                        ImgApi({ imgurl: NewImgurl == null ? FileName : NewImgurl, imgsign: NewImgsign == null ? Hashdata : NewImgsign, ordercode: OrderCode, angle: angle, opt: 'rotImg' + Id, successid: 'successid' + Id, failid: 'fail' + Id, ordertime: CreateTime, cancelReason:JSON.parse('<%=cancelReason%>'), orderid: Id });
                    },
                    openPhotoGallery: function (Id) {
                        $(".FileImg").viewer();
                        $("#rotImg" + Id).click();
                    },
                    IsShow: function (index) { 
                        return vm.OrderInfoKeyName[index].IsShow ? "" : "none";
                    },
                    SetStarDate: function (val) {
                        vm.Parameter.StarTimeText = val;
                    },
                    SetEndDate: function (val) {
                        vm.Parameter.EndTimeText = val;
                    },
                    GetStatusOrderList: function (type) {  
                        vm.Parameter.OrderStatusType = type; 
                        vm.Init(vm.Parameter, 0);
                    },
                    handleCheckAllChange: function (event) {
                        vm.BatchIdArray = event.target.checked ? OrderIdOptions : [];
                    },
                    handleCheckedCitiesChange: function (value) {
                        var checkedCount = value.length;
                        vm.checkAll = checkedCount === this.cities.length;
                        //this.isIndeterminate = checkedCount > 0 && checkedCount < this.cities.length;
                    },
                    MessageBox: function (val, Id, Num, Reason) {
                        // num:数组序号 Reason:作废原因 
                         
                        vm.Parameter.OrderId = Id;
                        vm.Parameter.GetType = val;

                        //当作废或通过操作需要选择时
                        if (( val == "Fail") && (vm.SuccessVal.length > 0 || vm.FailVal.length > 0)) {
                            
                            //弹出泡泡框选择
                            vm.Popover(val);
                            return;
                        }

                        vm.Parameter.BatchId = vm.BatchIdArray.join(",");

                        var h = this.$createElement;
                        this.$msgbox({
                            title: '消息',
                            type: 'warning',
                            message: h('p', null, [ h('span', null, '确认提交？ ')]),
                            showCancelButton: true,
                            confirmButtonText: '确定',
                            cancelButtonText: '取消',
                            beforeClose: function (action, instance, done) {
                                if (action === 'confirm') {
                                  
                                    if (val == "SaveRemark") {

                                        vm.Parameter.Store = $("#store" + Id).val();
                                        vm.Parameter.Times = $("#times" + Id).val();
                                        vm.Parameter.Nums = $("#nums" + Id).val();
                                        vm.Parameter.Price = $("#price" + Id).val(); 
                                    }

                                    vm.Init(vm.Parameter, 1);

                                    instance.confirmButtonLoading = false;
                                      
                                }

                                done();
                                
                            }
                        })
                    },
                    handleSizeChange: function (val) {
                        vm.Parameter.PageSize = val;
                        vm.Init(vm.Parameter, 0);
                    },
                    handleCurrentChange: function (val) {
                        vm.Parameter.PageIndex = val;
                        vm.Init(vm.Parameter, 0);
                    },
                    Export: function () {
                       
                        var Checked = "";
                        for (var ck = 0; ck < vm.OrderInfoCheckedNames.length; ck++) {

                            var Key = vm.OrderInfoKeyName[vm.OrderInfoCheckedNames[ck]].Key;
                            var Name = vm.OrderInfoKeyName[vm.OrderInfoCheckedNames[ck]].Name;

                            if (Key == "a.States") {
                                Key = "case " + Key + " when 0 then '待审核' when 1 then '已审核' else '已作废' end";
                            }
                            if (Key == "a.RedPackMoney") {
                                Key = "convert(varchar," + Key + "/100)+'.'+convert(varchar," + Key + "%100)"; //红包金额导出时除以100
                            }
                            Checked += Key + " as " + Name + ",";
                        } 
                        vm.Parameter.ExportData = Checked;
                        vm.Parameter.GetType = "Export";

                        window.location.href = "/MyAdmin/Order_Vue/UpdateOrder.aspx?" + $.param(vm.Parameter);
                    },
                    Popover: function (val) {
                        vm.popoverType = val;
                        vm.popoverVisible = true; 
                    },
                    ChooseVal: function (val) {
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
                                     
                                    vm.Parameter.popoverVal = val;
                                   
                                    vm.Init(vm.Parameter, 1);

                                    instance.confirmButtonLoading = false;
                                }

                                vm.popoverType = "";
                                vm.popoverVisible = false;

                                done();
                            }
                        })
                    }
                },
                watch:{
                    OrderInfoCheckedNames: function (val) {
                         
                        for (var i = 0; i < vm.OrderInfoKeyName.length; i++) {
                            if (val.indexOf(i) != -1)
                                vm.OrderInfoKeyName[i].IsShow = true;
                            else
                                vm.OrderInfoKeyName[i].IsShow = false;
                        }
                         
                    } 
                }
                 
            });

            $(function () {

                vm.Init(vm.Parameter, 0);

                vm.InitCheckedNames();

                vm.AwardsOptions =JSON.parse('<%=AwardsOptions%>');

                vm.GroupId = '<%=userseesion.GroupId%>';

            })

        </script>



    </form>
</body>
</html>

 





 