using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObjectCollider : MonoBehaviour
{
    float timer = 0f;
    string onOffLight = "none";
    public int thresholdLight;
    AudioSource breakSound;
    Transform ObjectPosition;
    Light ObjectLight;
    Renderer render;
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        breakSound = gameObject.GetComponent<AudioSource>();
        ObjectPosition = gameObject.GetComponent<Transform>();
        ObjectLight = gameObject.GetComponent<Light>();
    }

    void Update()
    {
        if (onOffLight == "On")
        {
            timer += Time.deltaTime;
            ObjectLight.intensity = timer;
            if (timer >= 3)
            {
                onOffLight = "Off";
            }
        }
        else if (onOffLight == "Off")
        {
            timer -= Time.deltaTime;
            ObjectLight.intensity = timer;
            if (timer <= 0)
            {
                timer = 0;
                onOffLight = "none";
                Debug.Log("none");
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "EscenarioV2")
        {
            //Se enciende el sonido
            breakSound.Play();
            render.enabled = false;
            // se cambia el objeto
            Debug.Log(transform.position.x.ToString() + transform.position.y.ToString() + transform.position.z.ToString());
            onOffLight = "On";
        }
    }

}
