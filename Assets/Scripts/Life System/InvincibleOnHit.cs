using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Life))]
public class InvincibleOnHit : MonoBehaviour
{
    public float invincibilityTime;

    private Life lifeComponent;

    private void Awake()
    {
        lifeComponent = GetComponent<Life>();
    }

    private void Start()
    {
        lifeComponent.onHealthChange.AddListener(onHealthChange);
    }

    private void onHealthChange(uint lifePoint)
    {
        lifeComponent.isInvincible = true;
        StartCoroutine(Invincibility());

        IEnumerator Invincibility()
        {
            yield return new WaitForSeconds(invincibilityTime);
            lifeComponent.isInvincible = false;
        }
    }
}
