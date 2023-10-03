/*
using UnityEngine;
using DG.Tweening;

public class RevolveAroundPlayer : MonoBehaviour
{
    public GameObject player;  // Reference to the player
    public float radius = 2f;  // Distance from the player
    public float duration = 5f;  // Duration of one revolution

    void Start()
    {
        Revolve();
    }

    void Revolve()
    {
        // Create a parent object at the player's position
        GameObject parentObject = new GameObject("RevolveParent");
        parentObject.transform.position = player.transform.position;
        transform.SetParent(parentObject.transform);

        // Set the initial position of the object relative to the parent
        transform.localPosition = new Vector3(radius, 0, 0);

        // Rotate the parent object around its Z-axis
        Tween rotateTween = parentObject.transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);  // Infinite loops

        // Optionally, you can add callbacks to the tween
        rotateTween.OnComplete(() => {
            // Code to execute when the rotation is complete (won't be called due to infinite loops)
        });
    }
}
*/
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class RevolveAroundPlayer : MonoBehaviour
{
    public GameObject player;  // Reference to the player
    public float radius = 2f;  // Distance from the player
    public float duration = 5f;  // Duration of one revolution
    public float followInterval = 0.02f;  // Interval at which to update the parent object's position

    private GameObject parentObject;  // Reference to the parent object

    void Start()
    {
        Revolve();
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer()
    {
        while (true)
        {
            if (parentObject != null)
            {
                parentObject.transform.position = player.transform.position;
            }
            yield return new WaitForSeconds(followInterval);
        }
    }

    void Revolve()
    {
        // Create a parent object at the player's position
        parentObject = new GameObject("RevolveParent");
        parentObject.transform.position = player.transform.position;
        transform.SetParent(parentObject.transform);

        // Set the initial position of the object relative to the parent
        transform.localPosition = new Vector3(radius, 0, 0);

        // Rotate the parent object around its Z-axis
        Tween rotateTween = parentObject.transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);  // Infinite loops

        // Optionally, you can add callbacks to the tween
        rotateTween.OnComplete(() => {
            // Code to execute when the rotation is complete (won't be called due to infinite loops)
        });
    }
}

