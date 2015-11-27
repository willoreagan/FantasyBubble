using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    public GameObject IdleState;
    public GameObject HurtState;
    public GameObject DizzyState;
    public static PlayerState instance;
	// Use this for initialization
	void Start ()
    {
        instance = this;
        setIdle();
    }
	
    public void setIdle()
    {
        IdleState.SetActive(true);
        HurtState.SetActive(false);
        DizzyState.SetActive(false);
    }
    public void setHurt()
    {
        IdleState.SetActive(false);
        HurtState.SetActive(true);
        DizzyState.SetActive(false);
    }
    public void setDizzy()
    {
        IdleState.SetActive(false);
        HurtState.SetActive(false);
        DizzyState.SetActive(true);
    }
}
