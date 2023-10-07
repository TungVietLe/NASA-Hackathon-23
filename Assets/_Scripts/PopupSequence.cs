using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Popup Sequence", menuName = "Custom Assets/Popup Sequence")]
public class PopupSequence : ScriptableObject
{
    [System.Serializable]
    public struct Popup
    {
        [TextArea(1, 1)]
        public string Title;
        [TextArea(3, 1)]
        public string Body;
    }

    public List<Popup> popups = new();
}
