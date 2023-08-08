using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    //ENCAPSULATION
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int value { get; private set; }
    private Image _background;
    private TextMeshProUGUI _text;
    public bool locked { get; set; }
    private void Awake()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    //Thiết lập thuộc tính cho Tile
    public void SetState(TileState state,int value)
    {
        this.state = state;
        this.value = value;

        _background.color = state.backgroundColor;
        _text.color = state.textColor;
        _text.text = value.ToString();
    }
    //tìm cell trống để spawn tile 
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
    //di chuyển tile khi cell hợp lệ
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
    //Thiết lập hoạt ảnh thuận mắt hơn
   
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
    //merge tile
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
