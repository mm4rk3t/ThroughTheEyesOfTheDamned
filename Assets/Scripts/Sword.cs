using UnityEngine;

public class Sword : Weapon
{
    private void Start()
    {
        GetComponents();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))//LEFT CLICK
        {
            StartCoroutine(SwordRoutine());
        }
        if (Input.GetMouseButtonDown(1) && _hasParry == true)//RIGHT CLICK
        {
            StartCoroutine(Parry(_parryWindow));
        }
    }


    

}

