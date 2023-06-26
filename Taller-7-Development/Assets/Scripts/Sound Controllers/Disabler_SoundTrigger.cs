using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler_SoundTrigger : MonoBehaviour
{
    SoundTrigger trigger;

    private void Awake()
    {
        trigger = GetComponent<SoundTrigger>();
    }


}
