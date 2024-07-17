using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KeypadController : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private string _targetPassword;
    [SerializeField] private int _passwordCountLimit;
    [SerializeField] private TextMeshProUGUI _passwordText;
    [SerializeField] private KeypadKey[] _keypadKeys;
    [SerializeField] private UnityEvent _onKeypadSolve;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _correctSound;
    [SerializeField] private AudioClip _wrongSound;

    private void Start()
    {
        foreach (var key in _keypadKeys)
        {
            key.OnInteract.AddListener(delegate { PasswordEntry(key.KeyValue); } );
        }

        _passwordText.text = "";
    }

    public void PasswordEntry(string number)
    {
        if (number == "Clear")
        {
            Clear();
            return;
        }
        else if(number == "Enter")
        {
            Enter();
            return;
        }

        int length = _passwordText.text.ToString().Length;
        if(length< _passwordCountLimit)
        {
            _passwordText.text = _passwordText.text + number;
        }
    }

    public void Clear()
    {
        _passwordText.text = "";
        _passwordText.color = Color.white;
    }

    private void Enter()
    {
        if (_passwordText.text == _targetPassword)
        {
            if (_audioSource != null)
                _audioSource.PlayOneShot(_correctSound);

            _passwordText.color = Color.green;
            StartCoroutine(waitAndClear());

            _onKeypadSolve.Invoke();
        }
        else
        {
            if (_audioSource != null)
                _audioSource.PlayOneShot(_wrongSound);

            _passwordText.color = Color.red;
            StartCoroutine(waitAndClear());
        }
    }

    IEnumerator waitAndClear()
    {
        yield return new WaitForSeconds(0.75f);
        Clear();
    }
}
