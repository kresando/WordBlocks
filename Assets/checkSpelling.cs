using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnapZoneSpellChecker : MonoBehaviour
{
    public List<SnapZoneTracker> snapZones; // Set these in the Inspector
    public string correctWord = "ACHIEVE";

    public TextMeshProUGUI resultTextTMP;

    void Update()
    {
        if (AllZonesFilled())
        {
            string assembledWord = "";

            foreach (var zone in snapZones)
            {
                assembledWord += zone.currentLetter.ToUpper();
            }

            if (assembledWord == correctWord)
            {
                resultTextTMP.text = "Correct!";
            }
            else
            {
                resultTextTMP.text = "Incorrect!";
            }
        }
        else
        {
            resultTextTMP.text = "Waiting...";
        }
    }

    bool AllZonesFilled()
    {
        foreach (var zone in snapZones)
        {
            if (string.IsNullOrEmpty(zone.currentLetter))
                return false;
        }
        return true;
    }
}

