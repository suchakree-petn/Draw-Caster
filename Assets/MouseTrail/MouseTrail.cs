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

        Trail = Instantiate(TrailPrefab, DrawCasterUtil.GetCurrentMousePosition(), Quaternion.identity).transform;
    }
    public void DisableMouseTrail()
    {
        isActive = false;

        Destroy(Trail.gameObject);
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
