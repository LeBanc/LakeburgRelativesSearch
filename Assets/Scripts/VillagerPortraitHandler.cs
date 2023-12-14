using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class VillagerPortraitHandler : MonoBehaviour
{
    public Image hairBehind;
    public Image face;
    public Image nose;
    public Image mouth;
    public Image eyes;
    public Image wrinkles;
    public Image eyeBrows;
    public Image beard;
    public Image hair;
    public Image over;

    private Material hairBehindMat;
    private Material faceMat;
    private Material noseMat;
    private Material mouthMat;
    private Material eyesMat;
    private Material wrinklesMat;
    private Material eyeBrowsMat;
    private Material beardMat;
    private Material hairMat;

    private void Awake()
    {
        hairBehindMat = new Material(hairBehind.material);
        faceMat = new Material(face.material);
        noseMat = new Material(nose.material);
        mouthMat = new Material(mouth.material);
        eyesMat = new Material(eyes.material);
        wrinklesMat = new Material(wrinkles.material);
        eyeBrowsMat = new Material(eyeBrows.material);
        beardMat = new Material(beard.material);
        hairMat = new Material(hair.material);

        hairBehind.material = hairBehindMat;
        face.material = faceMat;
        nose.material = noseMat;
        mouth.material = mouthMat;
        eyes.material = eyesMat;
        wrinkles.material = wrinklesMat;
        eyeBrows.material = eyeBrowsMat;
        beard.material = beardMat;
        hair.material = hairMat;
    }
}
