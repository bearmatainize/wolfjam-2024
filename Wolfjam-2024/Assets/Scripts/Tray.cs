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
                case "not":
                    var notGateComponent = Instantiate(notGatePrefab);
                    notGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    notGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    notGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
                case "or":
                    var orGateComponent = Instantiate(orGatePrefab);
                    orGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    orGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    orGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
                case "nor":
                    var norGateComponent = Instantiate(norGatePrefab);
                    norGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    norGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    norGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
                case "and":
                    var andGateComponent = Instantiate(andGatePrefab);
                    andGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    andGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    andGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
                case "nand":
                    var nandGateComponent = Instantiate(nandGatePrefab);
                    nandGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    nandGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    nandGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
                case "xor":
                    var xorGateComponent = Instantiate(xorGatePrefab);
                    xorGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    xorGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    xorGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
                case "default":
                case "xnor":
                    var xnorGateComponent = Instantiate(xnorGatePrefab);
                    xnorGateComponent.transform.position = new Vector3(transform.position.x - 3.0f + i, transform.position.y, -1.0f);
                    xnorGateComponent.GetComponentsInChildren<GateComponent>()[0].transform.localPosition = new Vector3(0.0f, 0.0f, -2.0f);
                    xnorGateComponent.GetComponentsInChildren<GateComponent>()[0].SetOriginalPosition();
                    break;
            }
        }
    }
}
