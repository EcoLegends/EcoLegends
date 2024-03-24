
using UnityEngine;

public class buttonScriptTutorial : MonoBehaviour
{
    public int button;
    public int direzione;
    
    public void OnClick()
    {
        if (direzione == -999)
        {
            Destroy(gameObject.transform.parent.parent.gameObject);
            return;
        }
        Debug.Log("Tutorial next page: "+(button + direzione));
        GameObject nuovo = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial" + (button + direzione)), gameObject.transform.parent.parent.parent);

        Destroy(gameObject.transform.parent.parent.gameObject);
    }
}
