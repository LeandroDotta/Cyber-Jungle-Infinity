using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    private InputAction actionMove;

    public Vector2 Direction
    {
        get
        {
            if (!enabled)
                return Vector2.zero;
                
            return actionMove.ReadValue<Vector2>().normalized;
        }
    }

    private void Start()
    {
        actionMove = actions["Move"];
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
