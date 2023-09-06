using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    private GraphicRaycaster raycaster;

    private void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    public void ShowCanvas()
    {
        gameObject.SetActive(true);
        raycaster.enabled = true;
    }

    public void HideCanvas()
    {
        // To avoid errors on children objects disabled while they still have some things to do: wait for end of frame
        StartCoroutine(HideAfterCoroutines());
    }

    private IEnumerator HideAfterCoroutines()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        raycaster.enabled = false;
        gameObject.SetActive(false);
    }
}
