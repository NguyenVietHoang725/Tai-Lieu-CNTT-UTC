function checknull(txt)
{
	if(txt.value.length==0)
		return true;
	else
		return false;
}

function validform(f)
{
	
	if(checknull(f.fullname))
	{
		alert(f.fullname.name + " must be not null");
		f.fullname.focus();
		return;
	}
	if(checknull(f.age))
	{
		alert(f.age.name + " must be not null");
		f.age.focus();
		return;
	}
	if(isNaN(f.age.value))
	{
		alert(f.age.name + " must be a number");
		f.age.value="";
		f.age.focus();
		return;
	}
	if(!isInteger(f.age))
	{
		alert(f.age.name + " must be an integer number");
		f.age.value="";
		f.age.focus();
		return;
	}
	if(eval(f.age.value)<=0 || eval(f.age.value)>=200)
	{
		alert(f.age.name + " must be in (0-200)");
		f.age.value="";
		f.age.focus();
		return;
	}
	if(notCheck(f.gender))
	{
		alert("gender must be choosen")
		return;
	}
	if(!StringMatch(f.phone, /^\(\d{2,4}\)[\s.-]\d{3}[\s.-]\d{3}$/))
	{
		alert("not valid")
		return;
	}
}
function StringMatch(txt, reg)
{
	return reg.test(txt.value);	
}
function notCheck(radio)
{
	var rt=true;
	for(i=0; i<radio.length; i++)
		if(radio[i].checked)
			rt=false;
	return rt;
}
function isInteger(txt)
{
	
	if((!isNaN(txt.value)) && (parseInt(txt.value)===Number(txt.value)))
		return true;
	else
		return false;
}


