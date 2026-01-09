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
            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass contextCompat = new AndroidJavaClass("androidx.core.content.ContextCompat");
            AndroidJavaClass activityCompat = new AndroidJavaClass("androidx.core.app.ActivityCompat");
            
            // Check Android version
            AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION");
            int sdkInt = versionClass.GetStatic<int>("SDK_INT");
            
            if (sdkInt >= 31) // Android 12+
            {
                string[] permissions = {
                    "android.permission.BLUETOOTH_SCAN",
                    "android.permission.BLUETOOTH_CONNECT",
                    "android.permission.BLUETOOTH_ADVERTISE"
                };
                
                foreach (string permission in permissions)
                {
                    int permissionCheck = contextCompat.CallStatic<int>("checkSelfPermission", currentActivity, permission);
                    if (permissionCheck != 0) // PackageManager.PERMISSION_GRANTED = 0
                    {
                        activityCompat.CallStatic("requestPermissions", currentActivity, new string[] { permission }, 1);
                    }
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
