using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class buttonScript : MonoBehaviour
{
    public int button;
    
    public void OnClick()
    {
        switch (button)
        {
            case 0:
                {
                    Destroy(transform.parent.gameObject);
                    break;
                }
        }
    }
}
