using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityController : MonoBehaviour
{
    public ElectricityFlow[] electricitywires;
    public void UpdateElectricityStatus(bool Status){
        for(int i=0;i<electricitywires.Length;i++){
            electricitywires[i].isElectricityFlowstop=!Status;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
