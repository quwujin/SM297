using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFramework;

/// <summary>
/// CacheBase 的摘要说明
/// </summary>
public static class CacheBase
{

    private static object _SyncLock = new object();

    #region IP限制模型
    private static Model.Security.IpAccessControlSettingModel _ipSettingModel;
    public static Model.Security.IpAccessControlSettingModel IPSettingModel
    {
        get
        {
            if (_ipSettingModel == null)
            {
                lock (_SyncLock)
                {
                    if (_ipSettingModel == null)
                    {
                        _ipSettingModel = new Model.Security.IpAccessControlSettingModel();
                        _ipSettingModel.IPAccessEnable = true;
                        _ipSettingModel.IPAccessControlLockTime = 720;
                        _ipSettingModel.IPAccessControlTime = 720;
                        _ipSettingModel.IPAccessMaxCount = 5;
                        _ipSettingModel.LogType = "IPErr";
                    }
                }
            }

            return _ipSettingModel;
        }
    }
    #endregion

    #region Mobile限制模型
    private static Model.Security.IpAccessControlSettingModel _MobileSettingModel;
    public static Model.Security.IpAccessControlSettingModel MobileSettingModel
    {
        get
        {
            if (_MobileSettingModel == null)
            {
                lock (_SyncLock)
                {
                    if (_MobileSettingModel == null)
                    {
                        _MobileSettingModel = new Model.Security.IpAccessControlSettingModel();
                        _MobileSettingModel.IPAccessEnable = true;
                        _MobileSettingModel.IPAccessControlLockTime = 720;
                        _MobileSettingModel.IPAccessControlTime = 720;
                        _MobileSettingModel.IPAccessMaxCount = 5;
                        _MobileSettingModel.LogType = "MobileErr";
                    }
                }
            }

            return _MobileSettingModel;
        }
    }
    #endregion

    #region OpenId限制模型
    private static Model.Security.IpAccessControlSettingModel _OpenIdSettingModel;
    public static Model.Security.IpAccessControlSettingModel OpenIdSettingModel
    {
        get
        {
            if (_OpenIdSettingModel == null)
            {
                lock (_SyncLock)
                {
                    if (_OpenIdSettingModel == null)
                    {
                        _OpenIdSettingModel = new Model.Security.IpAccessControlSettingModel();
                        _OpenIdSettingModel.IPAccessEnable = true;
                        _OpenIdSettingModel.IPAccessControlLockTime = 720;
                        _OpenIdSettingModel.IPAccessControlTime = 720;
                        _OpenIdSettingModel.IPAccessMaxCount = 5;
                        _OpenIdSettingModel.LogType = "OpenIdErr";
                    }
                }
            }

            return _OpenIdSettingModel;
        }
    }
    #endregion

    #region 项目信息缓存存储

    private static List<Model.ConfigModel> ConfigList = new List<Model.ConfigModel>();
    private static string Cachekey = "YH_object";

    public static List<Model.ConfigModel> FindCacheObjectInfo
    {
        get
        {
            lock (_SyncLock)
            {
                if (CacheHelper.Exists(Cachekey))
                {
                    CacheHelper.Get<List<Model.ConfigModel>>(Cachekey, out ConfigList);
                }
                else
                {
                    ConfigList = new Db.ConfigDal().GetModelList();

                    CacheHelper.Add<List<Model.ConfigModel>>(ConfigList, Cachekey);
                }
                return ConfigList;
            }
        }
    }

    public static string ClearCacheObjectInfo
    {
        get
        {
            CacheHelper.Clear(Cachekey);

            return "清除成功";
        }
    }
    #endregion


}