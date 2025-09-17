using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoPlaySceneLoader
{
    // Set this to the path of the scene you want to start with
    private const string StartScenePath = "Assets/Scenes/MainMenu.unity";

    static AutoPlaySceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadStartScene;
    }

    private static void LoadStartScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // If already in the desired scene, just play
            if (EditorSceneManager.GetActiveScene().path == StartScenePath)
                return;

            // Popup before switching
            bool goToMainMenu = EditorUtility.DisplayDialog(
                "Play Mode Scene Choice",
                "Do you want to start from the Main Menu scene?\n\n" +
                "Yes = Load MainMenu and Play\nNo = Stay in current scene",
                "Yes (MainMenu)",
                "No (Current Scene)"
            );

            if (goToMainMenu)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(StartScenePath);
                }
                else
                {
                    EditorApplication.isPlaying = false; // cancel play
                }
            }
        }
    }
}
