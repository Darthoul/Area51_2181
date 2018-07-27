using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBehaviour : MonoBehaviour {

    public class CamData {
        public float moveSpeed;
        public Vector3 targetDistance;
        public Transform target;

        public CamData (float moveSpeed, Vector3 targetDistance, Transform target) {
            this.moveSpeed = moveSpeed;
            this.targetDistance = targetDistance;
            this.target = target;
        }

        public bool CompareValues (CamData otherData) {
            return moveSpeed == otherData.moveSpeed &&
                   targetDistance == otherData.targetDistance &&
                   target == otherData.target;
        }
    }

    public float moveSpeed;
    public Vector3 targetDistance;
    public Transform target;
    Vector3 targetNode;

	// Update is called once per frame
	void LateUpdate () {
        targetNode = target.position + (target.right * targetDistance.x) + (target.up * targetDistance.y) + (target.forward * targetDistance.z);
        transform.position = Vector3.MoveTowards (transform.position, targetNode, moveSpeed * Time.deltaTime);
        transform.LookAt (target.position + Vector3.up * 1.5f);
    }

    void OnDrawGizmos () {
        Gizmos.DrawSphere (targetNode, 0.25f);
    }

    public void SetCamData (CamData camData) {
        moveSpeed = camData.moveSpeed;
        targetDistance = camData.targetDistance;
        target = camData.target;
    }

    public CamData GetCamData () {
        return new CamData (moveSpeed, targetDistance, target);
    }
}
