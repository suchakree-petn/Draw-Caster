using DrawCaster.Util;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTrail : MonoBehaviour
{

    public static MouseTrail Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject TrailPrefab;
    public bool isActive;

    public void EnableMouseTrail()
    {
        isActive = true;
        if (Trail == null)
        {
            Trail = Instantiate(TrailPrefab, DrawCasterUtil.GetCurrentMousePosition(), Quaternion.identity).transform;
        }
    }
    public void DisableMouseTrail()
    {
        isActive = false;
        if (Trail != null)
        {
            Destroy(Trail.gameObject);
        }
    }
    Transform Trail;

    private void Update()
    {
        if (isActive)
        {
            Trail.position = DrawCasterUtil.GetCurrentMousePosition();
        }
    }

}
