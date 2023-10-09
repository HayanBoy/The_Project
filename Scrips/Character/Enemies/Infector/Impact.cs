using UnityEngine;

public class Impact : MonoBehaviour
{
    public int Roll = 0;
    public float StopTime;
    public float StopRegdoll;
    private float Times = 0;
    public int layers;

    void Update()
    {
        if(Roll == 1)
        {
            this.gameObject.layer = layers;

            Times += Time.deltaTime;
            if (Times > StopTime)
            {
                GetComponent<BoxCollider2D>().enabled = true;
            }
            if (Times > StopRegdoll)
            {
                HingeJoint2D joint = GetComponent<HingeJoint2D>();
                joint.autoConfigureConnectedAnchor = false;
            }
        }

        else if (Roll == 2)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            HingeJoint2D joint = GetComponent<HingeJoint2D>();
            joint.autoConfigureConnectedAnchor = true;
            Times = 0;
            Invoke("Reverse", 3);
        }
    }

    void Reverse()
    {
        Roll = 0;
    }
}
