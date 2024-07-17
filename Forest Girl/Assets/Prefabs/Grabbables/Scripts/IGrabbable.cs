using UnityEngine;

public interface IGrabbable
{
    public void Grab();

    public string Name { get; }
    public Sprite Icon { get; }
    public GameObject Prefab { get; }
    public GameObject gameObject { get; }
    public Rigidbody Rigidbody { get; }
}
