using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControls : MonoBehaviour
{
    public InputSystem_Actions inputActions;

    // Inputs
    public InputAction advance;


    private void Start()
    {
        advance = inputActions.Controls.Advance;
    }


    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    // Singleton
    public static InputControls instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        inputActions = new InputSystem_Actions();
    }
}
