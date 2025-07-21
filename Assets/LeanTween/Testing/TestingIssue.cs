using UnityEngine;

public class TestingIssue : MonoBehaviour
{
    private LTDescr lt, ff;
    private int id, fid;

    private void Start()
    {
        LeanTween.init();

        lt = LeanTween.move(gameObject, 100 * Vector3.one, 2);
        id = lt.id;
        LeanTween.pause(id);

        ff = LeanTween.move(gameObject, Vector3.zero, 2);
        fid = ff.id;
        LeanTween.pause(fid);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Debug.Log("id:"+id);
            LeanTween.resume(id);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LeanTween.resume(fid);
        }
    }
}