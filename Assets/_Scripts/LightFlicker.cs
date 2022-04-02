using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private Light light;

    [SerializeField]
    public float minIntensity = 0.1f;
    [SerializeField]
    public float maxIntensity = 1f;
    [SerializeField]
    public int smoothing = 100;

    Queue<float> smoothQueue; 
    float lastSum = 0;


    public Renderer renderer;

    private MaterialPropertyBlock propBlock;

    public bool on
    {
        get
        {
            return light.gameObject.activeInHierarchy;
        }
        set
        {
            light.gameObject.SetActive(value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        propBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        light.intensity = lastSum / (float)smoothQueue.Count;

        renderer.GetPropertyBlock(propBlock); 
        // Assign our new value.
        propBlock.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.black, light.intensity));
        // Apply the edited values to the renderer.
        renderer.SetPropertyBlock(propBlock);
    }

}