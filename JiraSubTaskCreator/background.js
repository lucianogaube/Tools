document.addEventListener('DOMContentLoaded', function() {
	var clipboard = new Clipboard('.btn');
	var clipboard2 = new Clipboard('.clip');
	var checkPageButton = document.getElementById('checkPage');
	var dropdownlist = document.getElementById("teams");
	var dropdownTemplates = document.getElementById("templates");
	var textToSpreadsheet = "";
	
	dropdownTemplates.addEventListener('click', function() {
		var template = dropdownTemplates.value;
		document.getElementById("story").style.display = "none";
		document.getElementById("bug").style.display = "none";
		document.getElementById("cloudStory").style.display = "none";
		document.getElementById("task").style.display = "none";
		
		if(template === "story"){
			document.getElementById("story").style.display = "inline-block";
		}
		else if(template === "bug"){
			document.getElementById("bug").style.display = "inline-block";
		}
		else if(template === "cloudStory"){
			document.getElementById("cloudStory").style.display = "inline-block";
		}
		else if(template === "task"){
			document.getElementById("task").style.display = "inline-block";
		}
	});
	
	function calculateHours(){
		var tasks = document.getElementsByName("t");
		var totaldev = 0;
		var totalqa = 0;
		
		for(i=0;i<tasks.length;i++) 
		{
			if(tasks[i].checked)
			{
				var task = document.getElementsByName(tasks[i].value);
				if(tasks[i].value == "Other")
				{
					other = document.getElementById("otherTask");
					otherSubs = other.value.split(';');
					for(k=0;k<otherSubs.length;k++)
					{
						if(otherSubs[k] != "")
						{
							var temp = otherSubs[k].substring(otherSubs[k].indexOf("(") + 1, otherSubs[k].indexOf(")"));
							if(temp != "")
							{
								totaldev += parseFloat(temp);
							}
						}
					}
					continue;
				}
				
				var hours = task[0].value;			
				if(hours != "")
				{
					if(isQaTask(tasks[i].value) == true)
					{
						totalqa += parseFloat(hours);
					}
					else
					{
						totaldev += parseFloat(hours); 
					}
				}		
			}						
		}
		
		parent = document.getElementsByName("iv");
		textToSpreadsheet = "IV-" + parent[0].value + ";;;" + String(totaldev) + ";0.00;" + String(totalqa);
		
		checkPageButton.setAttribute("data-clipboard-text", textToSpreadsheet);
	}
	
	function isQaTask(taskName){
		var isQA = new Boolean(false);
		if(taskName === "QA Prep" ||
			taskName === "QA Exec" ||
			taskName === "Show And Tell" ||
			taskName === "OWASP" ||
			taskName === "Unit Test Review"){
			isQA = true;
		}
		return isQA;	
	}
	
	function removeColorClass(docToRemove)	{
		var exp = /Color/;
		var classList = docToRemove.className.split(/\s+/);
		for (var i = 0; i < classList.length; i++) {
			if (classList[i].match(exp)) {
				docToRemove.classList.remove(classList[i]);
			}
		}		
	}
	
	dropdownlist.addEventListener('click', function() {
		var team = document.getElementById("teams").value;
		var teams = document.getElementById("teams");
		
		if(team === "10202"){
			if(teams.options[teams.selectedIndex].id === "LightBlue")
			{
				removeColorClass(document.getElementById('checkPage'));			
				removeColorClass(document.getElementById('myBar'));	
				
				document.getElementById("checkPage").classList.add('lightBlueColor');			
				document.getElementById("myBar").classList.add('lightBlueColor');	
				
				var elems = document.getElementsByName("temp");

				[].forEach.call(elems, function(el) {
					removeColorClass(el);
					el.classList.add("lightBlueColor");
				});		
			}
			else
			{
				removeColorClass(document.getElementById('checkPage'));			
				removeColorClass(document.getElementById('myBar'));	
				
				document.getElementById("checkPage").classList.add('blueColor');			
				document.getElementById("myBar").classList.add('blueColor');	
				
				var elems = document.getElementsByName("temp");

				[].forEach.call(elems, function(el) {
					removeColorClass(el);
					el.classList.add("blueColor");
			});		
			}	
		}
		else if(team === "10201"){
			removeColorClass(document.getElementById('checkPage'));			
			removeColorClass(document.getElementById('myBar'));	
			
			document.getElementById("checkPage").classList.add('greenColor');			
			document.getElementById("myBar").classList.add('greenColor');	
			
			var elems = document.getElementsByName("temp");

			[].forEach.call(elems, function(el) {
				removeColorClass(el);
				el.classList.add("greenColor");
			});			
		}
		else if(team === "10200"){
			removeColorClass(document.getElementById('checkPage'));			
			removeColorClass(document.getElementById('myBar'));	
			
			document.getElementById("checkPage").classList.add('redColor');			
			document.getElementById("myBar").classList.add('redColor');	
			
			var elems = document.getElementsByName("temp");

			[].forEach.call(elems, function(el) {
				removeColorClass(el);
				el.classList.add("redColor");
			});				
		}
		else if(team === "12300"){
			removeColorClass(document.getElementById('checkPage'));			
			removeColorClass(document.getElementById('myBar'));	
			
			document.getElementById("checkPage").classList.add('orangeColor');			
			document.getElementById("myBar").classList.add('orangeColor');	
			
			var elems = document.getElementsByName("temp");

			[].forEach.call(elems, function(el) {
				removeColorClass(el);
				el.classList.add("orangeColor");
			});					
		}
		else if(team === "10600"){
			removeColorClass(document.getElementById('checkPage'));			
			removeColorClass(document.getElementById('myBar'));	
			
			document.getElementById("checkPage").classList.add('yellowColor');			
			document.getElementById("myBar").classList.add('yellowColor');	
			
			var elems = document.getElementsByName("temp");

			[].forEach.call(elems, function(el) {
				removeColorClass(el);
				el.classList.add("yellowColor");
			});					
		}
		else if(team === ""){
			removeColorClass(document.getElementById('checkPage'));			
			removeColorClass(document.getElementById('myBar'));	
			
			document.getElementById("checkPage").classList.add('defaultColor');			
			document.getElementById("myBar").classList.add('defaultColor');	
			
			var elems = document.getElementsByName("temp");

			[].forEach.call(elems, function(el) {
				removeColorClass(el);
				el.classList.add("yellowColor");
			});		
		}
	});
  
	function move() {
	var elem = document.getElementById("myBar");   
	var width = 0;
	var id = setInterval(frame, 80);
		function frame() {
			if (width >= 100) {
				elem.innerHTML = 'Done!';
				clearInterval(id);
			} else {	
				width++; 			
				elem.style.width = width + '%'; 
				elem.innerHTML = width * 1  + '%';
				elem.value = width * 1  + '%';				
			}
		}		
	}
	
	checkPageButton.addEventListener('click', function() {
		calculateHours();
		checkPageButton.disabled = true;
		a = document.getElementsByName("t");
		parent=document.getElementsByName("iv");
		team=document.getElementById("teams");
		proj=document.getElementById("projects");
		selectedTeam = team.options[team.selectedIndex].value;
		selectedTeamId = team.options[team.selectedIndex].id;
		selectedProject = proj.options[proj.selectedIndex].value;
		other = document.getElementById("otherTask");
		otherSubs = other.value.split(';');
		ivs = parent[0].value.split(';');
		x=new XMLHttpRequest();
		x.open("POST","https://iquate.atlassian.net/rest/api/2/issue/bulk",true);
		x.setRequestHeader("Content-type","application/json");
		r={"issueUpdates":[]};
		
		for(j=0;j<ivs.length;j++) 
		{
			if(ivs[j] != "")
			{
				for(i=0;i<a.length;i++) 
				{
					if(a[i].checked) 
					{
						if(a[i].value == "Other")
						{
							for(k=0;k<otherSubs.length;k++)
							{
								if(otherSubs[k] != "")
								{
									r.issueUpdates.push
									(
										{
											"fields":
											{
												"project":{"key":selectedProject},
												"summary":otherSubs[k],
												"issuetype":{"name":"Sub-task"},
												"labels":[selectedTeamId],
												"parent":{"key":selectedProject + "-" + ivs[j]},
												"customfield_10200": {"id":selectedTeam}
											}
										}
									);
							}						
								}							
						}					
						else 
						{
							var summary = document.getElementsByName(a[i].value)[0].value;
							if(summary != ""){
								summary = " (" + summary + ")";
							}
							r.issueUpdates.push
							(
								{
									"fields":
									{
										"project":{"key":selectedProject},
										"summary":a[i].value + summary,
										"issuetype":{"name":"Sub-task"},
										"labels":[a[i].id, selectedTeamId],
										"parent":{"key":selectedProject + "-" + ivs[j]},
										"customfield_10200": {"id":selectedTeam}
									}
								}
							);
						}				
					}
				}
			}					
		}
		x.send(JSON.stringify(r));
		move();
	
  }, false);
}, false);

