using UnityEngine;

public class AudioResetProxy : MonoBehaviour
{
    public AudioPlayer playerScript;

    private bool isTouching = false;

    void Update()
    {
        if (!gameObject.activeSelf) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (touch.phase == TouchPhase.Began && hit.transform == transform)
                {
                    isTouching = true;
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isTouching)
                {
                    isTouching = false;
                    if (hit.transform == transform)
                        playerScript.StopAndReset();
                }
            }
        }
    }
}
