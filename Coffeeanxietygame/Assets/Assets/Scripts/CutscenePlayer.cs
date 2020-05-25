﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CutscenePlayer : MonoBehaviour
{
    [Tooltip("What gamepad/keyboard button action ID should trigger the next card?")]
    public string advanceButton = "Jump";

    [Tooltip("Which image should be in front, starting from zero?")]
    public int cardToShow = 0;

    [Tooltip("How fast to flip cards (in cards per second)")]
    public float flipSpeed = 5f;

    [Tooltip("How many degrees should each card behind the top card rotate?")]
    public float rotationIncrement = 0f;

    [Tooltip("How far should the next card be offset from the top card position?")]
    public Vector2 fanIncrement = new Vector2(15, -15);

    [Tooltip("Where should the top card fly to when we skip past it?")]
    public Vector2 flipAwayOffset = new Vector2(-100, 0);

    [Tooltip("What should happen when we're left with an empty stack?")]
    public UnityEvent OnFinishedStack;

    Image[] _images;
    Vector2 _centerPosition;
    float _currentCard;
    bool _hasFinished;

    // Call this to move to the next card - you can hook this up to a UI button.
    public bool TryAdvance()
    {
        if (cardToShow >= _images.Length)
            return false;

        cardToShow++;
        return true;
    }

    void Start()
    {
        // Collect all the images inside this parent, ordered from front to back.
        _images = GetComponentsInChildren<Image>();
        System.Array.Reverse(_images);

        // Remember where the lead image is.
        _centerPosition = _images[0].rectTransform.anchoredPosition;

        // Update the display of the rest of the stack.
        Layout();
    }

    // Check input, advance/animate if needed.
    void Update()
    {
        if (Input.GetButtonDown(advanceButton))
        {
            TryAdvance();
        }

        // Check if we've reached our target card and can stop animating.
        if (Mathf.Approximately(cardToShow, _currentCard))
        {

            // Is our target card the end of the stack?
            if (cardToShow == _images.Length && !_hasFinished)
            {
                _hasFinished = true;
                Debug.Log("Finished stack!");

                // Fire an event - this way you can trigger sounds, scene changes, etc.
                if (OnFinishedStack != null)
                    OnFinishedStack.Invoke();
            }

            return;
        }

        // We haven't reached our target card, so animate toward that position.
        // This gives a linear slide, which can look mechanical; you can use easing for more juice.
        _currentCard = Mathf.MoveTowards(_currentCard, cardToShow, flipSpeed * Time.deltaTime);
        Layout();
    }

    // Update the layout of the cards for the current animation frame.
    void Layout()
    {
        // Iterate over all cards in the stack.
        for (int i = 0; i < _images.Length; i++)
        {
            var image = _images[i];

            // For the top card, t = 0. t = 1 for the next card, etc.
            // t < 0 means it's the card that's being removed from the stack.
            float t = i - _currentCard;

            // Fade out the cards we've removed from the stack.    
            var color = image.color;
            color.a = Mathf.Clamp01(t + 1f);
            image.color = color;

            var trans = image.rectTransform;
            // Rotate cards so the current card is upright, and later cards fan out.
            trans.localRotation = Quaternion.Euler(0, 0, rotationIncrement * t);

            // If this is the card we're removing, slide to the flipAwayOffset.
            // Otherwise, shift it slightly from the previous card to fan it out.
            trans.anchoredPosition = _centerPosition + (t < 0f ?
                Vector2.Lerp(flipAwayOffset, Vector2.zero, t + 1f)
                : Mathf.Pow(t, 0.75f) * fanIncrement);
        }
    }
}