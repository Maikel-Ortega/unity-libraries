using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestCircleCharacter : MonoBehaviour 
{
    List<Vector3> storedPoints;
    public float speed = 10f;

    void OnEnable()
    {
        storedPoints = new List<Vector3>();

    }

    void Awake()
    {
    }

	void Update () 
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StorePoint();
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 i = new Vector2(xInput, yInput);

        this.transform.LookAt(this.transform.position+new Vector3(xInput,0,yInput));           

        this.transform.Translate(transform.forward* speed * i.magnitude*Time.deltaTime,Space.World);
	}

    void StorePoint()
    {
        storedPoints.Add(new Vector3(transform.position.x,0, transform.position.z));
    }

    void OnDrawGizmos()
    {
        if(storedPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (var item in storedPoints)
            {
                Gizmos.DrawSphere(item,0.2f);
            }
        }
    }
}
