Loyal Technical Assessment, completed by Yangsheng Chen, 2022/03



1. Project solution root folder name was "ReviewGenerator", which include solution file "ReviewGenerator.sln", and two projects folders:
	\ReviewGenerator\ReviewGenerator: for project "ReviewGenerator"
	\ReviewGenerator\ReviewGeneratorTests: for MS Unit test project "ReviewGeneratorTests"

2. Solution was developed with C# ASP.NET Core 6.0, and project "ReviewGenerator" was created with Docker enabled (Docker OS: Linux).
   single endpoint (/api/generate) was embeded in a MVC framework project, which provide a simple user-friendly front end interface to user.
   This ASP.Net Core application used Markov chains module to learn from a 5-core subset "Office_Products_5.json", and generate reviews from its trained model.

3. Source code was pushed to GitHub repository "LoyalTest_ReviewGenerator". Dataset file "Office_Products_5.json" was not able to include in the source folder due to its size, and was uploaded to Google Drive for download with link:
		https://drive.google.com/file/d/1w1Z4BZazqmx0nzgpgy-m6qKzCwRF3FY9/view?usp=sharing

4. To build and run the project, can use Visual Studio 2022. Once clone the source folder to local, open solution file "ReviewGenerator.sln" with Visual Studio 2022.
   Need to download dataset file "Office_Products_5.json", and add it to project "ReviewGenerator" from the solution.
   Check to make sure project "ReviewGenerator" is the Startup project, then can build and run it in Debug mode.

5. At local debug environment, run the program at ReviewGenerator profile (in Krestrel), it will start browser to
	URL: https://localhost:7171/
		click button "Generate Review", it will show random generated Rating and auto generated Review content from Markov chains module.
		The action in HomeController called the action in GenerateController to get the Review from  from Markov chains module.
   To see output from endpoint (/api/generate) directly, use browser to connect to:
	URL: https://localhost:7171/api/generate
		it will show JSON output from the browser content.
		Refreshing it will show new content. It directly called the API endpoint /api/generate provided by GenerateController, which return the JSON object as output.

6. At local debug environment, can run the program at Docker profile (if Docker desktop Linux was installed and ran), it will start browser to:
	URL: https://localhost:49153/
		port number 49153 can be found from the Container list of the Docker desktop.
   To see output from endpoint (/api/generate) directly, use browser to connect to:
	URL: https://localhost:49153/api/generate

7. the application "ReviewGenerator" was deploy on Azure as Web app, to test the app on Azure, use browser to connect to:
	URL: https://chentest.azurewebsites.net
   To see output from endpoint (/api/generate) directly, use browser to connect to:
	https://chentest.azurewebsites.net/api/generate

















