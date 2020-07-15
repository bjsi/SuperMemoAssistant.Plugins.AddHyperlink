using Forge.Forms.Annotations;
using Newtonsoft.Json;
using SuperMemoAssistant.Services.UI.Configuration;
using SuperMemoAssistant.Sys.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMemoAssistant.Plugins.AddHyperlink
{

  [Form(Mode = DefaultFields.None)]
  [Title("Dictionary Settings",
       IsVisible = "{Env DialogHostContext}")]
  [DialogAction("cancel",
      "Cancel",
      IsCancel = true)]
  [DialogAction("save",
      "Save",
      IsDefault = true,
      Validates = true)]
  public class AddHyperlinkCfg : CfgBase<AddHyperlinkCfg>, INotifyPropertyChangedEx
  {

    [Title("Add Hyperlink Plugin")]

    [Heading("By Jamesb | Experimental Learning")]

    [Heading("Features:")]
    [Text(@"- Provides a dialog to insert a hyperlink.
- Optionally integrates with Dev Context Menu.")]

    [Heading("Integration Settings")]
    [Field(Name = "Add to Dev Context Menu?")]
    public bool AddToDevContextMenu { get; set; } = true;

    [JsonIgnore]
    public bool IsChanged { get; set; }

    public override string ToString()
    {
      return "Add Hyperlink Settings";
    }

    public event PropertyChangedEventHandler PropertyChanged;

  }
}
