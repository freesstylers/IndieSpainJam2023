using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRefreshCollider : MonoBehaviour
{
    PolygonCollider2D col;
    SpriteRenderer spriteRenderer;
    List<Vector2> physicsShape = new List<Vector2>();

    bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        check = false;
        col = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
        if (check)
            return;

        check = true;
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        col.SetPath(0, physicsShape);
    }
}
