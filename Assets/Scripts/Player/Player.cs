using UnityEngine;
using Unity.Netcode;

public partial class Player : NetworkBehaviour
{
    private GameOptionsScriptableObjects gameOptions;
    
    [Header("Player")]
    [SerializeField] private Transform playerCamera;
    
    private void Start()
    {
        if (!IsLocalPlayer)
        {
            firstPersonAnimator.enabled = false;
            enabled = false; // TODO : Isto não devia de ser assim
        }
        else
        {
            gameOptions = GameManager.Instance.gameOptions;
            
            playerCanvas.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(true);
            
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            var cameraMain = Camera.main;
            if(cameraMain != null)
                if(cameraMain != playerCamera.GetComponent<Camera>())
                    cameraMain.gameObject.SetActive(false);
            
            AnimatorStart();
        }
    }

    private void FixedUpdate()
    {
        if (!IsLocalPlayer) return;

        EyeTrace();
    }

    private void Update()
    {
        if (!IsLocalPlayer) return;

        if (InputHelper.GetKeyDown(gameOptions.chatKey, 0.1f)) OpenChat();

        if (car != null)
        {
            CarMovement();
            
            return;
        }
        
        MovementUpdate();

        AimUpdate();
        
        EyeTraceInfo();

        AnimatorUpdate();
    }
}
