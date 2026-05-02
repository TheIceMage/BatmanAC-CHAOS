class Scripts.ChaosEffects extends MovieClip
{
	var CurrentEffect;
	var StartXPos = 1024;
	var StartYPos = 268;
	var PushAmount = 50;
	var ActiveEffects = [];
	var bActiveEffects = [];
	
	
	function CHAOSEffects(){
		super();
		this.CurrentEffect = 0;
	}
	
	
	function AddEffect(EffectName, EffectTime, bShowTime){
		if (bShowTime == undefined) {bShowTime = true;}
		this.CurrentEffect += 1;
		this.ActiveEffects.push(this.attachMovie("CE","CE" + this.CurrentEffect,this.getNextHighestDepth(),{_x:this.StartXPos,_y:this.StartYPos}));
		this["CE" + this.CurrentEffect].CENested.EffectName.text = EffectName;
		this["CE" + this.CurrentEffect].CENested.EffectTime.text = EffectTime;
		this["CE" + this.CurrentEffect].CENested.EffectTime._visible = bShowTime;
		this.MoveStackDown(0);
	}
	
	function MoveStackUp(i){
		if (i == undefined) {i = 0;}
		while(i > 0){
			i -= 1;
			this.ActiveEffects[i]._y -= this.PushAmount;
			}
   		}
		
	function MoveStackDown(i){
		if (i == undefined) {i = 0;}
		while(i < this.ActiveEffects.length - 1){
			this.ActiveEffects[i]._y += this.PushAmount;
			i += 1;
			}
   		}
		
	function AgeEffects(age){
		if (age == undefined) {age = 1;}
		var n = 0;
		for (var i = 0; i < this.ActiveEffects.length; i++) {
			n = Number(this.ActiveEffects[i].CENested.EffectTime.text);
			n -= 1;
			this.ActiveEffects[i].CENested.EffectTime.text = String(n);
			}
		this.DestroyAgedEffects()
		}
		
	function DestroyAgedEffects(){
		var n;
		for (var i = 0; i < this.ActiveEffects.length; i++) {
			n = Number(this.ActiveEffects[i].CENested.EffectTime.text);
			if (n <= 0){
				this.ActiveEffects[i].gotoAndPlay("Outro");
				}
			}
		}
		
	function RemoveEffect(effect){
		for (var i = 0; i < this.ActiveEffects.length; i++) {
      		if (this.ActiveEffects[i] == effect) {
				this.MoveStackUp(i);
         		this.ActiveEffects.splice(i, 1);
         		break;
      			}
   		}
	}
   
}