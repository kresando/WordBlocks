using UnityEngine;
using TMPro;
using System.Collections;

public class SpellChecker : MonoBehaviour
{
    public SnapZoneTracker[] snapZones; // Assign in Inspector
    public string targetWord = "ACHIEVE"; // Editable per object
    public TextMeshProUGUI feedbackText;

    public void CheckSpelling()
    {
        if (!gameObject.activeInHierarchy)
            return; // Don't run if this object is inactive

        string userWord = "";

        foreach (SnapZoneTracker zone in snapZones)
        {
            userWord += zone.currentLetter.ToUpper();
        }

        Debug.Log("User spelled: " + userWord + " (expected: " + targetWord + ")");

        if (userWord == targetWord.ToUpper())
        {
            Debug.Log("Correct! You spelled " + targetWord + ".");
            //feedbackText.text = "Correct!";
            ShowFeedback("Correct!", Color.green);
        }
        else
        {
            Debug.Log("Incorrect. Try again.");
            // feedbackText.text = "Try again!";
            ShowFeedback("Try again!", Color.red);
        }
    }

    private void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(true);
            feedbackText.text = message;
            feedbackText.color = color;
            //feedbackText.gameObject.SetActive(true);
            StopAllCoroutines(); // Cancel any previous hides
            StartCoroutine(HideFeedbackAfterDelay(3f));
        }
    }

    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackText.gameObject.SetActive(false);
    }
}





