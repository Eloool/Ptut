using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Stats
{
    public GameObject ObjectInHand;
    private Quaternion QuaternionObject;
    public GameObject PointHand;
    
    public static Hand instance;

    public PlayerMovement player;
    public BasicBehaviour behaviourManager;

    private void Start()
    {
        behaviourManager = GetComponent<BasicBehaviour>();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    private void Update()
    {
        if (ObjectInHand != null)
        {
            ObjectInHand.transform.position = PointHand.transform.position;
            ObjectInHand.transform.rotation = PointHand.transform.rotation * Quaternion.Euler(-90, -30, 180) * Quaternion.Euler(QuaternionObject.x *180,QuaternionObject.y * 180,QuaternionObject.z * 180);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(HitAnimation());

            if (ObjectInHand != null)
            {
                GetComponent<BreakObjectsHand>().BreakObjects(ObjectInHand.GetComponent<Item3d>().IconItem.GetComponent<Item>());
                GetComponent<InteractionKillEnnemi>().HitEnnemis(ObjectInHand.GetComponent<Item3d>().IconItem.GetComponent<Item>());
            }
            else
            {
                GetComponent<BreakObjectsHand>().BreakObjects(null);
                GetComponent<InteractionKillEnnemi>().HitEnnemis(null);
            }
        }
    }
    
    private IEnumerator HitAnimation()
    {
        behaviourManager.GetAnim.SetBool("Hit", true);
        yield return new WaitForSeconds(0.1f);
        behaviourManager.GetAnim.SetBool("Hit", false);
    }

    public void ChangeObject(GameObject obj)
    {
        if (ObjectInHand != null)
        {
            ObjectInHand.transform.rotation = QuaternionObject;
        }
        if (obj != null)
        {
            QuaternionObject = obj.transform.rotation;
            ObjectInHand = obj;
        }
        else
        {
            QuaternionObject = Quaternion.identity;
            ObjectInHand = null;
        }
    }
}
