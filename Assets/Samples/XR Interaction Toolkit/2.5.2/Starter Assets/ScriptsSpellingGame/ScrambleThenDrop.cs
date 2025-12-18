using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrambleThenDrop : MonoBehaviour
{
    public List<Transform> letterBlocks;
    public float forwardShift = -1f;
    public float delayBeforeDrop = 1f;

    private List<Vector3> initialPositions = new List<Vector3>();

    void Start()
    {
        foreach (Transform letter in letterBlocks)
        {
            initialPositions.Add(letter.localPosition);
        }

        //StartDropSequence();
    }

    public void StartDropSequenceAfterDelay(float delay)
    {
    StartCoroutine(DelayedStart(delay));
    }

private IEnumerator DelayedStart(float delay)
{
    yield return new WaitForSeconds(delay);
    StartDropSequence();  // same method you already use
}

    public void StartDropSequence()
    {
        StartCoroutine(ScrambleAndDrop());
    }

    public void ResetLetters()
    {
        for (int i = 0; i < letterBlocks.Count; i++)
        {
            Transform letter = letterBlocks[i];
            letter.localPosition = initialPositions[i];

            Rigidbody rb = letter.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    IEnumerator ScrambleAndDrop()
    {
        // Scramble Z positions
        List<float> zPositions = new List<float>();
        foreach (Transform letter in letterBlocks)
        {
            zPositions.Add(letter.localPosition.z);
        }

        for (int i = 0; i < zPositions.Count; i++)
        {
            float temp = zPositions[i];
            int randIndex = Random.Range(i, zPositions.Count);
            zPositions[i] = zPositions[randIndex];
            zPositions[randIndex] = temp;
        }

        for (int i = 0; i < letterBlocks.Count; i++)
        {
            Vector3 pos = letterBlocks[i].localPosition;
            pos.z = zPositions[i];
            letterBlocks[i].localPosition = pos;
        }

        // Wait a bit
        yield return new WaitForSeconds(0.5f);

        // Move forward
        for (int i = 0; i < letterBlocks.Count; i++)
        {
            StartCoroutine(SmoothMove(letterBlocks[i], forwardShift, 0.5f));
        }

        // Wait more
        yield return new WaitForSeconds(delayBeforeDrop);

        // Drop
        foreach (var letter in letterBlocks)
        {
            Rigidbody rb = letter.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }

    IEnumerator SmoothMove(Transform block, float shiftZ, float duration)
    {
        Vector3 startPos = block.position;
        Vector3 endPos = startPos + new Vector3(0, 0, shiftZ);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            block.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        block.position = endPos;
    }
}
