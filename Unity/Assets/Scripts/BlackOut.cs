using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
  public float Duration = 1f;
  private float _elapsed;
  public Image MainBack;
  private bool _hide;
  public AnimationCurve Curve;

  public void SetColor(Color color, bool hide)
  {
    _hide = hide;
    color.a = hide ? 0f : 1f;
    MainBack.color = color;
  }

  void Update()
  {
    _elapsed += Time.deltaTime;

    var color = MainBack.color;
    color.a = Mathf.Lerp(_hide ? 0f : 1f, !_hide ? 0f : 1f, Curve.Evaluate(_elapsed/Duration));
    MainBack.color = color;

    if(_hide && _elapsed/Duration > 1f)
      Destroy(gameObject);
  }
}
