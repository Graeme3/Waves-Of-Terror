using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region Variables

    public GameObject Player;
    public Transform targer;
    public float smoothing = 5f;

    private Vector3 _offset;

    #endregion

    // Use this for initialization
    void Start()
    {
        //_offset = Player.transform.position - transform.position;
        _offset = transform.position - targer.position;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    transform.position = Player.transform.position - _offset;
    //}

    void FixedUpdate()
    {
        Vector3 targetCamPos = targer.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
