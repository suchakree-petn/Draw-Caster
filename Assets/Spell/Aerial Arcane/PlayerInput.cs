using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<GameObject> onMouseClick;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Assuming left mouse button for casting
        {
            onMouseClick?.Invoke(gameObject);
        }
    }

    public void AddMouseListener(UnityAction<GameObject> listener)
    {
        onMouseClick.AddListener(listener);
    }

    public void RemoveMouseListener(UnityAction<GameObject> listener)
    {
        onMouseClick.RemoveListener(listener);
    }
}
