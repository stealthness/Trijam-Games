using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Menu
{
    /// <summary>
    /// This class manages the version selection menu.
    /// It allows players to choose between different game versions and loads the corresponding scenes.
    /// </summary>
    public class VersionMenuManager : MonoBehaviour
    {
        
        
        
        
        /// <summary>
        /// This function is called when a game version is selected from the menu.
        /// It loads the corresponding scene based on the selected version.
        /// </summary>
        /// <param name="selection"></param>
        public void OnSelectionClick(GameVersion selection)
        {
            var selectedVersion = selection switch
            {
                GameVersion.TrijamOriginal => "TrijamOriginal",
                GameVersion.TrijamRemastered => "TrijamRemastered",
                GameVersion.UntitledOriginal => "UntitledOriginal",
                GameVersion.UntitledRemastered => "UntitledRemastered",
                _ => "TrijamOriginal"
            };
            SceneManager.LoadScene(selectedVersion);
        }
    }

    public enum GameVersion
    {
        TrijamOriginal,
        TrijamRemastered,
        UntitledOriginal,
        UntitledRemastered
    }
}