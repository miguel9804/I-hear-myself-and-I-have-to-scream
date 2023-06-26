using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptChaleco : MonoBehaviour
{
    public comm Chaleco;

    private void OnTriggerEnter(Collider collision)
    {
        //--------------PECHO-----------------
        if (collision.gameObject.name == "PechoDere")
        {
            Chaleco.pechodere = 0x01;
        }
        if (collision.gameObject.name == "PechoDereB")
        {
            Chaleco.pechodereB = 0x01;
        }

        if (collision.gameObject.name == "PechoIzq")
        {
            Chaleco.pechoizq = 0x01;
        }
        if (collision.gameObject.name == "PechoIzqB")
        {
            Chaleco.pechoizqB = 0x01;
        }
        //--------------HOMBROS-----------------
        if (collision.gameObject.name == "HombroDere")
        {
            Chaleco.hombrodere = 0x01;
        }
        if (collision.gameObject.name == "HombroDereB")
        {
            Chaleco.hombrodereB = 0x01;
        }

        if (collision.gameObject.name == "HombroIzq")
        {
            Chaleco.hombroizq = 0x01;
        }
        if (collision.gameObject.name == "HombroIzqB")
        {
            Chaleco.hombroizqB = 0x01;
        }
        //--------------TORSO-----------------
        if (collision.gameObject.name == "TorsoDere")
        {
            Chaleco.tordere = 0x01;
        }
        if (collision.gameObject.name == "TorsoDereB")
        {
            Chaleco.tordereB = 0x01;
        }

        if (collision.gameObject.name == "TorsoIzq")
        {
            Chaleco.torizq = 0x01;
        }
        if (collision.gameObject.name == "TorsoIzqB")
        {
            Chaleco.torizqB = 0x01;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //--------------PECHO-----------------
        if (collision.gameObject.name == "PechoDere")
        {
            Chaleco.pechodere = 0x00;
        }
        if (collision.gameObject.name == "PechoDereB")
        {
            Chaleco.pechodereB = 0x00;
        }

        if (collision.gameObject.name == "PechoIzq")
        {
            Chaleco.pechoizq = 0x00;
        }
        if (collision.gameObject.name == "PechoIzqB")
        {
            Chaleco.pechoizqB = 0x00;
        }
        //--------------HOMBROS-----------------
        if (collision.gameObject.name == "HombroDere")
        {
            Chaleco.hombrodere = 0x00;
        }
        if (collision.gameObject.name == "HombroDereB")
        {
            Chaleco.hombrodereB = 0x00;
        }

        if (collision.gameObject.name == "HombroIzq")
        {
            Chaleco.hombroizq = 0x00;
        }
        if (collision.gameObject.name == "HombroIzqB")
        {
            Chaleco.hombroizqB = 0x00;
        }
        //--------------TORSO-----------------
        if (collision.gameObject.name == "TorsoDere")
        {
            Chaleco.tordere = 0x00;
        }
        if (collision.gameObject.name == "TorsoDereB")
        {
            Chaleco.tordereB = 0x00;
        }

        if (collision.gameObject.name == "TorsoIzq")
        {
            Chaleco.torizq = 0x00;
        }
        if (collision.gameObject.name == "TorsoIzqB")
        {
            Chaleco.torizqB = 0x00;
        }
    }
}
