using UnityEngine;
using System.Collections;

public class Keyboard_Input : BaseInputController
{
    public override void CheckInput()
    {
        horz = vert = 0;
        if (Input.GetKeyDown(KeyCode.A))
            horz = -1.5f;
        if (Input.GetKeyDown(KeyCode.D))
            horz = 1.5f;
        if (Input.GetKeyDown(KeyCode.W))
            vert = 1.5f;
        if (Input.GetKeyDown(KeyCode.S))
            vert = -1.5f;

        Up = (vert > 0);
        Down = (vert < 0);
        Right = (horz > 0);
        Left = (horz < 0);

        Fire1 = Input.GetButton("Fire1");
        shouldRespawn = Input.GetButton("Fire3");
    }

    public void LateUpdate()
    {
        CheckInput();
    }
}

