document.addEventListener('DOMContentLoaded', function() {
	var clipboard = new Clipboard('.btn');

	var checkPageButton = document.getElementById('checkPage');
	checkPageButton.addEventListener('click', function() {
    checkPageButton.disabled = true; 
    a = document.getElementsByName("t");
	parent=document.getElementsByName("iv");
	team=document.getElementById("teams");
	proj=document.getElementById("projects");
	selectedTeam = team.options[team.selectedIndex].value;
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
						r.issueUpdates.push
						(
							{
								"fields":
								{
									"project":{"key":selectedProject},
									"summary":a[i].value,
									"issuetype":{"name":"Sub-task"},
									"labels":[a[i].id],
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
	
  }, false);
}, false);

