#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   7/15/2020 7:54:18 PM
// Modified By:  james

#endregion




namespace SuperMemoAssistant.Plugins.AddHyperlink
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Windows;
  using System.Windows.Input;
  using Anotar.Serilog;
  using SuperMemoAssistant.Plugins.AddHyperlink.UI;
  using SuperMemoAssistant.Plugins.DevContextMenu.Interop;
  using SuperMemoAssistant.Services;
  using SuperMemoAssistant.Services.IO.HotKeys;
  using SuperMemoAssistant.Services.IO.Keyboard;
  using SuperMemoAssistant.Services.Sentry;
  using SuperMemoAssistant.Services.UI.Configuration;
  using SuperMemoAssistant.Sys.IO.Devices;
  using SuperMemoAssistant.Sys.Remoting;

  // ReSharper disable once UnusedMember.Global
  // ReSharper disable once ClassNeverInstantiated.Global
  [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
  public class AddHyperlinkPlugin : SentrySMAPluginBase<AddHyperlinkPlugin>
  {
    #region Constructors

    /// <inheritdoc />
    public AddHyperlinkPlugin() : base("Enter your Sentry.io api key (strongly recommended)") { }

    #endregion


    #region Properties Impl - Public

    /// <inheritdoc />
    public override string Name => "AddHyperlink";

    /// <inheritdoc />
    public override bool HasSettings => false;
    public AddHyperlinkCfg Config;

    #endregion

    /// <inheritdoc />
    public override void ShowSettings()
    {
      ConfigurationWindow.ShowAndActivate(HotKeyManager.Instance, Config);
    }

    private void LoadConfig()
    {
      Config = Svc.Configuration.Load<AddHyperlinkCfg>() ?? new AddHyperlinkCfg();
    }

    #region Methods Impl

    /// <inheritdoc />
    protected override void PluginInit()
    {

      LoadConfig();

      Svc.HotKeyManager.RegisterGlobal(
        "OpenInsertHyperlinkDialog",
        "Open a dialog to insert a hyperlink",
        HotKeyScopes.SMBrowser,
        new HotKey(Key.DbeAlphanumeric),
        InsertHyperlink
      );

      if (Config.AddToDevContextMenu)
      {

        var svc = GetService<IDevContextMenu>();
        if (svc.IsNull() || !svc.AddMenuItem(Name, "Insert Hyperlink", new ActionProxy(InsertHyperlink)))
        {
          LogTo.Error("Failed to register InsertHyperlink method with Dev Context Menu");
          return;
        }

        LogTo.Debug("Successfully registered InsertHyperlink method with Dev Context Menu");
      }
      
    }

    private void InsertHyperlink()
    {

      var selObj = ContentUtils.GetSelectionObject();
      if (selObj.IsNull())
        return;

      var res = Application.Current.Dispatcher.Invoke(() =>
      {
        var wdw = new InsertHyperlinkWdw();
        wdw.ShowDialog();
        return wdw;
      });

      if (!res.Confirmed 
        || selObj.IsNull() 
        || res.LinkText.IsNullOrEmpty() 
        || res.LinkUrl.IsNullOrEmpty())
      {
        return;
      }

      selObj.pasteHTML($"<a href='{res.LinkUrl}'>{res.LinkText}</a>");

    }

    #endregion

    #region Methods

    #endregion
  }
}
