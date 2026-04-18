using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControls : MonoBehaviour
{
    public InputSystem_Actions inputActions;

    // Inputs
    public InputAction advance;
    public InputAction pause;


    private void Start()
    {
        advance = inputActions.Controls.Advance;
        pause = inputActions.Controls.Pause;
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
    public static InputControls Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        inputActions = new InputSystem_Actions();
    }
}
