using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onstartbutton()
    {
        SceneManager.LoadScene("SpaceScene"); // Load the main game scene when the start button is pressed
    }

    public void onexitbutton()
    {
        Application.Quit(); // Exit the application when the exit button is pressed
        Debug.Log("Exit button pressed, application will close."); // Log message for debugging
    }
}
