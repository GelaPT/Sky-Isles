using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    //Este script deve estar contido no objeto que contem o Animator (ex no HumanoidArms dentro do prefab do player)
    [Header("Camera Shake")]
    public CameraShake shake;
    [Header("Spear")]
    public GameObject spearModel;
    [Header("Player")]
    public Player player;

    public void PlayShake(int amount)
    {
        shake.PlayShake(amount);
    }

    public void ThrowSpear()
    {
        
    }

    public void EndLeftArm()
    {
        player.SetLeftArmWeight(0);
    }

    public void EndRightArm()
    {
        player.SetRightArmWeight(0);
    }

    public void CanAim(int canAim)
    {
        switch (canAim)
        {
            case 0: player.CanAim = false; break;
            case 1: player.CanAim = true; break;
            default: player.CanAim = false; break;

        }
    }
}
