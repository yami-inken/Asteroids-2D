using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoPlaySceneLoader
{
    private const string StartScenePath = "Assets/Scenes/Menu Screen.unity"; // Your main menu scene path
    private static string _previousScenePath; // To remember the scene you were in

    static AutoPlaySceneLoader()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // When you press "Play" in the editor
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // If we are already in the main menu, do nothing.
            if (EditorSceneManager.GetActiveScene().path == StartScenePath)
            {
                return;
            }

            // Ask the user if they want to switch scenes
            bool goToMainMenu = EditorUtility.DisplayDialog(
                "Start From Main Menu?",
                "Do you want to start playing from the Main Menu scene?",
                "Yes, Start from Main Menu",
                "No, Use Current Scene"
            );

            if (goToMainMenu)
            {
                // Stop the editor from entering play mode immediately
                EditorApplication.isPlaying = false;

                // Save changes to the current scene if the user wants to
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    // Remember where we were, so we can return later
                    _previousScenePath = EditorSceneManager.GetActiveScene().path;
                    // Open the main menu scene
                    EditorSceneManager.OpenScene(StartScenePath);
                    // Tell the editor to enter play mode on the next available frame
                    EditorApplication.update += EnterPlayModeOnNextUpdate;
                }
            }
        }

        // When you stop playing and return to the editor
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            // If we have a stored previous scene, load it back
            if (!string.IsNullOrEmpty(_previousScenePath))
            {
                EditorSceneManager.OpenScene(_previousScenePath);
                _previousScenePath = null; // Clear it so it doesn't happen again
            }
        }
    }

    private static void EnterPlayModeOnNextUpdate()
    {
        // Unsubscribe from the event so this only runs once
        EditorApplication.update -= EnterPlayModeOnNextUpdate;
        // Now, enter play mode
        EditorApplication.isPlaying = true;
    }
}
