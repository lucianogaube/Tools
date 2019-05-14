document.addEventListener('DOMContentLoaded', function() {
	var clipboard = new Clipboard('.btn');
	var clipboard2 = new Clipboard('.clip');
	var createButton = document.getElementById('checkPage');
	var dropdownlist = document.getElementById("teams");
	var dropdownTemplates = document.getElementById("templates");
	var textToSpreadsheet = "";
	var QATasks = new Set(["QA Prep", "QA Prep", "Show And Tell", "OWASP", "Unit Test Review"]);
	
	//Template button will change according to selection
	dropdownTemplates.addEventListener('click', function() {
		var template = dropdownTemplates.value;
		document.getElementById("story").style.display = "none";
		document.getElementById("bug").style.display = "none";
		document.getElementById("cloudStory").style.display = "none";
		document.getElementById("task").style.display = "none";
		
		if(template != null){
			document.getElementById(template).style.display = "inline-block";	
		}
	});
	
	function calculateHours(){
		var tasks = document.getElementsByName("t");
		var totaldev = 0;
		var totalqa = 0;
		
		tasks.forEach(task => {
			if(task.checked){
				var taskTime = document.getElementsByName(task.value); 
				if(task.value == "Other")
				{
					other = document.getElementById("otherTask");
					otherSubs = other.value.split(';');          
					otherSubs.forEach(other =>
					{
						if(other != "")
						{
							var temp = getCustomTaskTime(other);
							if(temp != "")
							{
								totaldev += parseFloat(temp);
							}
						}
					});
					return;
				}
				
				var hours = taskTime[0].value;			
				if(hours != ""){
					if(isQaTask(task.value) == true){
						totalqa += parseFloat(hours);
					}
					else{
						totaldev += parseFloat(hours); 
					}
				}		
			}			
		});
		
		parent = document.getElementsByName("iv");
		//Text to clipboard, can be used in spreadsheet later
		textToSpreadsheet = "IV-" + parent[0].value + ";" + String(totaldev) + ";" + String(totalqa);		
		createButton.setAttribute("data-clipboard-text", textToSpreadsheet);
	}
  
	function getCustomTaskTime(taskName){
		var res = taskName.match(/\(([0-9]\d+)\)/);
		if(res === null) return ""

		return res[1];
	}
	
	function isQaTask(taskName){
		var isQA = false;
		if(QATasks.has(taskName)){
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
  
	function changeColorTeam(color){
		removeColorClass(document.getElementById('checkPage'));			
		removeColorClass(document.getElementById('myBar'));	

		document.getElementById("checkPage").classList.add(color);			
		document.getElementById("myBar").classList.add(color);	

		elems = document.getElementsByName("temp");

		[].forEach.call(elems, function(el) {
		  removeColorClass(el);
		  el.classList.add(color);
		});		
	}
	
	dropdownlist.addEventListener('click', function() {
		var team = document.getElementById("teams").value;
		var teams = document.getElementById("teams");
		var elems;
    
		switch(team){
				case "10202":
			  if(teams.options[teams.selectedIndex].id === "LightBlue")
			  {
				changeColorTeam("lightBlueColor");		
			  }
			  else
			  {
				changeColorTeam("blueColor");	
			  }
			  break;
			case "10201":
				changeColorTeam("greenColor");
			  break;
			case "10200":
				changeColorTeam("redColor");
			  break;
			case "12300":
				changeColorTeam("orangeColor");
			  break;
			case "10600":
				changeColorTeam("yellowColor");
			  break;
			default:
				changeColorTeam("defaultColor");
			  break;
		}
	});
  
  //Progress bar
	function InitProgressBar() {
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
	
	createButton.addEventListener('click', function() {
		calculateHours();
		createButton.disabled = true;
		var subTasks = document.getElementsByName("t");
		var parent = document.getElementsByName("iv");
		var team = document.getElementById("teams");
		var proj = document.getElementById("projects");
		var selectedTeam = team.options[team.selectedIndex].value;
		var selectedTeamId = team.options[team.selectedIndex].id;
		var selectedProject = proj.options[proj.selectedIndex].value;
		var other = document.getElementById("otherTask");
		var otherSubs = other.value.split(';');
		var stories = parent[0].value.split(';');
		var request = new XMLHttpRequest();
		request.open("POST","https://iquate.atlassian.net/rest/api/2/issue/bulk",true);
		request.setRequestHeader("Content-type","application/json");
		var resp = {"issueUpdates":[]};
		
		stories.forEach(story => {
			if(story == "") return;
			
			subTasks.forEach(subTask => {
				if(!subTask.checked) return;
				
				if(subTask.value == "Other"){
					otherSubs.forEach(otherSub =>{
						if(otherSub === "") return;
						
						resp.issueUpdates.push({
							"fields":
							{
								"project":{"key":selectedProject},
								"summary":otherSub,
								"issuetype":{"name":"Sub-task"},
								"labels":[selectedTeamId],
								"parent":{"key":selectedProject + "-" + story},
								"customfield_10200": {"id":selectedTeam}
							}
						});													
					});							
				}					
				else {
					var subTaskTime = document.getElementsByName(subTask.value)[0].value;
					if(subTaskTime != ""){
						subTaskTime = " (" + subTaskTime + ")";
					}
					resp.issueUpdates.push({
						"fields":
						{
							"project":{"key":selectedProject},
							"summary":subTask.value + subTaskTime,
							"issuetype":{"name":"Sub-task"},
							"labels":[subTask.id, selectedTeamId],
							"parent":{"key":selectedProject + "-" + story},
							"customfield_10200": {"id":selectedTeam}
						}
					});
				}				
			});							
		});
		
		request.send(JSON.stringify(resp));
		InitProgressBar();
	
  }, false);
}, false);

