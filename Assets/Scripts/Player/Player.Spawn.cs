using Unity.Mathematics;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Player
{
    [Header("Spawning")]
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private float safePositionTimer = 2;
    [SerializeField] private Vector3 safePosition;

    private float respawnTime;
    private float lastSafePosition;

    private void CalculateSafePosition()
    {
        if (isGrounded) safePosition = groundCheck.position;

        lastSafePosition = Time.time;
    }
    
    private void SpawnPlayer()
    {
        transform.position = spawnPosition;
    }

    [ServerRpc]
    private void DropAllItemsServerRpc(NetworkBehaviourReference player)
    {
        if (player.TryGet(out Player ply))
        {
            int slotIndex = -1;
            foreach (var invSlot in ply.inventorySlots)
            {
                slotIndex++;
                if (invSlot.isEmpty) continue;
                
                var worldGameObject = Instantiate(invSlot.inventoryItem.worldPrefab, 
                    safePosition + Random.insideUnitSphere + Vector3.up * 2,
                    invSlot.inventoryItem.worldPrefab.transform.rotation);
                
                worldGameObject.name = invSlot.inventoryItem.name;

                var worldGameObjectInvItem = worldGameObject.GetComponent<InventoryGroundItem>();
                worldGameObjectInvItem.inventoryItem = invSlot.inventoryItem;
                worldGameObjectInvItem.amount.Value = invSlot.Amount;
                worldGameObjectInvItem.Durability = invSlot.Durability;

                worldGameObject.GetComponent<NetworkObject>().Spawn();

                ply.DropItemClientRpc(slotIndex, true);
            }
        }
    }
    
    private void RespawnPlayer(bool dropItems = true)
    {
        if(dropItems) DropAllItemsServerRpc(this);

        // Advanced Movement
        if(dashing) EndDash();
        if(isTethered) EndGrapple();
        if(isTetheredPlus) EndGrapplePlus();
        
        // Basic Movement
        horizontalVelocity = Vector2.zero;
        verticalVelocity = -1f;
        
        // Status
        currentHealth.Value = maxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;

        transform.position = spawnPosition;

        respawnTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(safePosition + Vector3.up * 2, 2);
    }
}
