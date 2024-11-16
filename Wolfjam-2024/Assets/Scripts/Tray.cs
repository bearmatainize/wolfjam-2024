using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{

    [SerializeField] GateComponent gateComponentPrefab;

    [SerializeField] List<GateComponent> gateComponents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGateComponents(string[] types)
    {
        for (int i = 0; i < types.Length; i++)
        {
            switch (types[i])
            {
                case "or":
                    var newGateComponent = Instantiate(gateComponentPrefab);
                    newGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    newGateComponent.SetOriginalPosition();
                    Debug.Log(newGateComponent.transform.position);
                    break;
            }
        }
    }
}
