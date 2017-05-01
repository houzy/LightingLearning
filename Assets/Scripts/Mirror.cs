/*
 * @Author: houzy 
 * @Date: 2017-05-01 10:51:02 
 * @Last Modified by:   houzy 
 * @Last Modified time: 2017-05-01 10:51:02 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

    /// <summary>
    /// 指明镜面的发现方向
    /// </summary>
	public enum NormalOfMirrorPlane {
		// 镜面物体的z轴
		Forward,
		// 镜面物体的y轴
		Up,
		// 镜面物体的x轴
		Right
	};

	/// <summary>
	/// 镜子所在的面
	/// </summary>
	public GameObject MirrorPlane;

    // 镜子所在的物体的哪个轴作为法线方向
	public NormalOfMirrorPlane NormalOfMirror;

    // Reflection Probe
	private ReflectionProbe mProbe;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		mProbe = GetComponent<ReflectionProbe>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.Space))
		{
			// Debug.Log("Plane up  == " + MirrorPlane.transform.up);
			Vector3 mirrorPlaneNormal;
			if (NormalOfMirror == NormalOfMirrorPlane.Up)
			{
                mirrorPlaneNormal = transform.TransformDirection(MirrorPlane.transform.up);
			}
			else if (NormalOfMirror == NormalOfMirrorPlane.Forward)
			{
                mirrorPlaneNormal = transform.TransformDirection(MirrorPlane.transform.forward);
			}
			else
			{
                mirrorPlaneNormal = transform.TransformDirection(MirrorPlane.transform.right);
			}
			// Debug.Log("Plane Normal  == " + mirrorPlaneNormal);
			Vector3 mirrorCenterToCamera = Camera.main.transform.position - MirrorPlane.transform.position; 
			// Debug.Log("Mirror centor to camera == " + mirrorCenterToCamera);
			Vector3 projectVector = Vector3.ProjectOnPlane(mirrorCenterToCamera, mirrorPlaneNormal);
			// Debug.Log("Project vector == " + projectVector);
			Vector3 projectPoint = MirrorPlane.transform.position + projectVector;
			// Debug.Log("Project Point == " + projectPoint);
			mProbe.transform.position = projectPoint + (projectPoint - Camera.main.transform.position);

			mProbe.RenderProbe();
		}
	}
}
