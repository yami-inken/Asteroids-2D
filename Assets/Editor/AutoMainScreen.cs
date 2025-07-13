using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoMainScreen
{
    // Set this to the path of the scene you want to start with
    private const string StartScenePath = "Assets/Scenes/Menu Screen.unity";

    static AutoMainScreen()
    {
        EditorApplication.playModeStateChanged += LoadStartScene;
    }

    private static void LoadStartScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (EditorSceneManager.GetActiveScene().path != StartScenePath)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(StartScenePath);
                }
                else
                {
                    EditorApplication.isPlaying = false; // cancel play if user doesn't save
                }
            }
        }
    }
}
