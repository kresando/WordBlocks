using System.Collections.Generic;
using UnityEngine;

public class WordController : MonoBehaviour
{
    public List<GameObject> wordObjects;
    private int currentWordIndex = -1;
    
    [Tooltip("Set to FALSE if using MainMenuController to control game start")]
    public bool autoStartOnEnable = true;

    void Start()
    {
        foreach (GameObject word in wordObjects)
        {
            word.SetActive(false);
        }

        // Only auto-load first word if autoStartOnEnable is true
        if (autoStartOnEnable)
        {
            LoadNextWord();
        }
    }

    public void LoadNextWord()
    {
        // Hide and reset current word
        if (currentWordIndex >= 0 && currentWordIndex < wordObjects.Count)
        {
            GameObject currentWord = wordObjects[currentWordIndex];
            ScrambleThenDrop std = currentWord.GetComponent<ScrambleThenDrop>();
            if (std != null)
            {
                std.ResetLetters();
            }
            currentWord.SetActive(false);
        }

        // Next word
        currentWordIndex = (currentWordIndex + 1) % wordObjects.Count;

        // Show and drop
        GameObject newWord = wordObjects[currentWordIndex];
        newWord.SetActive(true);

        // adding audio 
        AudioSource audio = newWord.GetComponent<AudioSource>();
        if (audio != null)
        {
        audio.Play();
        }

        ScrambleThenDrop newStd = newWord.GetComponent<ScrambleThenDrop>();
        if (newStd != null)
        {
            float delay = 0f;
            AudioSource clipSource = newWord.GetComponent<AudioSource>();
            if (audio != null && audio.clip != null)
            {
                delay = clipSource.clip.length; // get length of audio
            }

            newStd.StartDropSequenceAfterDelay(delay);
        }
    }
}


