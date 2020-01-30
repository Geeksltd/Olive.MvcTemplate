// ********************************************************************
// WARNING: This file is auto-generated from Model\\Project.cs
// and may be overwritten at any time. Do not change it manually.
// ********************************************************************

namespace MSharp
{
    partial class AppRole
    {
        internal static ProjectRole Local_Request => ProjectRole.Of("Local.Request");
        internal static ProjectRole Anonymous => ProjectRole.Of("Anonymous");
        internal static ProjectRole Admin => ProjectRole.Of("Admin");
    }
    
    partial class Layouts
    {
        internal static MasterPage AdminDefault => MasterPage.Of("Admin default");
        internal static MasterPage AdminDefaultModal => MasterPage.Of("Admin default Modal");
        internal static MasterPage Blank => MasterPage.Of("Blank");
        internal static MasterPage Login => MasterPage.Of("Login");
    }
    
    partial class PageSettings
    {
        internal static PageSettingKey LeftMenu => PageSettingKey.Of("LeftMenu");
        internal static PageSettingKey SubMenu => PageSettingKey.Of("SubMenu");
        internal static PageSettingKey TopMenu => PageSettingKey.Of("TopMenu");
    }
}