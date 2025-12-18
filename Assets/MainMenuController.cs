using UnityEngine;

/// <summary>
/// Controls the Main Menu and game start functionality for VR Spelling Game.
/// Attach this to a GameManager object in the scene.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("Menu Panels")]
    [Tooltip("The main menu panel/canvas that contains Play and Quit buttons")]
    public GameObject mainMenuPanel;
    
    [Tooltip("Parent object containing all game objects (word objects like ACHIEVE, RELIEF, etc.)")]
    public GameObject gamePanel;
    
    [Header("References")]
    [Tooltip("Reference to the WordController that manages word loading")]
    public WordController wordController;
    
    [Header("Audio (Optional)")]
    [Tooltip("Sound to play when button is clicked")]
    public AudioClip buttonClickSound;
    private AudioSource audioSource;
    
    void Start()
    {
        // Setup audio source if button sound is assigned
        if (buttonClickSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        
        // Show main menu on start
        ShowMainMenu();
    }
    
    /// <summary>
    /// Shows the main menu and hides the game content
    /// </summary>
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
            
        if (gamePanel != null)
            gamePanel.SetActive(false);
    }
    
    /// <summary>
    /// Called when Play button is pressed. Starts the game.
    /// </summary>
    public void StartGame()
    {
        PlayButtonSound();
        
        // Hide menu, show game
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
            
        if (gamePanel != null)
            gamePanel.SetActive(true);
        
        // Start the game by loading the first word
        if (wordController != null)
        {
            wordController.LoadNextWord();
        }
        else
        {
            Debug.LogWarning("MainMenuController: WordController reference is not assigned!");
        }
    }
    
    /// <summary>
    /// Called when Quit button is pressed. Exits the application.
    /// </summary>
    public void QuitGame()
    {
        PlayButtonSound();
        
        Debug.Log("Quitting game...");
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    /// <summary>
    /// Returns to main menu from game (can be called from a "Back to Menu" button)
    /// </summary>
    public void ReturnToMainMenu()
    {
        PlayButtonSound();
        ShowMainMenu();
    }
    
    private void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
