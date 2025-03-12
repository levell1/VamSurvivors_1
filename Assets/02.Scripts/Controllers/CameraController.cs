
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null) 
        {
            return;
        }

        //TODO
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
    }
}
