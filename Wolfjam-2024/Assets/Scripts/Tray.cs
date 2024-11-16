using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{

    //[SerializeField] GateComponent gateComponentPrefab;

    [SerializeField] List<GateComponent> gateComponents;

    [SerializeField] private NotGate notGatePrefab;
    [SerializeField] private OrGate orGatePrefab;
    [SerializeField] private NorGate norGatePrefab;
    [SerializeField] private AndGate andGatePrefab;
    [SerializeField] private NandGate nandGatePrefab;
    [SerializeField] private XorGate xorGatePrefab;
    [SerializeField] private XnorGate xnorGatePrefab;


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
                    var newGateComponent = Instantiate(orGatePrefab);
                    newGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    newGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
            }
        }
    }
}
