using TMPro;
using UnityEngine;

public partial class MainUI
{
    [Header("Audio")]
    [SerializeField] private TextMeshProUGUI masterVolume;
    [SerializeField] private TextMeshProUGUI musicVolume;
    [SerializeField] private TextMeshProUGUI gameSoundsVolume;
    [SerializeField] private TextMeshProUGUI voiceVolume;

    /// <summary>
    /// Handles the Master Volume Value
    /// </summary>
    /// <param name="newValue"></param>
    public void MasterVolumeHandle(float newValue)
    {
        masterVolume.text = newValue.ToString("F1").Replace(",", ".");
        gameOptions.masterVolume = newValue;
        gameOptionsScriptableObject.masterVolume = newValue;
        SaveOptions();
    }
    
    /// <summary>
    /// Handles the Music Volume Value
    /// </summary>
    /// <param name="newValue"></param>
    public void MusicVolumeHandle(float newValue)
    {
        musicVolume.text = newValue.ToString("F1").Replace(",", ".");
        gameOptions.musicVolume = newValue;
        gameOptionsScriptableObject.musicVolume = newValue;
        SaveOptions();
    }
    
    /// <summary>
    /// Handles the Game SFX Volume Value
    /// </summary>
    /// <param name="newValue"></param>
    public void GameSoundsVolumeHandle(float newValue)
    {
        gameSoundsVolume.text = newValue.ToString("F1").Replace(",", ".");
        gameOptions.gameSoundsVolume = newValue;
        gameOptionsScriptableObject.gameSoundsVolume = newValue;
        SaveOptions();
    }
    
    /// <summary>
    /// Handles the Voice Volume Value
    /// </summary>
    /// <param name="newValue"></param>
    public void VoiceVolumeHandle(float newValue)
    {
        voiceVolume.text = newValue.ToString("F1").Replace(",", ".");
        gameOptions.voiceVolume = newValue;
        gameOptionsScriptableObject.voiceVolume = newValue;
        SaveOptions();
    }
}