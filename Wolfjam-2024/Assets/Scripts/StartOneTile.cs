using UnityEngine;

public class StartOneTile : MonoBehaviour
{
    [SerializeField] private GateComponent gateComponent;

    public GateComponent GateComponent { get { return gateComponent; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gateComponent = GetComponentsInChildren<GateComponent>()[0];
        //gateComponent.LockComponent();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LockComponent()
    {
        gateComponent.LockComponent();
    }
}
