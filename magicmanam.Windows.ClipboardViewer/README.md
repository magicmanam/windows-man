magicmanam.Windows.ClipboardViewer
==============================

`magicmanam.Windows.ClipboardViewer` [nuget package](https://www.nuget.org/packages/magicmanam.Windows.ClipboardViewer) is a simple wrapper over Windows ClipboardViewer.

In order to add it to your solution, run `Install-Package magicmanam.Windows.ClipboardViewer` from your NuGet Package Manager console in Visual Studio. Sample of code you can find below:

```CSharp
using magicmanam.Windows;
using magicmanam.Windows.ClipboardViewer;

class Form {
  private ClipboardViewer _clipboardViewer;

  .....................

  public Form()
  {
    //To make sure that the viewer still ges clipboard events (well-known Windows issue with customer viewers),
    // just call the line below every n-seconds (by timer or other way)
    this._clipboardViewer.RefreshViewer();
  }

  .....................

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == Messages.WM_CREATE)
    {
      this._clipboardViewer = new ClipboardViewer(this.Handle);
    }
    else if (this._clipboardViewer != null)
    {
      this._clipboardViewer.HandleWindowsMessage(m.Msg, m.WParam, m.LParam);
    }
    if (m.Msg == Messages.WM_DRAWCLIPBOARD)
    {
      //After registering you clipboard viewer you can get this event
      var dataObject = System.Windows.Forms.Clipboard.GetDataObject();
    }
    ...........
  }
}
```
