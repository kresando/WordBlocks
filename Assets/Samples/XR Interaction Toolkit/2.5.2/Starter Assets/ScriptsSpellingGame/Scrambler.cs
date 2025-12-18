using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrambler : MonoBehaviour
{
    public List<Transform> letterBlocks;

    void Start()
    {
        ScrambleZPositions();
    }

    void ScrambleZPositions()
    {
        // Step 1: Store current Z positions
        List<float> zPositions = new List<float>();
        foreach (Transform letter in letterBlocks)
        {
            zPositions.Add(letter.localPosition.z);
        }

        // Step 2: Shuffle the Z values
        for (int i = 0; i < zPositions.Count; i++)
        {
            float temp = zPositions[i];
            int randIndex = Random.Range(i, zPositions.Count);
            zPositions[i] = zPositions[randIndex];
            zPositions[randIndex] = temp;
        }

        // Step 3: Reassign Z values back in new order
        for (int i = 0; i < letterBlocks.Count; i++)
        {
            Vector3 pos = letterBlocks[i].localPosition;
            pos.z = zPositions[i];
            letterBlocks[i].localPosition = pos;
        }
    }
}