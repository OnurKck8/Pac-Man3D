using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public NavMeshAgent _follow;
    public Transform _enemySpawnPlace;
    public GameObject _targetCube;
    public GameObject _informationPanel;
    public MeshRenderer _transparent;

    string _color = "Red";

    private void OnTriggerEnter(Collider other)
    {
        switch(_transparent.enabled)
        {
            case true:

                if(other.gameObject.tag=="Player" && _color=="Red")
                {
                    SceneManager.LoadScene("SampleScene");
                }

                if (other.gameObject.tag == "Player" && _color == "Blue")
                {
                    GameObject _newInformationPanel = Instantiate(_informationPanel, transform.position, Quaternion.identity);
                    Destroy(_newInformationPanel, 2.0f);

                    BeRed();

                    _targetCube.transform.position = _enemySpawnPlace.position;

                    CancelInvoke("BeRed");
                    CancelInvoke("StartAnimation");
                    _transparent.enabled = false;
                }

                if(other.gameObject==_targetCube)
                {
                    other.gameObject.GetComponent<TargetCube>().MoveTargetCube();
                }
                break;

            case false:

                if (other.gameObject == _targetCube)
                {
                    other.gameObject.GetComponent<TargetCube>().MoveTargetCube();

                    _transparent.enabled = true;
                }
                break;
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _follow.destination = _targetCube.transform.position;
    }

    public void BeBlue()
    {
        _color = "Blue";


    }

    void StartAnimation()
    {

    }

    void BeRed()
    {
        _color = "Red";
    }
}
