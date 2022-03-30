using UnityEngine;
using Unity.Netcode;
using UnityEngine.EventSystems;

public partial class Player : NetworkBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform playerCamera;
    
    private void Start()
    {
        if (!IsLocalPlayer)
        {
            enabled = false;
        }
        else
        {
            playerCanvas.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(true);
            
            characterController = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            var cameraMain = Camera.main;
            if(cameraMain != null)
                if(cameraMain != playerCamera.GetComponent<Camera>())
                    cameraMain.gameObject.SetActive(false);   
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

        if (Input.GetKeyDown(KeyCode.Y)) OpenChat();
        
        MovementUpdate();

        AimUpdate();
        
        EyeTraceInfo();
    }
}
