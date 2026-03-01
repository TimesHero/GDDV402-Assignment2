using System.Collections.Generic;

using UnityEngine;
using Unity.Profiling;

public class ProfilerExample : MonoBehaviour
{
    static readonly ProfilerMarker performanceMarker = new("ProfilerExample.Prepare");

    public GameObject prefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddMarker();
    }

    public void AddMarker()
    {
        using (performanceMarker.Auto())
        {
            IntenseLogic();
        }
    }

    private void IntenseLogic()
    {
        List<GameObject> gameObjects = new();
        for (int i = 0; i < 42069; i++)
        {
            GameObject go = Instantiate(prefab);

            go.transform.position = Random.insideUnitSphere * 100;

            gameObjects.Add(go);
            
        }
    }
}