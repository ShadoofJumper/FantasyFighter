using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityAction onMainAttackEnd;
    public UnityAction onDie;
    public UnityAction onHit;

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    private Coroutine currentHitAnim;

    private void Start()
    {
        myRenderer              = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext           = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault    = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    public void ShowHitAnim(float delay = 0.0f)
    {
        if(currentHitAnim!=null)
            StopCoroutine(currentHitAnim);
        StartCoroutine(HitAnim(delay));
    }

    IEnumerator HitAnim(float delay)
    {
        yield return new WaitForSeconds(delay);
        whiteSprite();
        for (int i = 0; i < 10; i++)
            yield return null;
        normalSprite();
    }

    private void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    private void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("MainAttackEnd"))
        {
            if(onMainAttackEnd != null)
            {
                //after invoke clear event
                onMainAttackEnd.Invoke();
                onMainAttackEnd = null;
            }
        }
        else if (message.Equals("Die"))
        {
            if (onDie != null)
                onDie.Invoke();
        }
        else if (message.Equals("Hit"))
        {
            if (onHit != null)
                onHit.Invoke();
        }
    }

}
