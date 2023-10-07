using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private class Resource
    {
        public int Amount;
        public ResourceMeter Meter;

        public Resource(int amount, ResourceMeter meter)
        {
            Amount = amount;
            Meter = meter;
        }
     }

    private Dictionary<string, Resource> resources;

    public int Oxygen 
    {
        get => resources["oxygen"].Amount;
        set 
        {
            int max = resources["oxygen"].Meter.MaxAmount;
            if (value <= max)
                resources["oxygen"].Amount = value;
            else
                Debug.LogWarning($"Trying to set value of Oxygen to {value}, which is greater than its max of {max}");
        }
    }
    public int Temperature
    {
        get => resources["temperature"].Amount;
        set 
        {
            int max = resources["temperature"].Meter.MaxAmount;
            if (value <= resources["temperature"].Meter.MaxAmount)
                resources["temperature"].Amount = value;
            else
                Debug.LogWarning($"Trying to set value of Temperature to {value}, which is greater than its max of {max}");
        }
    }

    [SerializeField] private ResourceMeter oxygenMeter;
    [SerializeField] private ResourceMeter temperatureMeter;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Pipeline is {UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset.GetType().Name}");

        resources = new Dictionary<string, Resource>
        {
            ["oxygen"] = new Resource(0, oxygenMeter),
            ["temperature"] = new Resource(-290, temperatureMeter)
        };

        // init oxygen min/max
        resources["oxygen"].Meter.MinAmount = 0;
        resources["oxygen"].Meter.MaxAmount = 100;

        // init temp min/max
        resources["temperature"].Meter.MinAmount = -290; // todo: check val, can we have negatives?
        resources["temperature"].Meter.MaxAmount = 100; // todo: check val
    }

    // Update is called once per frame
    void Update()
    {
        DebugUI();
        UIUpdate();
    }

    private void UIUpdate()
    {
        foreach (KeyValuePair<string, Resource> resourceEntry in resources)
        {
            int amount = resourceEntry.Value.Amount;
            ResourceMeter meter = resourceEntry.Value.Meter;

            meter.Amount = amount;
        }
    }

    private void DebugUI()
    {
        int diff = 10;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            diff = -10;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Oxygen += Oxygen + diff >= 0 ? diff : 0;
            Debug.Log($"Oxygen set to {Oxygen}");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Temperature += diff;
            Debug.Log($"Temperature set to {Temperature}");
        }
    }
}
