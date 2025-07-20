using UnityEngine;
using UnityEngine.UI;

public class LimitReachedUI : MonoBehaviour
{
    [SerializeField] private GameObject messageObject;
    private Coroutine hideCoroutine;

    public void ShowMessage(float duration = 2f)
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        messageObject.SetActive(true);
        hideCoroutine = StartCoroutine(HideAfterSeconds(duration));
    }

    private System.Collections.IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageObject.SetActive(false);
    }
}