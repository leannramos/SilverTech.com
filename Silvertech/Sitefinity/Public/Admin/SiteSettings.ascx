<%@ Control Language="C#" %>
<%@ Register Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sitefinity" Assembly="Telerik.Sitefinity" %>

<h2>
    <asp:Literal runat="server" ID="lGeneralSettings" Text="Custom Site Settings" />
</h2>

<sitefinity:TextField ID="gTMHead" runat="server" DisplayMode="Write" DataFieldName="GTMHead" Title="GTM Head" CssClass="sfSettingsSection" ></sitefinity:TextField>
<sitefinity:TextField ID="gTMBody" runat="server" DisplayMode="Write" DataFieldName="GTMBody" Title="GTM Body" CssClass="sfSettingsSection" ></sitefinity:TextField>

<div class="sfSettingsSection">
    <h2>
        <asp:Literal runat="server" ID="lTwitterSettings" Text="Twitter Card Settings" />
    </h2>

    <sitefinity:TextField ID="twitterSite" runat="server" DisplayMode="Write" DataFieldName="TwitterSite" Title="Twitter Site" CssClass="sfSettingsSection" ></sitefinity:TextField>
    <sitefinity:TextField ID="twitterTitle" runat="server" DisplayMode="Write" DataFieldName="TwitterTitle" Title="Twitter Title" CssClass="sfSettingsSection" ></sitefinity:TextField>
    <sitefinity:TextField ID="twitterDescription" runat="server" DisplayMode="Write" DataFieldName="TwitterDescription" Title="Twitter Description" CssClass="sfSettingsSection" ></sitefinity:TextField>
    <sitefinity:TextField ID="twitterImage" runat="server" DisplayMode="Write" DataFieldName="TwitterImage" Title="Twitter Image" CssClass="sfSettingsSection" ></sitefinity:TextField>
</div>