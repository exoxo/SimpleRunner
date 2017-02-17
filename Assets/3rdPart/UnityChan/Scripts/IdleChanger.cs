using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]



public class IdleChanger : MonoBehaviour
{
	
	private Animator anim;						
	private AnimatorStateInfo currentState;		
	private AnimatorStateInfo previousState;	
	public bool _random = true;				
	public float _threshold = 0.5f;				
	public float _interval = 3.5f;				

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		currentState = anim.GetCurrentAnimatorStateInfo (0);
		previousState = currentState;

		StartCoroutine ("RandomChange");
	}
	

	IEnumerator RandomChange ()
	{
        while (true)
        {
            if (_random)
            {
                int randVal = (int)Random.Range(0f, 11f);
                anim.Play(randVal.ToString());
                yield return new WaitForSeconds(_interval);
            }
        }
	}
}
