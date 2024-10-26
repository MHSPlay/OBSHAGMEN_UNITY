using UnityEngine;

public class Settings : MonoBehaviour {

#region sensitivity
    static float defaultSensitivity = 1f;
    float mouseSensitivity = defaultSensitivity;

    public void SetMouseSensitivity( float sensitivity ) => mouseSensitivity = sensitivity;
    public float GetMouseSensitivity( ) => mouseSensitivity;
#endregion



}
