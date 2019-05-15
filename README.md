# Jira Sub-Task Creator 

As a Scrum Master, I had to manually create all the Sub-tasks for the Stories that were Ready for the Sprint.

The process takes some time, having to create 8-10 Sub-tasks for each Story (around 20 per Sprint), 
when 5-6 of them were Default Sub-tasks, the same for all stories. This (tedious) process was taking me around 1-2 hours per Sprint,
depending on the amount of Stories.

Having that in mind, I used the REST API call provided by Jira and created a Chrome Plug-in
where you can create all the sub-tasks for the Stories you need in just 1 click, cutting down the time spent on this to seconds.

## Pre-Reqs 

You need to be logged in Jira in order to the Plug-in to work.

When you are creating a new sub-task, DO NOT CLICK outside the plug-in, doing so will lose all your changes.

After clicking the CREATE button, wait for the timer to click away as Jira needs some seconds to process the request.


## Custom Data

Projects, Agile Team and Templates:

These are already provided, but it can be easily changed in the backgroung.js to suit your own environment.

Time calculation:
You can use the Textbox after each default sub-task to determine the hours you will spend on it.
For CUSTOM SUB-TASKS the time can be added inside parenthesis ie. 'New custom sub (20)'
These times are calculated when you click in the CREATE button, separating the QA time with the Dev Time, this can be easily changed as well.
After clicking on the CREATE SUB-TASKS it will copy to your Clipboard the Story # + QA total time + Dev total time. ie 'IV-000,45,25'





