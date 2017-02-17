using UnityEngine;
using System.Collections;


class Jump : BaseCommand
{
    public override void Execute(Transform tr, Vector3 srcTr, Vector3 dstTr)
    {
        tr.Translate(new Vector3(0, 0.7f, 0));
        MoveUp(tr, srcTr, dstTr);
        MoveDown(tr, dstTr, srcTr);

    }

    public void MoveUp(Transform tr, Vector3 srcTr, Vector3 dstTr)
    {
        while (srcTr != dstTr)
            tr.position = Vector3.Lerp(srcTr, dstTr, 2 * Time.deltaTime);
    }

    public void MoveDown(Transform tr, Vector3 srcTr, Vector3 dstTr)
    {
        while (srcTr != dstTr)
            tr.position = Vector3.Lerp(srcTr, dstTr, 2 * Time.deltaTime);
    }
}

