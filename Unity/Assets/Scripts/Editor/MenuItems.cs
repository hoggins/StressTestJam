using UnityEngine;
using UnityEditor;

namespace DefaultNamespace.Editor
{
    public class MenuItems
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        private static void NewMenuOption()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}