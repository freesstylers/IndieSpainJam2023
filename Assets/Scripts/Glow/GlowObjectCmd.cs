using UnityEngine;
using System.Collections.Generic;

public class GlowObjectCmd : MonoBehaviour
{
	public Color GlowColor = new Color(0.5686275f, 0.2941177f, 0.8784314f);
	public float LerpFactor = 10;

    public bool running = true;

    bool off = false;

	public Renderer[] Renderers
	{
		get;
		private set;
	}

	public Color CurrentColor
	{
		get { return _currentColor; }
	}

	private Color _currentColor;
	private Color _targetColor;

	void Start()
	{
		Renderers = GetComponentsInChildren<Renderer>();
		GlowController.RegisterObject(this);
    }

    public void Enter()
    {
        if (!off)
        {
            _targetColor = GlowColor;
            running = true;
		}
    }

    public void Exit()
    {
        _targetColor = Color.black;
        running = true;
    }

    public void TurnOff()
    {
        _targetColor = Color.black;
        running = true;
        off = true;
    }

    public void TurnOn()
    {
        off = false;
        Enter();
    }

    /// <summary>
    /// Update color, disable self if we reach our target color.
    /// </summary>
    private void Update()
	{
        if(running)
        {
		    _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

		    if (_currentColor.Equals(_targetColor))
		    {
			    running = false;
            }
        }
    }
}
