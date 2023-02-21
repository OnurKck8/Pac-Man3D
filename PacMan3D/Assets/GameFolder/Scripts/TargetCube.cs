using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            MoveTargetCube();
        }
    }

    public void MoveTargetCube()
    {
        float z = Random.Range(1f, 4f);
        float x = Random.Range(-4f, 4f);

        transform.position = new Vector3(x, 0, z);
    }
}
