using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public SpriteRenderer MySpriteRenderer;

    public Sprite SweatParticle;
    public Sprite SteamParticle;

    float lifetime { get; set; }
    float curLifetime { get; set; }

    private void Update()
    {
        curLifetime -= Time.deltaTime;
        MySpriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, curLifetime / lifetime));

        if (curLifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetParticle(Sprite customSprite)
    {
        MySpriteRenderer.sprite = customSprite;

        lifetime = .8f;
        transform.localPosition = Vector3.up * .5f;
        iTween.MoveAdd(this.gameObject, Vector3.down * .2f, lifetime);
        curLifetime = lifetime;
    }

    public void SetSweatParticle()
    {
        MySpriteRenderer.sprite = SweatParticle;

        lifetime = .25f;
        transform.localPosition = Vector3.up * .5f + Vector3.right * .5f;
        iTween.MoveAdd(this.gameObject, Vector3.up * .25f + Vector3.right * .25f, lifetime);
        curLifetime = lifetime;
    }

    public void SetSteamParticle()
    {
        MySpriteRenderer.sprite = SteamParticle;

        lifetime = .5f;
        transform.localPosition = Vector3.up * .5f + Vector3.right * .5f;
        iTween.MoveAdd(this.gameObject, Vector3.up * .125f + Vector3.right * .125f, lifetime);
        curLifetime = lifetime;
    }
}
