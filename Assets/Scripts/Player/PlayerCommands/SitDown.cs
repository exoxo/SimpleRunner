using UnityEngine;
using System.Collections;



class SitDown : BaseCommand
{
    public override void Execute(Transform tr)
    {
        tr.Translate(new Vector3(0, -0.7f, 0));
    }
}

