using UnityEngine;
using System.Collections;


class TouchInput : BaseInputController
{
    Vector2 deltaSum;
    Vector2 touchVectorOrigin = -1 * Vector2.one;
    Touch myTouch;

    BaseCommand baseCommand;

    public void CheckInput()
    {
        horz = 0;
        vert = 0;
        if (Input.touchCount > 0)
        {
            myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchVectorOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchVectorOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchVectorOrigin.x;
                float y = touchEnd.y - touchVectorOrigin.y;
                touchVectorOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horz = x > 0 ? 1.5f : -1.5f;
                else
                    vert = y > 0 ? 1.5f : -1.5f;
            }
        }
    }

    public void LateUpdate()
    {
        CheckInput();
    }
}

