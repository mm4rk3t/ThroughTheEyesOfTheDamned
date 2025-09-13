using UnityEngine;

public class ProyectileDebug : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(Bullet,new Vector3(0,0,0),Quaternion.identity);
        }
    }
}
