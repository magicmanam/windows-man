magicmanam.RemoteManagement
==============================

`magicmanam.RemoteManagement` [nuget package](https://www.nuget.org/packages/magicmanam.RemoteManagement) provides features for work with services and EventViewer on remote boxes.

In order to add it to your solution, run `Install-Package magicmanam.RemoteManagement` from your NuGet Package Manager console in Visual Studio. Sample of code:

Can list Windows services and change service's status on specified remote box.\
As well as read Windows events:

```
using magicmanam.RemoteManagement;

.......
using (var shell = RemoteShell.Create(boxName))
        {
            foreach (var ev in shell.Events.ReadEvents("Application", 15 /* For the last n-minutes */))
            {
                //ev.TimeCreated;
                //ev.LevelDisplayName;
                //ev.FormatDescription();
                //ev.ToXml();
                //...
            }
        }
```
