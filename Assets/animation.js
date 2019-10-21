#pragma strict
var save : Animator;
function Start () {
	save = this.GetComponent(Animator);
}

function Update () {
		save.SetInteger("int",0);

}
