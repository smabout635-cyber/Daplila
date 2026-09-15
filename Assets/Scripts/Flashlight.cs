using UnityEngine;

/// <summary>
/// Flashlight.cs - Player flashlight controller
/// Like in Granny game - follow the camera and can be toggled on/off
/// Attach this script to a Light object under Main Camera
/// </summary>
public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlightLight;
    [SerializeField] private float batteryLife = 100f;
    [SerializeField] private float drainRate = 5f; // Battery drains per second when on
    [SerializeField] private float rechargeRate = 2f; // Battery recharges per second when off
    
    private float currentBattery;
    private bool isFlashlightOn = true;
    private bool isBatteryDead = false;

    void Start()
    {
        currentBattery = batteryLife;
        
        // Find the light component
        flashlightLight = GetComponent<Light>();
        
        if (flashlightLight == null)
        {
            Debug.LogError("❌ No Light component found! Add a Light to this object!");
            return;
        }
        
        // Start with flashlight ON
        flashlightLight.enabled = true;
        Debug.Log("🔦 Flashlight initialized! Battery: " + currentBattery + "%");
    }

    void Update()
    {
        // Toggle flashlight with F key
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

        // Drain battery when flashlight is on
        if (isFlashlightOn && currentBattery > 0)
        {
            currentBattery -= drainRate * Time.deltaTime;
            
            if (currentBattery <= 0)
            {
                currentBattery = 0;
                isBatteryDead = true;
                TurnOffFlashlight();
                Debug.Log("⚠️ BATTERY DEAD! Flashlight turned off!");
            }
        }

        // Recharge battery when flashlight is OFF
        if (!isFlashlightOn && currentBattery < batteryLife && !isBatteryDead)
        {
            currentBattery += rechargeRate * Time.deltaTime;
            
            if (currentBattery > batteryLife)
            {
                currentBattery = batteryLife;
            }
        }

        // Allow turn on if battery has some charge
        if (isBatteryDead && currentBattery > 10f)
        {
            isBatteryDead = false;
            Debug.Log("✅ Battery recharged enough to use!");
        }

        // Display battery percentage in console (optional)
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("🔋 Battery: " + GetBatteryPercentage().ToString("F1") + "%");
        }
    }

    /// <summary>
    /// Toggle flashlight on/off
    /// </summary>
    public void ToggleFlashlight()
    {
        if (isFlashlightOn)
        {
            TurnOffFlashlight();
        }
        else
        {
            TurnOnFlashlight();
        }
    }

    /// <summary>
    /// Turn flashlight on
    /// </summary>
    public void TurnOnFlashlight()
    {
        if (currentBattery > 0 && !isBatteryDead)
        {
            isFlashlightOn = true;
            flashlightLight.enabled = true;
            Debug.Log("🔦 Flashlight ON - Battery: " + GetBatteryPercentage().ToString("F1") + "%");
        }
        else if (isBatteryDead)
        {
            Debug.Log("⚠️ Battery is DEAD! Turn it off to recharge!");
        }
        else
        {
            Debug.Log("⚠️ No battery!");
        }
    }

    /// <summary>
    /// Turn flashlight off
    /// </summary>
    public void TurnOffFlashlight()
    {
        isFlashlightOn = false;
        flashlightLight.enabled = false;
        Debug.Log("💡 Flashlight OFF - Recharging...");
    }

    /// <summary>
    /// Get current battery percentage (0-100)
    /// </summary>
    public float GetBatteryPercentage()
    {
        return (currentBattery / batteryLife) * 100f;
    }

    /// <summary>
    /// Check if flashlight is on
    /// </summary>
    public bool IsFlashlightOn()
    {
        return isFlashlightOn;
    }

    /// <summary>
    /// Check if battery is dead
    /// </summary>
    public bool IsBatteryDead()
    {
        return isBatteryDead;
    }

    /// <summary>
    /// Recharge battery (full)
    /// </summary>
    public void RechargeBattery()
    {
        currentBattery = batteryLife;
        isBatteryDead = false;
        Debug.Log("✅ Battery fully recharged!");
    }

    /// <summary>
    /// Add battery amount
    /// </summary>
    public void AddBattery(float amount)
    {
        currentBattery += amount;
        if (currentBattery > batteryLife)
            currentBattery = batteryLife;
        
        if (isBatteryDead && currentBattery > 10f)
            isBatteryDead = false;
            
        Debug.Log("🔋 Battery +" + amount + "% = " + GetBatteryPercentage().ToString("F1") + "%");
    }

    /// <summary>
    /// Get current battery amount
    /// </summary>
    public float GetBatteryAmount()
    {
        return currentBattery;
    }

    /// <summary>
    /// Set light intensity (brightness)
    /// </summary>
    public void SetIntensity(float intensity)
    {
        if (flashlightLight != null)
            flashlightLight.intensity = intensity;
    }

    /// <summary>
    /// Set light range
    /// </summary>
    public void SetRange(float range)
    {
        if (flashlightLight != null)
            flashlightLight.range = range;
    }
}
