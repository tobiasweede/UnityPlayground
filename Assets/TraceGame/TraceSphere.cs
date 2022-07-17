using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceSphere : MonoBehaviour
{
    public bool ToggleMove;
    private int m_Id;
    public int Id
    {
        get { return m_Id; }
        set { m_Id = value; }
    }
    private Vector3 currentVelocity;
    private float timeToVelocityChange;
    private Vector3 areaSize;
    public void SetAreaSize(Vector3 size)
    {
        areaSize = size;
    }

    void Start()
    {
        if (areaSize == null)
        { // Only needed if not spawned by script
            MeshRenderer r = GetComponentInParent<MeshRenderer>();
            areaSize = r.bounds.size;
        }

        PickNewVelocity();
        PickNewVelocityChangeTime();
        ToggleMove = true;
    }

    void Update()
    {
        if (ToggleMove)
        {
            HandleTime();
            BoundaryCheck();
            Move();
        }
    }
    private void BoundaryCheck()
    {
        if ((transform.localPosition.x > areaSize.x && currentVelocity.x > 0) || (transform.localPosition.x < -areaSize.x && currentVelocity.x < 0))
        {
            currentVelocity.x *= -1;
        }
        else if ((transform.localPosition.y > areaSize.y && currentVelocity.y > 0) || (transform.localPosition.y < -areaSize.y && currentVelocity.y < 0))
        {
            currentVelocity.y *= -1;
        }
        if ((transform.localPosition.z > areaSize.z && currentVelocity.z > 0) || (transform.localPosition.z < -areaSize.z && currentVelocity.z < 0))
        {
            currentVelocity.z *= -1;
        }
    }

    private void HandleTime()
    {
        timeToVelocityChange -= Time.deltaTime;
        if (timeToVelocityChange < 0)
        {
            PickNewVelocity();
            PickNewVelocityChangeTime();
        }
    }
    private void PickNewVelocityChangeTime()
    {
        timeToVelocityChange = Random.Range(2.0f, 5.0f);
    }
    private void PickNewVelocity()
    {
        currentVelocity = Random.insideUnitSphere;
        currentVelocity *= 3.0f;
    }
    private void Move()
    {
        transform.position = transform.position + currentVelocity * Time.deltaTime;
    }
}
