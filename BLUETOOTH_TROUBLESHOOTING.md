# BrainLink Pro SDK - Bluetooth Troubleshooting Guide

## Common Bluetooth Connection Issues and Solutions

### 1. **Permissions Not Granted**

#### Android Issues:
- **Problem**: App cannot scan for or connect to Bluetooth devices
- **Cause**: Missing Bluetooth and location permissions
- **Solution**: 
  - Ensure `AndroidManifest.xml` is properly configured (now included)
  - Grant location permissions when prompted
  - For Android 12+, ensure new Bluetooth permissions are granted

#### iOS Issues:
- **Problem**: App crashes or cannot access Bluetooth
- **Cause**: Missing Bluetooth usage descriptions
- **Solution**: 
  - Ensure `Info.plist` is properly configured (now included)
  - Grant Bluetooth permissions when prompted

### 2. **Device Not Found During Scan**

#### Troubleshooting Steps:
1. **Check Device Compatibility**:
   - Ensure your BrainLink device is in the whitelist: `BrainLink_Pro`, `BrainLink_Lite`, `BrainLink`, `BrainLink_Lite_P`, `Brainlink_Lite`, `ROYWOS`, `BrainLink_Pink`

2. **Device Preparation**:
   - Turn on your BrainLink device
   - Ensure device is in pairing mode
   - Make sure device is not connected to another app/device

3. **Phone Settings**:
   - Enable Bluetooth on your phone
   - Enable Location Services (required for BLE scanning)
   - Disable battery optimization for the app

### 3. **Connection Fails After Device Found**

#### Common Causes:
- Device already connected to another app
- Bluetooth interference
- Low battery on BrainLink device
- Distance too far from device

#### Solutions:
1. **Reset Bluetooth Connection**:
   - Turn Bluetooth off and on
   - Restart the app
   - Restart your phone if necessary

2. **Check Device Status**:
   - Ensure BrainLink device has sufficient battery
   - Move closer to the device (within 10 meters)
   - Remove interference sources

### 4. **Platform-Specific Issues**

#### Android Specific:
- **API Level 29+**: Requires location permissions for BLE scanning
- **API Level 31+**: Requires new Bluetooth permissions (`BLUETOOTH_SCAN`, `BLUETOOTH_CONNECT`)
- **Manufacturer Restrictions**: Some manufacturers have additional Bluetooth restrictions

#### iOS Specific:
- **iOS 13+**: Requires Bluetooth usage descriptions
- **Background Scanning**: Limited in background mode
- **Privacy Settings**: Check iOS Privacy settings for Bluetooth access

### 5. **Debug Steps**

#### Enable Debug Logging:
1. Check Unity Console for error messages
2. Look for these specific log messages:
   - `"unity=== ClickScan"` - Scan initiated
   - `"unity===nameAddressRiss"` - Device found
   - `"unity===connectDevice"` - Connection attempt
   - `"ReceiveContentState"` - Connection status

#### Common Error Messages:
- `"Bluetooth permissions not granted"` - Request permissions
- `"PoorSignal = 200"` - Device not connected
- `"bAndroidHeadsetConnected = false"` - Connection failed

### 6. **Build Settings Verification**

#### Android Build Settings:
- Minimum API Level: 29 (Android 10)
- Target API Level: 33+ (recommended)
- IL2CPP Scripting Backend (recommended)

#### iOS Build Settings:
- Minimum iOS Version: 11.0+
- Deployment Target: 11.0+

### 7. **Testing Procedure**

#### Step-by-Step Testing:
1. **Build and Install**: Deploy app to physical device (not emulator)
2. **Grant Permissions**: Allow all requested permissions
3. **Enable Bluetooth**: Ensure Bluetooth is on
4. **Enable Location**: Turn on location services
5. **Prepare Device**: Turn on BrainLink device
6. **Start Scan**: Tap scan button in app
7. **Wait for Discovery**: Allow 10-30 seconds for device discovery
8. **Connect**: Tap on discovered device to connect

### 8. **Advanced Troubleshooting**

#### If Still Not Working:
1. **Check Native Plugins**:
   - Verify Android AAR file is properly imported
   - Verify iOS static library is properly linked

2. **Unity Project Settings**:
   - Check Player Settings for correct package name
   - Verify scripting backend settings
   - Check target architecture settings

3. **Device-Specific Issues**:
   - Test on different Android/iOS devices
   - Check manufacturer-specific Bluetooth limitations
   - Verify device OS version compatibility

### 9. **Support Information**

#### When Seeking Help:
Include the following information:
- Unity version
- Target platform (Android/iOS)
- Device model and OS version
- BrainLink device model
- Error messages from Unity Console
- Steps to reproduce the issue

#### Contact Information:
- GitHub: https://github.com/Macrotellect/BrainLinkProSDK_Unity
- Check documentation files for additional support contacts
