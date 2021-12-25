using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBar : MonoBehaviour
{
    [SerializeField]private GameObject endPoint;
    [SerializeField]private GameObject startPoint;
    public GameObject GetEndPoint(){
        return endPoint;
    }
    
    public GameObject GetStartPoint(){
        return startPoint;
    }
}
