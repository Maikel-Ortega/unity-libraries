using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RDSTestsMono : MonoBehaviour 
{
    public RDSScriptableTable tableToTest;
	
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckTable();
        }
	}

    void CheckTable()
    {
        Debug.Log("CHECKING RESULTS IN " + tableToTest.id);
        List<RDSScriptableProperty> results = tableToTest.GetRdsResult();

        foreach (var item in results)
        {
            Debug.Log("RESULT: "+item.rdsObject.id);
        }
    }
}
