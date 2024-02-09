using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out Obstacle obstacle))
    //    {
    //        if (obstacle.obstacleType == Obstacle.ObstacleType.Ice)
    //        {
    //            StartCoroutine(Haptic(0f, 1f, .2f));
    //            StartCoroutine(IceSlide(other.gameObject.GetComponent<IceWall>()));
    //        }
    //    }
    //}

    //float _iceSlideSpeed = 0;
    //IEnumerator IceSlide(IceWall _spline)
    //{
    //    if (spa.Container == null)
    //    {
    //        spa.Container = _spline.Container;
    //
    //        spa.MaxSpeed = lastVel.magnitude;
    //        _iceSlideSpeed = spa.MaxSpeed;
    //
    //        if (_spline.IsStartPoint) 
    //          spa.Restart(true);
    //        else
    //        {
    //            spa.Restart(true);
    //            spa.NormalizedTime = 1f;
    //        }
    //    }
    //
    //    if (_spline.IsStartPoint)
    //    {
    //        if (spa.ElapsedTime / spa.Duration >= 1)
    //        {
    //            Vector3 rot = rb.transform.forward;
    //            spa.Container = null;
    //            rb.AddForce(rot.normalized * Mathf.Pow(_iceSlideSpeed, 2), ForceMode.Force);
    //            
    //            Debug.Log(rot);
    //            yield break;
    //        }
    //        else
    //        {
    //            Debug.Log($"{spa.ElapsedTime / spa.Duration}");
    //            yield return null;
    //            StartCoroutine(IceSlide(_spline));
    //            yield break;
    //        }
    //    }
    //    else
    //    {
    //        if ((-spa.ElapsedTime / spa.Duration) + 2 <= 0f)
    //        {
    //            Vector3 rot = -rb.transform.forward;
    //            spa.Container = null;
    //            rb.AddForce(rot.normalized * Mathf.Pow(_iceSlideSpeed, 2), ForceMode.Force);
    //    
    //            Debug.Log(rot);
    //            yield break;
    //        }
    //        else
    //        {
    //            Debug.Log($"{(-spa.ElapsedTime / spa.Duration) + 2}");
    //            yield return null;
    //            StartCoroutine(IceSlide(_spline));
    //            yield break;
    //        }
    //    }
    //}
}
