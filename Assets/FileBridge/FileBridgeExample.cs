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