using UnityEngine;

public partial class Player
{
    [Header("Aiming")]
    [SerializeField] private Transform headTransform;

    private float xRotation;

    private void AimUpdate()
    {
        float mouseX = 0;
        float mouseY = 0;

        if (takeInput)
        {
            mouseX = Input.GetAxis("Mouse X") * gameOptions.mouseSensitivity;
            mouseY = Input.GetAxis("Mouse Y") * gameOptions.mouseSensitivity;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        var headRotation = Mathf.Clamp(xRotation * 2, -45, 35f);
        
        headTransform.localRotation = Quaternion.Euler(headRotation, 0f, 0f);
        
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    
    private void EyeTrace()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hitInfo, 4, hitMask))
        {
            if (hitInfo.transform.TryGetComponent(out Car vehicle))
            {
                aimText.SetText(vehicle.name);
                lookingAt = vehicle.gameObject;
                return;
            }
            
            if (hitInfo.transform.TryGetComponent(out Resource resource))
            {
                aimText.SetText(resource.resourceName);
                lookingAt = resource.gameObject;
                return;
            }
        }
        
        lookingAt = null;
        aimText.SetText(string.Empty);
    }
    
    private void EyeTraceInfo()
    {
        if (lookingAt == null) return;

        if (lookingAt.TryGetComponent(out Car vehicle))
        {
            if (InputHelper.GetKeyDown(gameOptions.useKey, 0.2f))
            {
                vehicle.CarEnterServerRpc(this);
            }
        }
        
        if (lookingAt.TryGetComponent(out Resource resource))
        {
            //mudar isso para dar play na anima��o correspondente � ferramenta (machado / picareta / enxada) e usar o trigger DA anima�ao para receber o resource
            if (InputHelper.GetKeyDown(gameOptions.primaryAttackKey, 0.2f))
            {
                resource.HitResourceServerRpc(this, resource.resourceName, 2);
                //debug de anima��o
                switch (resource.resourceName)
                {
                    case "Tree":
                        firstPersonAnimator.SetTrigger("UseAxe");
                        break;
                    case "Stone":
                        firstPersonAnimator.SetTrigger("UsePickaxe");
                        break;
                }
                
            }
        }
    }
}
