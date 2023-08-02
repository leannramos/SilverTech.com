# Introduction 
This Frontend Boilerplate uses Grunt & Nunjucks & Express to spin up a local server with live reload.  

Last modified by:  DJ Hughes.  Feb 2019

# Getting Started
1.  Install Node.js
2.  open powershell in project folder.
3.  run "npm install"
	This will add "node_modules" which has all the necessary modules for this project.
3.  Run "grunt"
		This will run the project and compile all the SASS and JS, as well as copy the /library folder to both "/public" and "static_html".  This will also compile all nunjuck templates in dev/App/Templates to static html and place them in "static_html".

		static_html will be recompiled any time a SASS or JS file is modified.  It will not compile currently if a template file is modified, so you'll have to re-run "grunt" or save a sass/js file.
4.  You can also run "grunt html" if you want to just recompile the "static_html" folder.  

# Repo
https://silvertech.visualstudio.com/Front-End%20Starter%20Project/_git/Front-End%20Starter%20Project%202019

# Instructions
You can use .html files as templates/modules even though we are using nunjucks.  Any links just include .html so when it compiles to static html the links will be wired up.  