using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class BluetoothPermissionManager : MonoBehaviour
{
    private bool permissionsGranted = false;
    
    void Start()
    {
        RequestBluetoothPermissions();
    }
    
    public void RequestBluetoothPermissions()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        
        // For Android 12+ (API 31+), request new Bluetooth permissions
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check Android version
            AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION");
            int sdkInt = versionClass.GetStatic<int>("SDK_INT");
            
            if (sdkInt >= 31) // Android 12+
            {
                // Use Unity's Permission system for Android 12+ Bluetooth permissions
                string bluetoothScan = "android.permission.BLUETOOTH_SCAN";
                string bluetoothConnect = "android.permission.BLUETOOTH_CONNECT";
                string bluetoothAdvertise = "android.permission.BLUETOOTH_ADVERTISE";
                
                if (!Permission.HasUserAuthorizedPermission(bluetoothScan))
                {
                    Permission.RequestUserPermission(bluetoothScan);
                }
                
                if (!Permission.HasUserAuthorizedPermission(bluetoothConnect))
                {
                    Permission.RequestUserPermission(bluetoothConnect);
                }
                
                if (!Permission.HasUserAuthorizedPermission(bluetoothAdvertise))
                {
                    Permission.RequestUserPermission(bluetoothAdvertise);
                }
            }
        }
        
        StartCoroutine(CheckPermissions());
#else
        permissionsGranted = true;
#endif
    }
    
    IEnumerator CheckPermissions()
    {
        yield return new WaitForSeconds(1f);
        
#if UNITY_ANDROID && !UNITY_EDITOR
        permissionsGranted = Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) ||
                           Permission.HasUserAuthorizedPermission(Permission.FineLocation);
#endif
        
        if (permissionsGranted)
        {
            Debug.Log("Bluetooth permissions granted. Ready to scan for devices.");
        }
        else
        {
            Debug.LogWarning("Bluetooth permissions not granted. Please enable location permissions in device settings.");
        }
    }
    
    public bool ArePermissionsGranted()
    {
        return permissionsGranted;
    }
}
