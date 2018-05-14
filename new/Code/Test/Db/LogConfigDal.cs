using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Model;

namespace Db
{
    public class LogConfigDal
    {
        public List<LogConfigModel> ReaderLogConfig()
        {
            try
            {
                List<LogConfigModel> logConfigModels = new List<LogConfigModel>();
                XmlDocument doc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log.config";
                XmlReader reader = XmlReader.Create(filePath, settings);
                doc.Load(reader);
                reader.Close();

                XmlNode rootNode = doc.SelectSingleNode("configuration");
                if (rootNode != null)
                {
                    XmlNodeList nodeList = rootNode.ChildNodes;
                    bool enable;
                    foreach (var xn in nodeList)
                    {
                        XmlElement xe = (XmlElement)xn;
                        if (xe.Name == "ErrorLog")
                        {
                            if (bool.TryParse(xe.GetAttribute("enable"), out enable))
                            {
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "ErrorEnable",
                                    Types = 0,
                                    Val = enable ? "true" : "false",
                                    OldValue = enable ? "true" : "false",
                                    NodeName = "ErrorLog",
                                    Attribute = "enable"
                                });

                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "ErrorAppenders",
                                    Types = 1,
                                    Val = xe.GetAttribute("appenders").ToLower(),
                                    OldValue = xe.GetAttribute("appenders").ToLower(),
                                    NodeName = "ErrorLog",
                                    Attribute = "appenders"
                                });

                            }
                        }
                        else if (xe.Name == "DebugLog")
                        {
                            if (bool.TryParse(xe.GetAttribute("enable"), out enable))
                            {
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "DebugEnable",
                                    Types = 0,
                                    Val = enable ? "true" : "false",
                                    OldValue = enable ? "true" : "false",
                                    NodeName = "DebugLog",
                                    Attribute = "enable"
                                });

                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "DebugAppenders",
                                    Types = 1,
                                    Val = xe.GetAttribute("appenders").ToLower(),
                                    OldValue = xe.GetAttribute("appenders").ToLower(),
                                    NodeName = "DebugLog",
                                    Attribute = "appenders"
                                });
                            }
                        }
                        else if (xe.Name == "WarningLog")
                        {
                            if (bool.TryParse(xe.GetAttribute("enable"), out enable))
                            {
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "WarningEnable",
                                    Types = 0,
                                    Val = enable ? "true" : "false",
                                    OldValue = enable ? "true" : "false",
                                    NodeName = "WarningLog",
                                    Attribute = "enable"
                                });

                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "WarningAppenders",
                                    Types = 1,
                                    Val = xe.GetAttribute("appenders").ToLower(),
                                    OldValue = xe.GetAttribute("appenders").ToLower(),
                                    NodeName = "WarningLog",
                                    Attribute = "appenders"
                                });
                            }
                        }
                        else if (xe.Name == "InfoLog")
                        {
                            if (bool.TryParse(xe.GetAttribute("enable"), out enable))
                            {
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "InfoEnable",
                                    Types = 0,
                                    Val = enable ? "true" : "false",
                                    OldValue = enable ? "true" : "false",
                                    NodeName = "InfoLog",
                                    Attribute = "enable"
                                });
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "InfoAppenders",
                                    Types = 1,
                                    Val = xe.GetAttribute("appenders").ToLower(),
                                    OldValue = xe.GetAttribute("appenders").ToLower(),
                                    NodeName = "InfoLog",
                                    Attribute = "appenders"
                                });
                            }
                        }
                        else if (xe.Name == "PerformanceLog")
                        {
                            if (bool.TryParse(xe.GetAttribute("enable"), out enable))
                            {
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "PerfermanceEnable",
                                    Types = 0,
                                    Val = enable ? "true" : "false",
                                    OldValue = enable ? "true" : "false",
                                    NodeName = "PerformanceLog",
                                    Attribute = "enable"
                                });
                                logConfigModels.Add(new LogConfigModel()
                                {
                                    TabId = 1,
                                    Title = "PerfermanceAppenders",
                                    Types = 1,
                                    Val = xe.GetAttribute("appenders").ToLower(),
                                    OldValue = xe.GetAttribute("appenders").ToLower(),
                                    NodeName = "PerformanceLog",
                                    Attribute = "appenders"
                                });
                            }
                        }
                        else if (xe.Name == "AppId")
                        {
                            logConfigModels.Add(new LogConfigModel()
                            {
                                TabId = 1,
                                Title = "APPId",
                                Types = 1,
                                Val = xe.GetAttribute("value").ToLower(),
                                OldValue = xe.GetAttribute("value").ToLower(),
                                NodeName = "AppId",
                                Attribute = "value"
                            });
                        }
                        else if (xe.Name == "Appenders")
                        {
                            var appendersNodeList = xe.ChildNodes;
                            foreach (var appender in appendersNodeList)
                            {
                                XmlElement xmle = (XmlElement)appender;
                                if (xmle.GetAttribute("name") == "File")
                                {
                                    XmlNodeList paramList = xmle.ChildNodes;
                                    if (paramList.Count > 0)
                                    {
                                        foreach (var para in paramList)
                                        {
                                            XmlElement xeParam = (XmlElement)para;
                                            if (xeParam.GetAttribute("name") == "directory")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 2,
                                                    Title = "directory",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "childDirectoryFormat")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 2,
                                                    Title = "childDirectoryFormat",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "divideByType")
                                            {
                                                bool divideByLogLevel;
                                                if (bool.TryParse(xeParam.GetAttribute("value"), out divideByLogLevel))
                                                {
                                                    logConfigModels.Add(new LogConfigModel()
                                                    {
                                                        TabId = 2,
                                                        Title = "divideByType",
                                                        Types = 0,
                                                        Val = divideByLogLevel ? "true" : "false",
                                                        OldValue = divideByLogLevel ? "true" : "false",
                                                        NodeName = "param",
                                                        Attribute = "name"
                                                    });
                                                }
                                            }
                                            else if (xeParam.GetAttribute("name") == "fileNameFormat")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 2,
                                                    Title = "fileNameFormat",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                        }
                                    }
                                }
                                else if (xmle.GetAttribute("name") == "Email")
                                {
                                    XmlNodeList paramList = xmle.ChildNodes;
                                    if (paramList.Count > 0)
                                    {
                                        foreach (var para in paramList)
                                        {
                                            XmlElement xeParam = (XmlElement)para;
                                            if (xeParam.GetAttribute("name") == "smtpServer")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "smtpServer",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "user")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "user",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "password")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "password",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "subject")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "subject",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "from")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "from",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "fromName")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "fromName",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "to")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "to",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "bcc")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "bcc",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "cc")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "cc",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "enableSsl")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "enableSsl",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "sslPort")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 3,
                                                    Title = "sslPort",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                        }
                                    }
                                }
                                else if (xmle.GetAttribute("name") == "DB")
                                {
                                    XmlNodeList paramList = xmle.ChildNodes;
                                    if (paramList.Count > 0)
                                    {
                                        foreach (var para in paramList)
                                        {
                                            XmlElement xeParam = (XmlElement)para;
                                            if (xeParam.GetAttribute("name") == "connectionStrings")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 4,
                                                    Title = "connectionStrings",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "dbtype")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 4,
                                                    Title = "dbtype",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                        }
                                    }
                                }
                                else if (xmle.GetAttribute("name") == "LogApi")
                                {
                                    XmlNodeList paramList = xmle.ChildNodes;
                                    if (paramList.Count > 0)
                                    {
                                        foreach (var para in paramList)
                                        {
                                            XmlElement xeParam = (XmlElement)para;
                                            if (xeParam.GetAttribute("name") == "host")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 5,
                                                    Title = "host",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                            else if (xeParam.GetAttribute("name") == "secretKey")
                                            {
                                                logConfigModels.Add(new LogConfigModel()
                                                {
                                                    TabId = 5,
                                                    Title = "secretKey",
                                                    Types = 1,
                                                    Val = xeParam.GetAttribute("value"),
                                                    OldValue = xeParam.GetAttribute("value"),
                                                    NodeName = "param",
                                                    Attribute = "name"
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return logConfigModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LogConfigModel> GetConfigModelsByTabId(List<LogConfigModel> logModelList, int tabId)
        {
            List<LogConfigModel> selectedModelList = new List<LogConfigModel>();
            if (logModelList == null)
            {
                logModelList = ReaderLogConfig();
            }
            if (logModelList.Count > 0)
            {
                selectedModelList.AddRange(logModelList.Where(x => x.TabId == tabId).ToList());
            }
            return selectedModelList;
        }

        public void UpdateLogItem(LogConfigModel updateModel)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = false;
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Log.config";
                XmlReader reader = XmlReader.Create(filePath, settings);
                doc.Load(reader);
                reader.Close();

                if (updateModel.NodeName == "AppId" || updateModel.NodeName == "ErrorLog" || updateModel.NodeName == " DebugLog" || updateModel.NodeName == "WarningLog" || updateModel.NodeName == "InfoLog" || updateModel.NodeName == "PerformanceLog")
                {
                    var selectedNode = doc.SelectSingleNode(string.Format("configuration//{0}", updateModel.NodeName));
                    if (selectedNode != null)
                    {
                        XmlElement xmlEle = (XmlElement)selectedNode;
                        if (xmlEle.GetAttribute(updateModel.Attribute) == updateModel.OldValue)
                        {
                            xmlEle.SetAttribute(updateModel.Attribute, updateModel.Val);
                        }
                    }
                }
                else
                {
                    var selectedNode = doc.SelectSingleNode(string.Format("configuration//{0}[@{1}='{2}']", updateModel.NodeName, updateModel.Attribute, updateModel.Title));
                    if (selectedNode != null)
                    {
                        XmlElement xmlEle = (XmlElement)selectedNode;
                        if (xmlEle.GetAttribute("value") == updateModel.OldValue)
                        {
                            xmlEle.SetAttribute("value", updateModel.Val);
                        }
                    }
                }
                doc.Save(filePath);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
