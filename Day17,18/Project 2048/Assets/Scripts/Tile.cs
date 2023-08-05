using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int value { get; private set; }
    private Image bacdground;
    private TextMeshProUGUI text;
    public bool locked { get; set; }
    private void Awake()
    {
        bacdground = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    //Thiết lập thuộc tính cho Tile
    public void SetState(TileState state,int value)
    {
        this.state = state;
        this.value = value;

        bacdground.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = value.ToString();
    }

    public void Spawn(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        transform.position = cell.transform.position;
    }
    public void MoveTo(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        StartCoroutine(Animate(cell.transform.position,false));
    }
    private IEnumerator Animate(Vector3 to,bool merging)
    {
        float elapsed = 0f;
        float duration = 0.1f;
        Vector3 from = transform.position;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = to;
        if (merging)
        {
            Destroy(gameObject);
        }

    }
    public void Merge(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = null;
        cell.tile.locked = true;
        StartCoroutine(Animate(cell.transform.position,true));
    }
}
