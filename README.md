# Windows Unity File Bridge
  
This example provide that hook drag end event from windows api.  
Plugin is built with cpp and it will be shared as open source after cleaning up soon.  

## Video
https://user-images.githubusercontent.com/79823287/123329700-103a9a80-d578-11eb-897a-6b97d196657a.mp4
  
## API
See [FileBridge.cs](Assets/FileBridge/FileBridge.cs).  
  



- `FileBridge.Enable()` : Enable hook
- `FileBridge.Disable()` : Disable hook
- `FileBridge.OnDragFiles` : Callback event when dragging end.
  

## Example
See [FileBridgeExample.cs](Assets/FileBridge/FileBridgeExample.cs).  
  
```csharp
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DLavender.Windows
{
    public class FileBridgeExample : MonoBehaviour
    {
        public Text board = default;
        
        private void OnEnable()
        {
            FileBridge.Enable();
            FileBridge.OnDragFiles += OnDragCallback;
        }

        private void OnDisable()
        {
            FileBridge.Disable();
        }

        private void OnDragCallback(List<string> paths)
        {
            board.text = "";
            foreach (string path in paths)
            {
                board.text += path;
                board.text += "\n";
            }
        }
    }
}
```
