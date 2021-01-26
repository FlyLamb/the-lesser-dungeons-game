using System;
using System.Linq;
using UnityEngine;

public class ModelAnimator : MonoBehaviour {
    public Animation[] animations;

    private int current = -1;
    private int frame = 0;
    private float nextFrame;

    public bool isPlaying = false;

    public Animation currentAnimation;

    public void Play(string name) {
        ClearDisplay();
        current = animations.ToList().IndexOf(animations.AsEnumerable().First((s) => { return s.name == name; })); //finds the index of name
        frame = 0;
        nextFrame = Time.time + 1 / animations[current].fps;
        isPlaying = true;
        animations[current].frames[frame].SetActive(true);
    }
    public void Play(int id) {
        ClearDisplay();
        current = id;
        frame = 0;
        nextFrame = Time.time + 1 / animations[current].fps;
        isPlaying = true;
        animations[current].frames[frame].SetActive(true);
    }

    public void ClearDisplay() {
        foreach (Animation a in animations) {
            foreach (GameObject g in a.frames)
                g.SetActive(false);
        }
        
    }

    private void Update() {
        if (current == -1) return;

        currentAnimation = animations[current];
        animations[current].frames[frame].SetActive(false);

        if (Time.time >= nextFrame) {
            if (frame < animations[current].frames.Length - 1) {
                frame++;
            } else if (animations[current].loop) {
                frame = 0;
            } else isPlaying = false;
            nextFrame = Time.time + 1 / animations[current].fps;
        }

        animations[current].frames[frame].SetActive(true);
    }
}

[Serializable]
public class Animation {
    public string name;
    public GameObject[] frames;
    public float fps;
    public bool loop;
}
