using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int secsToDestroy = 1;
    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        yield return new WaitForSecondsRealtime(secsToDestroy);
        Destroy(gameObject);
    }
}
